using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace Deportes_WPF
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;
        private string connectionString;

        public Login()
        {
            InitializeComponent();

            server = "estudiantes.is.escuelaing.edu.co";
            database = "deportes";
            uid = "deportes";
            password = "deportes20182";
            port = "3306";

            connectionString = "Server=" + server + ";" + "Port="+ port +";"+"Database=" +
            database + ";" + "Uid=" + uid + ";" + "Password=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private void conexion()
        {
            try
            {
                connection.Open();

                MessageBox.Show("mensaje nuevo");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(connectionString);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            conexion();

        }
    }
}
