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
    /// Lógica de interacción para ventanaPrestamosImpl.xaml
    /// </summary>
    public partial class VentanaPrestamosImpl : Window
    {
        private Entorno entorno;
        private static VentanaPrestamosImpl instance = null;

        private string siglaSelect;
        private UInt64 codigoEstu;
        private UInt32 cantidad;
        private string observacion;

        private bool valor_pres; // chbox prestamo
        private bool valor_dev; // chbox devolucion

        private VentanaPrestamosImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();                       

            Limpiar();
            Debug.WriteLine("<<< CONSTRUCTOR");
        }

        public static VentanaPrestamosImpl GetInstance()
        {
            if (instance == null)
                instance = new VentanaPrestamosImpl();
            Debug.WriteLine("<<< INSTANCIA");
            return instance;
        }

        private void Ocultar()
        {
            Debug.WriteLine("<<< OCULTAR");
            PrestamosImpl prestamos = PrestamosImpl.GetInstance();
            prestamos.Show();
            this.Hide();            
        }

        private void ActualizarCmbxSiglas()
        {
            Debug.WriteLine("<<< ActualizarCmbxSiglas ");
            List<string> lista = new List<string>();

            if (valor_pres) // caso prestamo
            {
                lista = entorno.Implementos_disponiblesSigla();

                if (lista[0] == entorno.ERRORSQL)
                {
                    cmbox_Sigla.ItemsSource = new List<string>(); ;
                    if (this.IsVisible) MessageBox.Show(lista[1]);                    
                }
                else
                {
                    cmbox_Sigla.ItemsSource = lista;

                }
            }
            else  // caso devolucion
            {
                lista = entorno.Implementos_disponiblesCodigo(codigoEstu);

                if (lista[0] == entorno.ERRORSQL)
                {
                    cmbox_Sigla.ItemsSource = new List<string>(); ;
                    if (this.IsVisible) MessageBox.Show(lista[1]);
                }
                else
                {
                    cmbox_Sigla.ItemsSource = lista;
                }
            }            
        }       

        /// <summary>
        /// Conla cantidad de disponibles de una Impl deportivo por su sigla, genera una lista de 1 hasta esa cantidad
        /// </summary>
        /// <param name="lista">lista de resultado query</param>
        /// <returns></returns>
        private List<int> Listar(List<string> lista)
        {
            Debug.WriteLine("<<< LISTAR ");
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
            Debug.WriteLine("<<< Btn1_Click ");
            List<string> lista = new List<string>();

            if (obs1.Text == null || obs1.Text == "") obs1.Text = "nada";

            observacion = obs1.Text;

            cantidad = Convert.ToUInt32(cmbox_Cant.SelectedValue.ToString());

            if (valor_pres) // caso prestamo
            {
                lista = entorno.AddImplementoPrestamo(siglaSelect, codigoEstu, cantidad, observacion);

                if (lista.Count != 0 && lista[0] == entorno.ERRORSQL)
                {
                    if (this.IsVisible) MessageBox.Show(lista[1]);
                    Limpiar();
                }
                else
                {   
                    Ocultar();
                    Limpiar();
                }
            }
            else  // caso devolucion
            {
                lista = entorno.DevuelveImplementoPrestamo(siglaSelect, codigoEstu, cantidad);

                if (lista.Count != 0 && lista[0] == entorno.ERRORSQL)
                {
                    //if (this.IsVisible) MessageBox.Show(lista[1]);
                    MessageBox.Show(lista[1]);
                    Limpiar();
                }
                else if (lista.Count != 0 && lista[0] == entorno.INFOSQL)
                {   
                    Ocultar();
                    Limpiar();
                }
                else
                {   
                    Ocultar();
                    Limpiar();
                }

            }
            
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("<<< Btn2_Click ");
            Ocultar();
            Limpiar();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }       

        private void Cmbox_Sigla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("<<< Cmbox_Sigla_SelectionChanged ");
            if (this.IsVisible) {
                siglaSelect = cmbox_Sigla.SelectedValue.ToString();
                List<string> lista = new List<string>();

                chbox_pres.IsEnabled = false;
                chbox_dev.IsEnabled = false;

                if (valor_pres) // caso de prestamo
                {
                    lista = entorno.Implementos_dispCabtidad_sigla(siglaSelect);

                    if (lista.Count != 0 && lista[0] == entorno.ERRORSQL)
                    {
                        cmbox_Cant.ItemsSource = new List<string>();
                        MessageBox.Show(lista[1]);
                    }
                    else
                    {
                        cmbox_Cant.ItemsSource = Listar(lista);
                    }
                }
                else // caso devolucion
                {
                    lista = entorno.Implementos_dispPrestamo_sigla(siglaSelect, codigoEstu);

                    if (lista.Count != 0 && lista[0] == entorno.ERRORSQL)
                    {
                        cmbox_Cant.ItemsSource = new List<string>();
                        MessageBox.Show(lista[1]);
                    }
                    else
                    {
                        cmbox_Cant.ItemsSource = Listar(lista);
                    }
                }

                cmbox_Cant.IsEnabled = true;
            }
        }

        private void Cmbox_Cant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("<<< Cmbox_Cant_SelectionChanged ");

            if (this.IsVisible)
            {
                if (valor_pres)
                {
                    obs1.IsEnabled = true;
                }

                btn1.IsEnabled = true;
            }            
        }

        private void Limpiar()
        {
            Debug.WriteLine("<<< LIMPIAR");
            btn3.IsEnabled = true;

            siglaSelect = "";
            codigoEstu = 0;
            cantidad = 0;
            observacion = "";

            codigo.Text = "";
            obs1.Text = "";
            codigo.IsEnabled = true;

            cmbox_Sigla.SelectedValue = null;
            cmbox_Cant.SelectedValue = null;
                        
            cmbox_Cant.IsEnabled = false;
            cmbox_Sigla.IsEnabled = false;

            chbox_pres.IsEnabled = false;
            chbox_pres.IsChecked = false;
            chbox_dev.IsEnabled = false;            
            chbox_dev.IsChecked = false;
            valor_pres = false;
            valor_dev = false;

            obs1.Text = "";
            obs1.IsEnabled = false;

            btn1.IsEnabled = false;

            codigo.Focus();
            //bool variable = chbox.IsChecked ?? false;
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("<<< Btn3_Click ");
            // capturar datos y validar
            if (codigo.Text == "" || !UInt64.TryParse(codigo.Text, out UInt64 abc))
            {
                MessageBox.Show("El código no es valido!");
            }
            else
            {
                codigoEstu = Convert.ToUInt64(codigo.Text);

                List<string> lista = new List<string>();
                lista = entorno.ValidarEstudianteDeportes(codigoEstu, "",0);

                if (lista.Count != 0 && lista[0] == entorno.ERRORSQL)
                {
                    Limpiar();
                    if (this.IsVisible) MessageBox.Show(lista[1]);
                }
                else
                {
                    btn3.IsEnabled = false;
                    codigo.IsEnabled = false;
                    chbox_pres.IsEnabled = true;
                    chbox_dev.IsEnabled = true;

                    //ActualizarCmbxSiglas();
                }
            }
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("<<< Btn4_Click ");
            Limpiar();
        }

        private void Chbox_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("<<< Chbox_Click ");
            CheckBox chk = (CheckBox)sender;
            string name = chk.Name;

            if (name == "chbox_pres")
            {
                valor_pres = chbox_pres.IsChecked ?? false;
                chbox_dev.IsChecked = !valor_pres;
                valor_dev = !valor_pres;
                Debug.WriteLine("<< pres: pres = " + valor_pres + ", dev= " + valor_dev);
            }
            else
            {
                valor_dev = chbox_dev.IsChecked ?? false;
                chbox_pres.IsChecked = !valor_dev;
                valor_pres = !valor_dev;
                Debug.WriteLine("<< dev: pres = " + valor_pres + ", dev= " + valor_dev);
            }

            cmbox_Sigla.IsEnabled = true;
            ActualizarCmbxSiglas();
        }        
    }
}
