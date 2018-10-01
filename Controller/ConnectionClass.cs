using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Deportes_WPF.Controller
{
    class ConnectionClass
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;
        private bool status; // true. ok, false: sin-conexion

        string query;
        private MySqlCommand cmd;
        private MySqlDataReader reader;


        public ConnectionClass()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            status = false;


            server = "estudiantes.is.escuelaing.edu.co";
            database = "deportes";
            user = "deportes";
            password = "deportes20182";
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);
        }

        //open connection
        public bool OpenConnection()
        {
            if (!status)
            {
                System.Windows.MessageBox.Show("Conexión no iniciada");

                return false;
            }
            else
            {
                try
                {
                    connection.Open();

                    return true;
                }
                catch (MySqlException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message + connectionString);

                    return false;
                }
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                status = false;
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }
        }


        //Execute query
        public MySqlDataReader executeQuery(string query) {

            MySqlDataReader result = null;

            if (!status)
            {
                System.Windows.MessageBox.Show("Conexión no iniciada");
            }
            else {                
                cmd = new MySqlCommand(query, connection);
                reader = cmd.ExecuteReader();
                result = reader;
            }

            return result;
        }


        //Insert statement
        public void Insert()
        {

            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string>[] Select()
        {

            string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

       

    }
}
