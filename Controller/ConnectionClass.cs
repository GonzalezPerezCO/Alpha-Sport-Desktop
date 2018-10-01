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
        private string connectionString;
        private bool status; // true. ok, false: sin-conexion

        private readonly string server = "estudiantes.is.escuelaing.edu.co";
        private readonly string database = "deportes";
        private readonly string user = "deportes";
        private readonly string password = "deportes20182";
        private readonly string port = "3306";         
        private readonly string sslM = "none";

        private MySqlCommand cmd;
        private MySqlDataReader reader;


        public ConnectionClass()
        {
            // cuerpo constructor
        }

        //Initialize values
        public void Initialize()
        {
            status = false;
            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            connection = new MySqlConnection(connectionString);
            status = true;             
        }

        public bool getStatus() {
            return status;
        }

        //open connection
        public void OpenConnection()
        {            
            if (!status)
            {
                //System.Windows.MessageBox.Show("Conexión no iniciada");
                throw new System.ArgumentException("Conexión no iniciada");
            }
            else
            {
                try
                {
                    connection.Open();
                }
                catch (MySqlException ex)
                {
                    //System.Windows.MessageBox.Show(ex.Message + connectionString);
                    throw new System.ArgumentException("ex.Message + connectionString");                    
                }
            }

        }

        //Close connection
        public void CloseConnection()
        {
            try
            {
                connection.Close();
                status = false;                
            }
            catch (MySqlException ex)
            {
                //System.Windows.MessageBox.Show(ex.Message);
                throw new System.ArgumentException(ex.Message);
                
            }
        }

        //Login
        public bool loginConnection(string email, string password) {
            string queryLog = "select email from tadmin where email= '" + email + "' and password= '" + password+ "';";
            cmd = new MySqlCommand(queryLog, connection);
            reader = cmd.ExecuteReader();

            if (reader.Read()) return true;
            else return false;
        }

        //Execute query
        public MySqlDataReader queryTable(string query) {
            reader = null;

            if (!status)
            {
                System.Windows.MessageBox.Show("Conexión no iniciada");
            }
            else {                
                cmd = new MySqlCommand(query, connection);
                reader = cmd.ExecuteReader();
            }

            return reader;
        }


        //Insert statement
        public void Insert()
        {

            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            OpenConnection();
            if ( this.status == true)
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
            OpenConnection();
            if (this.status == true)
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

            OpenConnection();
            if (this.status == true)
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
            OpenConnection();
            if (this.status == true)
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
            OpenConnection();
            if (this.status == true)
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
