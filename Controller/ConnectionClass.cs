﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deportes_WPF.Model;
using MySql.Data.MySqlClient;

namespace Deportes_WPF.Controller
{
    class ConnectionClass
    {
        private static ConnectionClass instance = null;

        private MySqlConnection connection;
        private string connectionString;

        private readonly string server = "estudiantes.is.escuelaing.edu.co";
        private readonly string database = "deportes";
        private readonly string user = "deportes";
        private readonly string password = "deportes20182";
        private readonly string port = "3306";         
        private readonly string sslM = "none";

        private MySqlCommand cmd;
        private MySqlDataReader reader;

        private ConnectionClass()
        {
            Debug.WriteLine(" ----   INITIALIZATE");
            Initialize();
        }

        public static ConnectionClass GetInstance()
        {
            Debug.WriteLine(" ----  GET INSTANCE ");
            if (instance == null)
            {
                instance = new ConnectionClass();
                Debug.WriteLine(" ----   NEW INSTANCE");
            }

            return instance;
        }

        //Initialize values
        public void Initialize()
        {
            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            connection = new MySqlConnection(connectionString);

            Debug.WriteLine(" ----   END INITIALIZATE");
        }

        
        //open connection
        public void OpenConnection()
        {            
            try
            {
                connection.Open();

                Debug.WriteLine(" ----   TRY OPEN CONNECTION");
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----  CATCH OPEN CONNECTION ");
                Debug.WriteLine(ex.Message + connectionString);               
            }          

        }

        //Close connection
        public void CloseConnection()
        {
           connection.Close();

            Debug.WriteLine(" ----   CLOSE CONNECTION");           
        }

        //Login
        public User loginConnection(string email, string password) {
            
            User result = null;

            string queryLog = "select email, nombre, apellido from tadmin where email= '" + email + "' and password= '" + password+ "';";

            Debug.WriteLine(" ----   LOGIN CONNECTION OPEN");

            this.OpenConnection();

            cmd = new MySqlCommand(queryLog, connection);
            reader = cmd.ExecuteReader();


            if (reader.Read())
            {                
                result = new User((string)reader["email"], (string)reader["nombre"], (string)reader["apellido"]);
            }
            Debug.WriteLine(" ----   result: " + result.ToString());
            this.CloseConnection();
            Debug.WriteLine(" ----   result: "+ result.ToString());
            return result;
        }

        //Execute query
        public MySqlDataReader queryTable(string query) {
            
            reader = null;

            Debug.WriteLine(" ----   QUERY TABLE OPEN CONNECTION");

            this.OpenConnection();
            
            cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();            

            this.CloseConnection();

            return reader;
        }


        //Insert statement
        public void Insert()
        {

            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            OpenConnection();
           
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
            
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            this.OpenConnection();
           
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

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

           this. OpenConnection();
           
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
            
        }

        //Select statement
        public List<string>[] Select()
        {
            /*
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
            }*/

            return null;
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;
            
            //Create Mysql Command
            MySqlCommand cmd = new MySqlCommand(query, connection);

            //ExecuteScalar will return one value
            Count = int.Parse(cmd.ExecuteScalar() + "");

            //close Connection
            this.CloseConnection();

            return Count;         
        }

       

    }
}
