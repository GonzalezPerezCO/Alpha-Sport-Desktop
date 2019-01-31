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
    /// Lógica de interacción para VentanaAddDelImpl.xaml
    /// </summary>
    public partial class VentanaAddDelImpl : Window
    {
        private Entorno entorno;
        private static VentanaAddDelImpl instance;

        private string nombreIn;
        private string siglaIn;
        private UInt32 cantidadIn;
        private bool nuevoImpl;

        private VentanaAddDelImpl()
        {
            InitializeComponent();
            Limpiar();
        }

        public static VentanaAddDelImpl GetInstance()
        {
            if (instance == null)
                instance = new VentanaAddDelImpl();

            return instance;
        }

        private void Limpiar()
        {
            nombreIn = "";
            siglaIn = "";
            cantidadIn = 0;
            nuevoImpl = false;

            // para nuevo
            chbox_nuevo.IsEnabled = true;
            chbox_nuevo.IsChecked = false;

            input_sigla_nueva.Text = "";
            input_nombre.Text = "";
            input_cantidad.Text = "";

            input_sigla_nueva.IsEnabled = false;
            input_nombre.IsEnabled = false;
            input_cantidad.IsEnabled = false;

            // para eliminar
            chbox_eliminar.IsEnabled = true;
            chbox_eliminar.IsChecked = false;

            cmbox_Sigla.IsEnabled = false;
            cmbox_Sigla.SelectedValue = null;

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
                if (input_sigla_nueva.Text == "" || input_nombre.Text == "" || !UInt32.TryParse(input_cantidad.Text, out UInt32 abc))
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
                        Limpiar();
                        MessageBox.Show("Nuevo Impl Deportivo agregado correctamente!");
                    }

                }
            }
            else // caso eliminar impl
            {
                List<string> eliminacion = entorno.Eliminar_Implemento(cmbox_Sigla.SelectedValue.ToString());

                if (eliminacion.Count != 0 && (eliminacion[0] == entorno.ERRORSQL || eliminacion[0] == entorno.INFOSQL))
                {
                    MessageBox.Show(eliminacion[1]);
                }
                else
                {
                    Limpiar();
                    MessageBox.Show("Impl Deportivo borrado correctamente!");
                }

            }
        }

        private void ActualizarCmboxSiglas(List<string> lista)
        {
            cmbox_Sigla.ItemsSource = lista;
        }

        private void Ocultar()
        {
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
            if (this.IsEnabled) siglaIn = cmbox_Sigla.SelectedValue.ToString();
        }

        private void Chbox_Click_nuevo(object sender, RoutedEventArgs e)
        {
            nuevoImpl = true;
            chbox_nuevo.IsEnabled = false;
            EstadoCamposEliminar(false);

            input_nombre.IsEnabled = true;
            input_sigla_nueva.IsEnabled = true;
            input_cantidad.IsEnabled = true;

            btn1.IsEnabled = true;
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

        private void Chbox_Click_eliminar(object sender, RoutedEventArgs e)
        {
            nuevoImpl = false;
            EstadoCamposNuevo(false);

            cmbox_Sigla.IsEnabled = true;

            // para actualizar 
            List<string> lista = entorno.Implementos_disponiblesSigla();

            if (lista.Count != 0 && (lista[0] == entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
            {
                MessageBox.Show(lista[1]);
            }
            else
            {
                ActualizarCmboxSiglas(lista); // lleno el combobox con siglas
            }
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
