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

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para TablaImplementos.xaml
    /// </summary>
    public partial class TablaImplementos : Window
    {
        private Entorno entorno;

        public TablaImplementos()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            mostrarTabla();
        }

        public void mostrarTabla()
        {
            DataTable dt = entorno.tablaImplementos();
            dtgrid1.ItemsSource = dt.DefaultView;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void bt5_Click(object sender, RoutedEventArgs e)
        {
            Window main = new Main();

            main.Show();
            this.Hide();
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            Window prestar = new Prestar();

            prestar.Show();
            this.Hide();
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            Window prestados = new ConsultarPrestamos();

            prestados.Show();
            this.Hide();
        }

        private void Bt3_Click(object sender, RoutedEventArgs e)
        {
            Window casilleros = Casilleros.GetInstance();
            casilleros.Show();
            this.Hide();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
