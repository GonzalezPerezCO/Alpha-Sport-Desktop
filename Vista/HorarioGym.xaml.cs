using AlphaSport.Controller;
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

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para HorarioGym.xaml
    /// </summary>
    public partial class HorarioGym : Window
    {
        private Entorno entorno;

        public HorarioGym()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            MostrarTabla();
        }

        public void MostrarTabla()
        {
            System.Data.DataTable dt = entorno.TablaHorarioGym("LUNES");
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

        private void Bt5_Click(object sender, RoutedEventArgs e)
        {
            Window main = new Main();

            main.Show();
            this.Hide();
        }

        private void Click_bt1(object sender, RoutedEventArgs e)
        {

        }
    }
}
