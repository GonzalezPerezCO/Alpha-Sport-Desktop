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
    /// Lógica de interacción para ventanaPrestamosImpl.xaml
    /// </summary>
    public partial class VentanaPrestamosImpl : Window
    {
        private Entorno entorno;
        private static VentanaPrestamosImpl instance = null;

        private List<string> listaDisponibles; // lista de disponibles por sigla
        private List<int> listaCantDisponibles; // lista de disponibles de una sigla

        private string siglaSelect;

        private VentanaPrestamosImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

            listaDisponibles = new List<string>();
            listaCantDisponibles = new List<string>();

            siglaSelect = "";

            Limpiar();

            ActualizarCmbxSiglas();
        }

        public static VentanaPrestamosImpl GetInstance()
        {
            if (instance == null)
                instance = new VentanaPrestamosImpl();

            return instance;
        }

        private void Ocultar()
        {
            PrestamosImpl prestamos = PrestamosImpl.GetInstance();
            prestamos.Show();
            this.Hide();
        }  

        private void ActualizarCmbxSiglas()
        {
            cmbox_Sigla.ItemsSource = entorno.Implementos_disponiblesSigla();
        }

        private void ActualizarCmbxDisponibles()
        {
            cmbox_Cant.ItemsSource = Listar(entorno.Implementos_dispCabtidad_sigla(siglaSelect));
        }

        private List<string> Listar(List<string> lista)
        {
            List<string> sucesion = new List<string>();

            int maximo = Convert.ToInt32(lista[0]);

            for (int i = 1; i <= maximo; i++)
            {
                sucesion.Add(i);
            }

            return sucesion;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Ocultar();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Limpiar();
            ActualizarCmbxSiglas();
        }

        private void Cmbox_Sigla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            siglaSelect = cmbox_Sigla.ToString();
            ActualizarCmbxDisponibles();
            cmbox_Cant.IsEnabled = true;
        }

        private void Limpiar()
        {
            codigo.Text = "";
            cmbox_Sigla.SelectedValue = null;
            cmbox_Cant.SelectedValue = null;

            codigo.Focus();
        }
    }
}
