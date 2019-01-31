using AlphaSport.Controller;
using System;
using System.Collections.Generic;
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
            Debug.WriteLine("******* 1");
        }

        public static VentanaAddDelImpl GetInstance()
        {
            Debug.WriteLine("******* 2");
            if (instance == null)
                instance = new VentanaAddDelImpl();

            return instance;
        }

        private void Limpiar()
        {
            Debug.WriteLine("******* 3");
            nombreIn = "";
            siglaIn = "";
            cantidadIn = 0;
            nuevoImpl = false;

            // para nuevo
            chbox_nuevo.IsEnabled = true;
            chbox_nuevo.IsChecked = false;

            inputSiglaNueva.Text = "";
            inputNombre.Text = "";
            inputCantidad.Text = "";

            inputSiglaNueva.IsEnabled = false;
            inputNombre.IsEnabled = false;
            inputCantidad.IsEnabled = false;

            // para eliminar
            chbox_eliminar.IsEnabled = true;
            chbox_eliminar.IsChecked = false;

            cmbox_Sigla.IsEnabled = false;
            cmbox_Sigla.SelectedValue = null;

            btn1.IsEnabled = false;
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("******* 4");
            Limpiar();
        }


        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("******* 5");

            if (nuevoImpl) // caso impl nuevo
            {
                //capturar datos
                if (inputSiglaNueva.Text == "" || inputNombre.Text == "" || !UInt32.TryParse(inputCantidad.Text, out UInt32 abc))
                {
                    MessageBox.Show("Hay campos incorrectos, llene correctamente todos los campos!");
                }
                else
                {
                    nombreIn = inputNombre.Text.TrimStart().TrimEnd();
                    siglaIn = inputSiglaNueva.Text.TrimStart().TrimEnd();

                    cantidadIn = Convert.ToUInt32(inputCantidad.Text);

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
            Debug.WriteLine("******* 6");
            cmbox_Sigla.ItemsSource = lista;
        }

        private void Ocultar()
        {
            Debug.WriteLine("******* 7");
            TablaImplementos tablaImpl = TablaImplementos.GetInstance();
            tablaImpl.Show();
            this.Hide();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("******* 8");
            Limpiar();
            Ocultar();
        }

        private void Cmbox_Sigla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("******* 9");
            //if (this.IsEnabled)
                siglaIn = cmbox_Sigla.SelectedValue.ToString();
        }

        private void Chbox_Click_nuevo(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("******* 10");
            nuevoImpl = true;
            chbox_nuevo.IsEnabled = false;
            EstadoCamposEliminar(false);

            inputNombre.IsEnabled = true;
            inputSiglaNueva.IsEnabled = true;
            inputCantidad.IsEnabled = true;

            btn1.IsEnabled = true;
        }


        private void EstadoCamposNuevo(bool estado)
        {
            Debug.WriteLine("******* 11");
            chbox_nuevo.IsEnabled = estado;
            inputSiglaNueva.IsEnabled = estado;
            inputNombre.IsEnabled = estado;
            inputCantidad.IsEnabled = estado;
        }

        private void EstadoCamposEliminar(bool estado)
        {
            Debug.WriteLine("******* 12");
            chbox_eliminar.IsEnabled = estado;
            cmbox_Sigla.IsEnabled = estado;
        }

        private void Chbox_Click_eliminar(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("******* 13");
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
