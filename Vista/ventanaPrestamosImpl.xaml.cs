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
        private UInt64 codigoEs;
        private UInt32 cantidad;
        private string observacion;

        private VentanaPrestamosImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

            listaDisponibles = new List<string>();
            listaCantDisponibles = new List<int>();            

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
            List<string> lista = entorno.Implementos_disponiblesSigla();
            if (lista[0]==entorno.ERRORSQL)
            {
                cmbox_Sigla.ItemsSource = new List<string>();
                if(this.IsVisible) MessageBox.Show(lista[1]);
            }
            else
            {
                cmbox_Sigla.ItemsSource = lista;
            }
        }

        private void ActualizarCmbxDisponibles()
        {
            cmbox_Cant.ItemsSource = Listar(entorno.Implementos_dispCabtidad_sigla(siglaSelect));
        }

        private void ActualizarCmbxDevolucion()
        {
            cmbox_Cant.ItemsSource = Listar(entorno.Implementos_dispPrestamo_sigla(siglaSelect, codigoEs));
        }

        /// <summary>
        /// Conla cantidad de disponibles de una Impl deportivo por su sigla, genera una lista de 1 hasta esa cantidad
        /// </summary>
        /// <param name="lista">lista de resultado query</param>
        /// <returns></returns>
        private List<int> Listar(List<string> lista)
        {
            List<int> sucesion = new List<int>();

            int maximo = Convert.ToInt32(lista[0]);

            for (int i = 1; i <= maximo; i++)
            {
                sucesion.Add(i);
            }

            return sucesion;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            bool esDevolver = chbox.IsChecked ?? false;
            observacion = obs1.Text;
            string mensaje = ""; // mensaje para mostrar al terminar o fallar

            if (!esDevolver) // caso de prestamo
            {
                mensaje = entorno.AddImplementoPrestamo(siglaSelect, codigoEs, cantidad, observacion);
            }
            else
            {
                mensaje = entorno.DevuelveImplementoPrestamo(siglaSelect, codigoEs, cantidad);
            }

            Ocultar();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
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
            siglaSelect = cmbox_Sigla.SelectedValue.ToString();
            chbox.IsEnabled = true;
        }

        private void Cmbox_Cant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chbox.IsEnabled = false;

            cantidad = Convert.ToUInt32(cmbox_Cant.SelectedValue.ToString());
        }

        private void Limpiar()
        {
            siglaSelect = "";
            codigoEs = 0;
            cantidad = 0;
            observacion = "";

            codigo.Text = "";
            cmbox_Sigla.SelectedValue = null;
            cmbox_Cant.SelectedValue = null;

            codigo.IsEnabled = true;
            cmbox_Cant.IsEnabled = false;
            cmbox_Sigla.IsEnabled = false;

            chbox.IsEnabled = false;
            chbox.IsChecked = false;

            obs1.Text = "";
            obs1.IsEnabled = false;

            codigo.Focus();
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            // capturar datos y validar

            if (codigo.Text == "" || !UInt64.TryParse(codigo.Text, out UInt64 abc))
            {
                MessageBox.Show("El código no es valido!");
            }
            else
            {
                codigoEs = Convert.ToUInt64(codigo.Text);

                codigo.IsEnabled = false;
                ActualizarCmbxSiglas();
                cmbox_Sigla.IsEnabled = true;
            }
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Chbox_Checked(object sender, RoutedEventArgs e)
        {
            obs1.IsEnabled = true;
            cmbox_Cant.IsEnabled = true;

            bool esDevolver = chbox.IsChecked ?? false;

            if (!esDevolver) // caso prestamo
            {
                ActualizarCmbxDisponibles();
            }
            else
            {
                ActualizarCmbxDevolucion();
            }
        }
    }
}
