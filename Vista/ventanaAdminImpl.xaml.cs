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
    /// Lógica de interacción para ventanaAdminImpl.xaml
    /// </summary>
    public partial class VentanaAdminImpl : Window
    {
        private Entorno entorno;
        private static VentanaAdminImpl instance = null;

        private bool nuevoImpl; // si es o no un nuevo prestamo
        private UInt64 codigo; 
        private bool selec; // alguno chbox esta seleccionado
        private string nombre;
        private string sigla;
        private UInt32 cantidad;

        public VentanaAdminImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

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
            text1.Text = "";

            nuevoImpl = true;
            codigo = 0;
            selec = new bool();
            nombre = "";
            sigla = "";
            cantidad = 0;

            text1.Text = "";
            text2.Text = "";
            text3.Text = "";

            chbx_nuevo.IsChecked = false;
            chbx_eliminar.IsChecked = false;

            cmbox1.SelectedValue = null;

            EstadoBotonesAlgunos(); // deshabilita algunos elementos
            
            chbx_nuevo.IsEnabled = true;
            chbx_eliminar.IsEnabled = true;

            text1.Focus();
        }

        private void ActualizarCmbx()
        {
            Debug.WriteLine("<<< ActualizarCmbox: s y nuevo = "+selec+ " y "+ nuevoImpl);
            if (selec && !nuevoImpl) // seleccionado chbox y es Eliminar
            {
                List<string> lista = entorno.Implementos_disponiblesSigla();

                if (lista.Count != 0 && (lista[0] == entorno.INFOSQL || lista[0] == entorno.INFOSQL)) // cuando si esta pero no tiene casillero
                {
                    MessageBox.Show(lista[1]);
                }
                else
                {
                    cmbox1.ItemsSource = lista;
                }
            }
            
        }

        private void EstadoBotonesAlgunos()
        {
            text1.IsEnabled = false;
            text2.IsEnabled = false;
            text3.IsEnabled = false;
            cmbox1.IsEnabled = false;
            btn1.IsEnabled = false;
        }

        private bool CapturarDatos()
        {
            Debug.WriteLine("<<< CAPTURA ");
            bool result = false;

            if (selec)
            {
                if (selec && nuevoImpl) // el unico que campura datos es para Nuevo Implemento
                {
                    if (text1.Text == "" || text2.Text == "" || !UInt64.TryParse(text2.Text, out UInt64 abc) || text3.Text == "")
                    {
                        MessageBox.Show("Llene todos los campos correctamente para poder continuar.!");
                    }
                    else
                    {
                        nombre = text3.Text;
                        sigla = text1.Text;
                        cantidad = Convert.ToUInt32(text2.Text);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccion si desea Nuevo Implemento Deportivo o Eliminar un Implemento Deportivo.!");
            }

            return result;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            bool result = CapturarDatos();
            List<string> lista = new List<string>();

            if (result)
            {
                lista = entorno.Nuevo_Implemento(nombre, sigla, cantidad);

                EstadoBotonesAlgunos(); // primero para evitar el Listener de Selection_Change_Cmbx
                Limpiar();
            }
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            Ocultar();
        }

        private void Ocultar()
        {
            TablaImplementos impl = TablaImplementos.GetInstance();
            impl.Show();
            impl.MostrarTabla();
            this.Hide();
        }

        private void Chbox_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("<<< Chbox_Click ");
            CheckBox chk = (CheckBox)sender;
            string name = chk.Name;

            if (name == "chbx_nuevo")
            {
                Debug.WriteLine("<<< Chbox_Click NUEVO ");
                nuevoImpl = true;

                chbx_eliminar.IsChecked = false;
                chbx_eliminar.IsEnabled = false;

                selec = true;

            }
            else if (name == "chbx_eliminar")
            {
                Debug.WriteLine("<<< Chbox_Click ELIMINAR");
                nuevoImpl = false;

                chbx_nuevo.IsChecked = false;
                chbx_nuevo.IsEnabled = false;

                cmbox1.IsEnabled = true;

                selec = true;

                ActualizarCmbx();
            }
            else
            {
                Limpiar(); // limpia todo
            }
        }

        private void Cmbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbox1.IsVisible)
            {
                sigla = cmbox1.SelectedValue.ToString();
            }
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
