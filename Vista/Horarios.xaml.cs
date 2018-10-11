using Deportes_WPF.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para Horarios.xaml
    /// </summary>
    public partial class Horarios : Window
    {
        private Entorno entorno;

        public Horarios()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();
            this.Hide();
            tabla.Show();
        }

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            string codigo = txt3.Text;

            if (codigo == "")
            {
                MessageBox.Show("No hay codigo del estudiante");
            }
            else
            {
                //0: nombre, 1:codigo, 2:dia1, 3:dia2, 4:dia3, 5:hora1, 6:hora2, 7:hora3
                List<string> lista = entorno.horario(codigo);

                if (lista.Capacity > 0)
                // nombre, apellido, carrera, semestre, dia1, dia2, dia3, hora1, hora2, hora3
                {
                    txt4.Content = lista[0] + " " +lista[1];
                    txt5.Content = lista[2];
                    txt6.Content = lista[3];
                    txt7.Content = lista[4];
                    txt8.Content = lista[5];
                    txt9.Content = lista[6];
                    txt10.Content = lista[7];
                    txt11.Content = lista[8];
                    txt12.Content = lista[9];

                }
                else
                {                   
                    MessageBox.Show("Estudiante no encontrado!");

                }

            }

        }
    }
}
