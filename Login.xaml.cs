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
    
    public  partial class Login : Window
    {
        private Entorno entorno;

        public Login()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance(); ;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {            
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

                    Window main = new TablaInscritos();

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
