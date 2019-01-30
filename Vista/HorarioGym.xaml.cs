using AlphaSport.Controller;
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

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para HorarioGym.xaml
    /// </summary>
    public partial class HorarioGym : Window
    {
        private Entorno entorno;

        private string diaActual;
        private List<string> datosFechaActual = new List<string>();

        public HorarioGym()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
           
            cmbx1.ItemsSource = entorno.DIAS;

            datosFechaActual = entorno.CalcularHoy();
            diaActual = datosFechaActual[0];

            cmbx1.SelectedItem = diaActual;

            MostrarTabla();
        }

        public void MostrarTabla()
        {   
            DataTable dt = entorno.TablaHorarioGym(cmbx1.SelectedItem.ToString());
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
            TablaInscritos tablaIns = new TablaInscritos();

            tablaIns.Show();
            this.Hide();
        }

        private void Cmbx1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarTabla();
        }
    }
}
