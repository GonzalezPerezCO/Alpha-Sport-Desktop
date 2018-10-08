using Deportes_WPF.Controller;
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

namespace Deportes_WPF.Vista
{
    /// <summary>
    /// Lógica de interacción para Asistencia.xaml
    /// </summary>
    public partial class Asistencia : Window
    {

        private Entorno entorno;

        public Asistencia()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            bt4.IsEnabled = false;
            bt5.IsEnabled = false;
            lab6.Content = "Ingrese el código del estudiante y realice la busqueda";
            txt1.Focus();
        }       

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            this.Hide();
            tabla.Show();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            lab5.Content = "nombre y apellido";
            txt1.Text = "";
            txt1.Focus();
        }

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            string codigo = txt1.Text;

            if (codigo == "")
            {
                MessageBox.Show("No hay codigo del estudiante");
            }
            else
            {

                List<string> lista = entorno.asistencia(codigo);

                if (lista.Capacity > 0)
                {
                    lab5.Content = lista[0];
                    
                }
                else
                {
                    lab5.Content = "No encontrado";
                    lab6.Content = "Estudiante no encontrado!";
                    
                }

            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
