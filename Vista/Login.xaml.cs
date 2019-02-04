using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AlphaSport.Vista;
using AlphaSport.Controller;

namespace AlphaSport
{
    
    public  partial class Login : Window
    {
        private Entorno entorno;

        public Login()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance(); ;
            ////lab1.Content = entorno.PROYECTO;
            lab2.Content = entorno.PERIODO;
            txt1.Focus();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {            
            string email = txt1.Text;
            string password = txt2.Password.ToString();

            if (email == "" || password == "")
            {
                MessageBox.Show("Llene todos los campos");
            }
            else
            {
                if (entorno.Login(email, password))
                {
                    entorno.InactividadAppWPF(); // inactividad programada para Restart app

                    Window ventana = new Main();

                    ventana.Show();
                    this.Hide();
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

        private void Click_creditos(object sender, MouseButtonEventArgs e)
        {
            VentanaCreditos creditos = new VentanaCreditos();
            creditos.Show();
            this.Hide();
        }
    }
}