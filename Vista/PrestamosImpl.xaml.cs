using AlphaSport.Controller;
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
    /// Lógica de interacción para PrestamosImpl.xaml
    /// </summary>
    public partial class PrestamosImpl : Window
    {

        private Entorno entorno;
        private static PrestamosImpl instance;


        private PrestamosImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;

            MostrarTabla();
        }

    public static PrestamosImpl GetInstance()
        {
            if (instance == null)
                instance = new PrestamosImpl();

            return instance;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Bt1_Click(object sender, RoutedEventArgs e)
        {
            VentanaPrestamosImpl ventaPrestamos = VentanaPrestamosImpl.GetInstance();
            ventaPrestamos.Show();
            this.Hide();
        }

        private void Bt5_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void Bt2_Click(object sender, RoutedEventArgs e)
        {
            TablaImplementos implementos = new TablaImplementos();
            implementos.Show();
            this.Hide();

        }

        private void MostrarTabla()
        {
            DataTable dt = entorno.TablaPrestamos();
            dtgrid1.ItemsSource = dt.DefaultView;
        }

        private void Bt4_Click(object sender, RoutedEventArgs e)
        {
            MostrarTabla();
        }
    }
}
