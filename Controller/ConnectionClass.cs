using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AlphaSport.Model;
using MySql.Data.MySqlClient;

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace AlphaSport.Controller
{
    internal class ConnectionClass : IDisposable
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
                
        private bool disposed = false; // Flag: Has Dispose already been called?
        
        public void Dispose()
        {   
            Dispose(true); // Dispose of unmanaged resources.            
            GC.SuppressFinalize(this); // Suppress finalization.
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                instance.Dispose();
                connection.Dispose();
                cmd.Dispose();
                reader.Dispose();
            }

            connectionString = null;
            disposed = true;
        }

        ~ConnectionClass()
        {
            Dispose(false);
        }

        // https://canyouhearthebits.wordpress.com/2008/08/08/como-implementar-correctamente-idisposable/
        // https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose

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
                Debug.WriteLine("CATCH EX: "+ex.Message);               
            }          

        }

        //Close connection
        public void CloseConnection()
        {
           connection.Close();

            Debug.WriteLine(" ----   CLOSE CONNECTION");           
        }

        //Login
        public User LoginConnection(string email, string password) {
            
            User result = null;

            string queryLog = "select email, nombre, apellido from tadmin where email= '" + email + "' and password= '" + password+ "';";

            Debug.WriteLine(" ----   LOGIN CONNECTION OPEN");

            this.OpenConnection();

            cmd = new MySqlCommand(queryLog, connection);
            reader = cmd.ExecuteReader();


            if (reader.Read())
            {                
                result = new User((string)reader["email"], (string)reader["nombre"], (string)reader["apellido"]);                
                Debug.WriteLine(" ----   result: " + result.ToString());
            }
            this.CloseConnection();

            Debug.WriteLine(" ----   result: retorna");
            return result;
        }

        //Buscar estudiante
        public bool BuscarEstudiante(UInt64 codigo, string email)
        {
            bool result = false;

            string queryEstu = "SELECT id_e FROM testudiantes WHERE codigo = " + codigo + " OR email = '" + email + "';";

            Debug.WriteLine(" ----  BUSCAR ESTUDIANTE");

            this.OpenConnection();

            cmd = new MySqlCommand(queryEstu, connection);
            reader = cmd.ExecuteReader();


            if (reader.Read())
            {
                result = true;
                Debug.WriteLine(" ----   result: " + result.ToString());
            }
            this.CloseConnection();

            Debug.WriteLine(" ----   result: retorna");
            return result;
        }

        //Execute query return Table
        public DataTable MostrarTabla(string query) {
            
            reader = null;
            DataTable dt = new DataTable();

            Debug.WriteLine(" ----   QUERY TABLE OPEN CONNECTION");

            this.OpenConnection();
            
            cmd = new MySqlCommand(query, connection);

            try {
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Debug.WriteLine(" ----   If true READER HASROWS");
                    dt.Load(reader);
                }
                else
                {
                    Debug.WriteLine(" ----   else MOSTRAR TABLA TO STRING: " + reader.ToString());
                }


            } catch(MySqlException ex) {
                Debug.WriteLine(" ----   CATCH QERY TABLE: "+ex);
            }

            this.CloseConnection();

            return dt;
        }

        //execute one query
        public void QueryExecute(string query)
        {
            Debug.WriteLine(" ----   Query add estu full");
            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();            
            this.CloseConnection();

            Debug.WriteLine(" ----   FIN; Query add estu full");            
        }

        public List<string> BuscarCasilleroReader(string query)
        {
            reader = null;
            List<string> result = new List<string>();

            Debug.WriteLine(" ----   QUERY READER OPEN CONNECTION buscar_casillero reader");

            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);

            try
            {
                Debug.WriteLine(" ----   RESULT QERY TRY recibido buscar_casillero reader: " + query);
                reader = cmd.ExecuteReader();

                // Lista: nombre, codigo, casillero, disponible{0:no, 1:si}, entrada, salida

                while (reader.Read())
                {
                    if (reader.GetString(0) != null) { result.Add(reader.GetString(0)); } else { result.Add("N/A"); }
                    if (reader.GetString(1) != null) { result.Add(Convert.ToString(reader.GetString(1))); } else { result.Add("0"); }
                    if (reader.GetString(2) != null) { result.Add(Convert.ToString(reader.GetString(2))); } else { result.Add("0"); }
                    if (reader.GetString(3) != null) { result.Add(Convert.ToString(reader.GetBoolean(3))); } else { result.Add("0"); }
                    result.Add(reader.GetMySqlDateTime(4).ToString());
                    result.Add(reader.GetMySqlDateTime(5).ToString());
                }

                Debug.WriteLine(" ----   RESULT QERY READER buscar_casillero reader: tamaño "+result.Count);
                foreach (var item in result)
                {
                    Debug.WriteLine(" ---- " + item.ToString());
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----   CATCH QERY READER buscar_casillero reader: " + ex);
            }

            this.CloseConnection();

            return result;
        }

        public string CambiarHorario(int hora, string dia, string email)
        {
            string result = "";

            string queryEstu = "call addHorario("+hora+", '"+dia+"', '"+email+"')";

            Debug.WriteLine(" ----  ADD_HORARIO");

            this.OpenConnection();

            cmd = new MySqlCommand(queryEstu, connection);

            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("<<< catch : "+ ex.Message);
                result = ex.Message;
            }

            
            this.CloseConnection();

            Debug.WriteLine(" ----   ADD_HORARIO result: retorna");

            return result;
        }

        public string AddImplementoPrestamo(string query)
        {
            string result = "";

            Debug.WriteLine(" ----  ADD_IMPL_PRESTAMO");

            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);

            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("<<< catch : " + ex.Message);
                result = ex.Message;
            }


            this.CloseConnection();

            Debug.WriteLine(" ----   ADD_IMPL_PRESTAMO result: retorna");

            return result;
        }

        public string DevuelveImplementoPrestamo(string query)
        {
            string result = "";

            Debug.WriteLine(" ----  DEVUELVE_IMPL_PRESTAMO");

            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);

            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("<<< catch : " + ex.Message);
                result = ex.Message;
            }


            this.CloseConnection();

            Debug.WriteLine(" ----   DEVUELVE_IMPL_PRESTAMO result: retorna");

            return result;
        }

        public List<List<string>> CasillerosDisponiblesReader(string queryDisp, string querySecc)
        {
            reader = null;
            List<List<string>> result = new List<List<string>>();

            List<string> disp = new List<string>();
            List<string> secc = new List<string>();

            Debug.WriteLine(" ----   QUERY READER OPEN CONNECTION casilleros_disp reader");

            this.OpenConnection();

            cmd = new MySqlCommand(queryDisp, connection);

            try
            {
                Debug.WriteLine(" ----   RESULT QERY TRY recibido casilleros_disp reader: " + queryDisp);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   disp.Add(reader.GetString(0));

                }

                Debug.WriteLine(" ----   RESULT QERY READER casilleros_disp reader: ");
                foreach (var item in disp)
                {
                    Debug.WriteLine(" ---- " + item.ToString());
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----   CATCH QERY READER casilleros_disp reader: " + ex);
            }

            this.CloseConnection();
            this.OpenConnection();

            cmd = new MySqlCommand(querySecc, connection);

            try
            {
                Debug.WriteLine(" ----   RESULT QERY TRY recibido casilleros_disp reader: " + querySecc);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    secc.Add(reader.GetString(0));

                }

                Debug.WriteLine(" ----   RESULT QERY READER casilleros_disp reader: ");
                foreach (var item in secc)
                {
                    Debug.WriteLine(" ---- " + item.ToString());
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----   CATCH QERY READER casilleros_disp reader: " + ex);
            }

            this.CloseConnection();

            result.Add(disp);
            result.Add(secc);

            foreach (var item in result)
            {
                foreach (var item2 in item)
                {
                    Debug.WriteLine("<<<< Info result: "+item2);
                }
            }

            return result;
        }


        //Execute query return Array Asistencia datos
        public List<string> AsistenciaReader(string query)
        {

            reader = null;
            List<string> result = new List<string>();

            Debug.WriteLine(" ----   QUERY READER OPEN CONNECTION asistencia reader");

            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);

            try
            {
                Debug.WriteLine(" ----   RESULT QERY TRY recibido asistencia reader: " + query);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(0) != null) { result.Add(reader.GetString(0)); } else { result.Add("N/A"); }
                    if (reader.GetString(1) != null) { result.Add(reader.GetString(1)); } else { result.Add("N/A"); }
                    if (reader.GetString(2) != null) { result.Add(reader.GetString(2)); } else { result.Add("N/A"); }
                    if (reader.GetString(3) != null) { result.Add(Convert.ToString(reader.GetString(3))); } else { result.Add("0"); }
                    if (reader.GetString(4) != null) { result.Add(Convert.ToString(reader.GetString(4))); } else { result.Add("0"); }
                    if (reader.GetString(5) != null) { result.Add(Convert.ToString(reader.GetString(5))); } else { result.Add("0"); }
                    if (reader.GetString(6) != null) { result.Add(reader.GetString(6)); } else { result.Add("N/A"); }
                    //if (reader.GetString(6) != null) { result.Add(reader.GetString(6)); } else { result.Add("N/A"); }
                    //if (reader.GetString(7) != null) { result.Add(reader.GetString(7)); } else { result.Add("N/A"); }
                    //if (reader.GetString(8) != null) { result.Add(Convert.ToString(reader.GetString(8))); } else { result.Add("0"); }
                    //if (reader.GetString(9) != null) { result.Add(Convert.ToString(reader.GetString(9))); } else { result.Add("0"); }
                    //if (reader.GetString(10) != null) { result.Add(Convert.ToString(reader.GetString(10))); } else { result.Add("0"); }

                }

                Debug.WriteLine(" ----   RESULT QERY READER asistencia reader: ");
                foreach (var item in result)
                {
                    Debug.WriteLine(" ---- " + item.ToString());
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----   CATCH QERY READER asistencia reader: " + ex);
            }

            this.CloseConnection();

            return result;
        }

        //Execute query return Array Datos Estudiante
        public List<string> DatosEstuReader(string query)
        {

            reader = null;
            List<string> result = new List<string>();

            Debug.WriteLine(" ----   QUERY READER OPEN CONNECTION Datos_Estudiante reader");

            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);

            try
            {
                Debug.WriteLine(" ----   RESULT QERY TRY recibido Datos_Estudiante  reader: " + query);
                reader = cmd.ExecuteReader();

                while (reader.Read()) //0: nombre, 1: carrera, 2: email, 3: semestre, 4: fallas, 5: asistencias, 6: codigo
                {
                    if (reader.GetString(0) != null) { result.Add(reader.GetString(0)); } else { result.Add("N/A"); }
                    if (reader.GetString(1) != null) { result.Add(reader.GetString(1)); } else { result.Add("N/A"); }
                    if (reader.GetString(2) != null) { result.Add(reader.GetString(2)); } else { result.Add("N/A"); }
                    if (reader.GetString(3) != null) { result.Add(Convert.ToString(reader.GetString(3))); } else { result.Add("0"); }
                    if (reader.GetString(4) != null) { result.Add(Convert.ToString(reader.GetString(4))); } else { result.Add("0"); }
                    if (reader.GetString(5) != null) { result.Add(Convert.ToString(reader.GetString(5))); } else { result.Add("0"); }
                    if (reader.GetString(6) != null) { result.Add(Convert.ToString(reader.GetString(6))); } else { result.Add("0"); }
                }

                Debug.WriteLine(" ----   RESULT QERY READER Datos_Estudiante  reader: ");
                foreach (var item in result)
                {
                    Debug.WriteLine(" ---- " + item.ToString());
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----   CATCH QERY READER Datos_Estudiante  reader: " + ex);
            }

            this.CloseConnection();

            return result;
        }


        //Execute query return Array casilleors disponibles
        public List<string> ListaUnicaReader(string query)
        {

            reader = null;
            List<string> result = new List<string>();

            Debug.WriteLine(" ----   QUERY READER OPEN CONNECTION casilleors disponibles reader");

            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);

            try
            {
                Debug.WriteLine(" ----   RESULT QERY TRY recibido casilleors disponibles reader: " + query);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }

                Debug.WriteLine(" ----   RESULT QERY READER casilleors disponibles reader: ");
                foreach (var item in result)
                {
                    Debug.WriteLine(" ---- " + item.ToString());
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----   CATCH QERY READER casilleors disponibles reader: " + ex);
            }

            this.CloseConnection();

            return result;
        }


        //Query statament one string
        public List<string> QuerySumarAsistencia(string query)
        {
            reader = null;
            List<string> result = new List<string>();

            Debug.WriteLine(" ----   QUERY OPEN CONNECTION statament FALLAS_ASISTENCIAS");

            this.OpenConnection();

            cmd = new MySqlCommand(query, connection);

            try
            {
                Debug.WriteLine(" ----   RESULT QERY TRY statament FALLAS_ASISTENCIAS: " + query);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(0) != null) { result.Add(Convert.ToString(reader.GetString(0))); } else { result.Add("0"); }
                    if (reader.GetString(1) != null) { result.Add(Convert.ToString(reader.GetString(1))); } else { result.Add("0"); }
                }
                Debug.WriteLine(" ----   RESULT QERY statament FALLAS_ASISTENCIAS: ");
                foreach (var item in result)
                {
                    Debug.WriteLine(" ---- " + item.ToString());
                }


            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(" ----   CATCH QERY statament FALLAS_ASISTENCIAS: " + ex);
            }

            this.CloseConnection();

            return result;

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
            MySqlCommand cmd = new MySqlCommand
            {
                //Assign the query using CommandText
                CommandText = query,
                //Assign the connection using Connection
                Connection = connection
            };

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

        public string GetPeriodo() {
            return "Periodo Académico";
        }
        
    }
}
