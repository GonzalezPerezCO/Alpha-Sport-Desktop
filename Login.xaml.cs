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

namespace Deportes_WPF
{
    
    public  partial class Login : Window
    {
        private Entorno entorno;

        public Login()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance(); ;
            lab1.Content = entorno.PROYECTO;
            txt1.Focus();
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

                    Window tabla = new TablaInscritos();

                    this.Hide();
                    tabla.Show();
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado!");
                }
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
