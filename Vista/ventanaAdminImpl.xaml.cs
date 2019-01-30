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
    /// Lógica de interacción para ventanaAdminImpl.xaml
    /// </summary>
    public partial class VentanaAdminImpl : Window
    {
        private Entorno entorno;
        private static VentanaAdminImpl instance;

        private string nombreIn;
        private string siglaIn;
        private UInt32 cantidadIn;
        private bool nuevoImpl;

        private VentanaAdminImpl()
        {
            InitializeComponent();
            Limpiar();
        }

        public static VentanaAdminImpl GetInstance()
        {
            if (instance == null)
                instance = new VentanaAdminImpl();

            return instance;
        }

        private void Limpiar()
        {
            nombreIn = "";
            siglaIn = "";
            cantidadIn = 0;
            nuevoImpl = false;

            chbox_nuevo.IsChecked = true;
            input_sigla_nueva.Text = "";
            input_nombre.Text = "";
            input_sigla.Text = "";

            chbox_eliminar.IsChecked = true;
            cmbox_Sigla.SelectedValue = false;

            btn1.IsEnabled = false;
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {            

            Limpiar();
            Ocultar();
        }

        private void Ocultar() {
            TablaImplementos tablaImpl = TablaImplementos.GetInstance();
            tablaImpl.Show();
            this.Hide();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            Ocultar();
        }

        private void Cmbox_Sigla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Chbox_Click_nuevo(object sender, RoutedEventArgs e)
        {
            nuevoImpl = true;
            chbox_eliminar.IsEnabled = false;
        }

        private void Chbox_Click_eliminar(object sender, RoutedEventArgs e)
        {
            nuevoImpl = false;
            chbox_nuevo.IsEnabled = false;
            //cmbox_Sigla.ItemsSource = entorno.Implementos_disponiblesSigla();
        }
    }
}
