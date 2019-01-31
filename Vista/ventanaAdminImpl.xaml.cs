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

            chbox_nuevo.IsEnabled = true;
            chbox_nuevo.IsChecked = false;
            input_sigla_nueva.Text = "";
            input_nombre.Text = "";
            input_cantidad.Text = "";

            chbox_eliminar.IsEnabled = true;
            chbox_eliminar.IsChecked = false;
            cmbox_Sigla.SelectedValue = false;

            btn1.IsEnabled = false;
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

            if (nuevoImpl) // caso impl nuevo
            {                
                //capturar datos
                if (input_sigla_nueva.Text == "" || input_nombre.Text == "" || UInt32.TryParse(input_cantidad.Text, out UInt32 abc))
                {
                    MessageBox.Show("Hay campos incorrectos, llene correctamente todos los campos!");
                }
                else
                {
                    nombreIn = input_nombre.Text.TrimStart().TrimEnd(); ;
                    siglaIn = input_sigla_nueva.Text.TrimStart().TrimEnd(); ;
                    cantidadIn = Convert.ToUInt32(input_cantidad.Text);

                    List<string> lista = entorno.Nuevo_Implemento(nombreIn, siglaIn, cantidadIn);

                    if (lista.Count != 0 && (lista[0] == entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
                    {
                        MessageBox.Show(lista[1]);
                    }
                    else
                    {
                        MessageBox.Show("Nuevo Impl Deportivo agregado correctamente!";
                    }
                    
                }
            }
            else // caso eliminar impl
            {
                // List<string> lista = entorno.Implementos_disponiblesSigla();
            }

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
            EstadoCamposEliminar(false);
        }

        private void Chbox_Click_eliminar(object sender, RoutedEventArgs e)
        {
            nuevoImpl = false;
            EstadoCamposNuevo(false);
            //cmbox_Sigla.ItemsSource = entorno.Implementos_disponiblesSigla();
        }

        private void EstadoCamposNuevo(bool estado)
        {
            chbox_nuevo.IsEnabled = estado;
            input_sigla_nueva.IsEnabled = estado;
            input_nombre.IsEnabled = estado;
            input_cantidad.IsEnabled = estado;
        }

        private void EstadoCamposEliminar(bool estado)
        {
            chbox_eliminar.IsEnabled = estado;
            cmbox_Sigla.IsEnabled = estado;
        }
    }
}
