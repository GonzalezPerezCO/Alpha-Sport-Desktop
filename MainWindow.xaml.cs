using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Deportes_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;


        string query;
        MySqlCommand cmd;
        MySqlDataReader reader;


        public MainWindow()
        {
            InitializeComponent();

            mostrarTabla();
        }


        public void mostrarTabla() {



            server = "estudiantes.is.escuelaing.edu.co";
            database = "deportes";
            user = "deportes";
            password = "deportes20182";
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + connectionString);
            }

            query = "select nombre, apellido, codigo, carrera, semestre from testudiantes";
            cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dtgrid1.ItemsSource = dt.DefaultView;
            }
            else {
                MessageBox.Show(reader.ToString());
            }


            connection.Close();
            




           

        }

      
    }
}
