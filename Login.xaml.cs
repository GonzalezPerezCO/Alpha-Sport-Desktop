using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Deportes_WPF.Controller;
using MySql.Data.MySqlClient;

namespace Deportes_WPF
{
    
    public partial class Login : Window
    {
        private Entorno entorno;

        public Login()
        {
            InitializeComponent();
            entorno = new Entorno();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            /*server = "estudiantes.is.escuelaing.edu.co";
            database = "deportes";
            user = "deportes";
            password = "deportes20182";
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);

           
            if (txt1.Text == "" || txt2.Password.ToString() == "") {
                MessageBox.Show("Llene todos los campos");
            }
            else {

                try
                {
                    connection.Open();                   

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + connectionString);
                }

                query = "select email from tadmin where email= '" + txt1.Text + "' and password= '" + txt2.Password.ToString() + "';";
                cmd = new MySqlCommand(query, connection);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //if (Convert.ToString(reader["email"]) != txt1.Text) MessageBox.Show("Parametros incorrectos");

                    Window main = new MainWindow();

                    this.Hide();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado!");
                }

                connection.Close();
            }
            
            */
            string email = txt1.Text;
            string password = txt2.Password.ToString();

            if (email == "" || password == "")
            {
                MessageBox.Show("Llene todos los campos");
            }
            else
            {
                if (entorno.login(email, password))
                {
                    //if (Convert.ToString(reader["email"]) != txt1.Text) MessageBox.Show("Parametros incorrectos");

                    Window main = new MainWindow();

                    this.Hide();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado!");
                }
                
            }
        }
        
       
    }
}
