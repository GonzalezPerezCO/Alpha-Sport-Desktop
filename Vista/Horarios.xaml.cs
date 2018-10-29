using Deportes_WPF.Controller;
using System;
using System.Collections.Generic;
using System.Data;
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
        private string codigo;

        public Horarios()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }            
             
        private void limpiar() {
            txt3.Text = "";
            txt4.Content = "Nombre y Apellido";
            txt5.Content = "Desconocido";
            txt6.Content = "0";
            txt7.Content = "0";
        }

        private void click_bt2(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            tabla.Show();
            this.Hide();
        }

        private void click_bt1(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void click_bt3(object sender, RoutedEventArgs e)
        {
            codigo = txt3.Text;

            if (codigo == "")
            {
                MessageBox.Show("No hay codigo del estudiante");
            }
            else
            {
                //0: nombre, 1: carrera, 2: semestre, 3: fallas, 4: codigo, 5: dia1, 6: dia2, 7: dia3, 8: hora1, 9: hora2, 10: hora3
                List<string> lista = entorno.asistencia(Convert.ToInt32(codigo));

                if (lista.Capacity > 0)
                {
                    txt4.Content = lista[0];
                    txt5.Content = lista[1];
                    txt6.Content = lista[2];
                    txt7.Content = lista[3];                    
                }
            }
        }
    }
}
