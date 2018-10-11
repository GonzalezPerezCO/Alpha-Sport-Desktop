using Deportes_WPF.Controller;
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
using System.Windows.Shapes;

namespace Deportes_WPF.Vista
{
    /// <summary>
    /// Lógica de interacción para Cupos.xaml
    /// </summary>
    public partial class Cupos : Window
    {
        private Entorno entorno;

        public Cupos()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            mostrarTabla();

        }

        public void mostrarTabla()
        {
            DataTable dt = entorno.mostrarTabla();

            dtgrid1.ItemsSource = dt.DefaultView;

        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            mostrarTabla();
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
