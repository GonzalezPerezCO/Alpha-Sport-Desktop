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

        private VentanaAdminImpl()
        {
            InitializeComponent();
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

            chbox_eliminar.IsChecked = true;
            input_sigla_nueva.Text = "";
            input_nombre.Text = "";
            input_sigla.Text = "";

            chbox_nuevo.IsChecked = true;
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

        private void Chbox_Click(object sender, RoutedEventArgs e)
        {
            cmbox_Sigla.ItemsSource = entorno.Implementos_disponiblesSigla();
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
    }
}
