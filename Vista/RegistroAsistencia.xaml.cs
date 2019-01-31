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
    /// Lógica de interacción para RegistroAsistencia.xaml
    /// </summary>
    public partial class RegistroAsistencia : Window
    {
        private Entorno entorno;


        public RegistroAsistencia()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            MostrarTabla();
        }

        private void Bt7_Click(object sender, RoutedEventArgs e)
        {
            MostrarTabla();
        }

        public void MostrarTabla()
        {
            DataTable dt = entorno.TablaFallasAsistencia();
            dtgrid1.ItemsSource = dt.DefaultView;
        }

        private void Click_bt2(object sender, RoutedEventArgs e)
        {
            TablaInscritos inscritos = new TablaInscritos();

            inscritos.Show();
            this.Hide();
        }

        private void Bt5_Click(object sender, RoutedEventArgs e)
        {
            Window main = new Main();

            main.Show();
            this.Hide();
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
