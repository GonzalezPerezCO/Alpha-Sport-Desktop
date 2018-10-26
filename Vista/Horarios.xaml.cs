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
                List<string> lista = entorno.datos(codigo);
                List<string> listaH = entorno.horario(codigo);
                Debug.WriteLine(">>>>>>>> Horario: ");

                //DataTable dt = entorno.mostrarHorario(Int32.Parse(txt3.Text));

                //dtgrid1.ItemsSource = dt.DefaultView;

            }

        }
    }
}
