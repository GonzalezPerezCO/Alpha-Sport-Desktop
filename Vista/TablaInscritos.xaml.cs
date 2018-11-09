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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlphaSport.Vista;
using Deportes_WPF.Controller;
using Deportes_WPF.Vista;

namespace Deportes_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class TablaInscritos : Window
    {
        private Entorno entorno;

        public TablaInscritos()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            mostrarTabla();
        }


        public void mostrarTabla()
        {
           DataTable dt = entorno.tablaInscritos();           
           dtgrid1.ItemsSource = dt.DefaultView;           
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            Window asistencia = new Asistencia();

            asistencia.Show();
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

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            Window cupos = new Cupos();

            cupos.Show();
            this.Hide();            
        }              

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            Window agregar = new agregarEstud();
                        
            agregar.Show();
            this.Hide();
        }

        private void bt4_Click(object sender, RoutedEventArgs e)
        {
            Window horario = new Horarios();
            
            horario.Show();
            this.Hide();
        }

        private void bt5_Click(object sender, RoutedEventArgs e)
        {
            Window main = new Main();

            main.Show();
            this.Hide();
        }
    }
}
