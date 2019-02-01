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

            nuevoImpl = false;
            codigo = 0;
            selec = false;

            chbx_nuevo.IsChecked = false;
            chbx_eliminar.IsChecked = false;

            cmbox1.SelectedValue = null;

            text1.Focus();
        }

        private void ActualizarCmbx()
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

        private bool CapturarDatos()
        {
            bool result = false;

            if (text1.Text == "" || !UInt64.TryParse(text1.Text, out UInt64 abc))
            {
                MessageBox.Show("Ingrese un número de carnet valido!");
            }
            else
            {
                codigo = Convert.ToUInt64(text1.Text);
                
                if(selec && !nuevoImpl) ActualizarCmbx(); // seleccionado chbox y es Eliminar

                result = true;
            }

            return result;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            bool result = CapturarDatos();          

            if (result)
            {
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("error");
            }
            //Limpiar();
            //Ocultar();
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
            this.Hide();
        }

        private void Chbox_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("<<< Chbox_Click ");
            CheckBox chk = (CheckBox)sender;
            string name = chk.Name;

            if (name == "chbx_nuevo")
            {
                nuevoImpl = chbx_nuevo.IsChecked ?? false;
                chbx_eliminar.IsChecked = !nuevoImpl;
                selec = true;

            }
            else if (name == "chbx_eliminar")
            {
                nuevoImpl = chbx_eliminar.IsChecked ?? false;
                chbx_nuevo.IsChecked = !nuevoImpl;
                selec = true;
            }
            else
            {
                nuevoImpl = false;
                chbx_nuevo.IsChecked = false;
                chbx_eliminar.IsChecked = false;
                selec = false;
            }
        }
    }
}
