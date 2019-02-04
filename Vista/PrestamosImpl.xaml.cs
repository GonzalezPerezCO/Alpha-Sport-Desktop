using AlphaSport.Controller;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para PrestamosImpl.xaml
    /// </summary>
    public partial class PrestamosImpl : Window
    {

        private Entorno entorno;
        private static PrestamosImpl instance;

        private UInt64 codigoEs;


        private PrestamosImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            //lab1.Content = entorno.PROYECTO;
            Limpiar();

            MostrarTabla();
        }

        public static PrestamosImpl GetInstance()
        {
            if (instance == null)
                instance = new PrestamosImpl();

            return instance;
        }

        private void Limpiar()
        {
            codigoEs = 0;
            codigo.Focus();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Bt1_Click(object sender, RoutedEventArgs e)
        {
            VentanaPrestamosImpl ventaPrestamos = VentanaPrestamosImpl.GetInstance();
            ventaPrestamos.Show();
            this.Hide();
        }

        private void Bt3_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void Bt2_Click(object sender, RoutedEventArgs e)
        {
            TablaImplementos implementos = TablaImplementos.GetInstance();
            implementos.Show();
            this.Hide();

        }

        public void MostrarTabla()
        {
            DataTable dt = entorno.TablaPrestamos();
            dtgrid1.ItemsSource = dt.DefaultView;
        }

        private void Bt4_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            MostrarTabla();
        }

        private List<string> SepararLista(List<string> lista)
        {
            // a,b,c,...,x

            List<string> result = new List<string>();

            string[] separadas;
            separadas = lista[0].Split(',');

            foreach (var item in separadas)
            {
                result.Add(item);
                Debug.WriteLine("<< Prestamo a lista: " + item);
            }

            return result;
        }

        private void Bt5_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = "";

            if (codigo.Text == "" || !UInt64.TryParse(codigo.Text, out UInt64 abc))
            {
                mensaje = "El código no es valido!";
            }
            else
            {   
                codigoEs = Convert.ToUInt64(codigo.Text);
                codigo.Text = "";

                List<string> datosPrestamo = entorno.PrestamosEstudiante(codigoEs);

                if (datosPrestamo.Count != 0 && (datosPrestamo[0] == entorno.ERRORSQL || datosPrestamo[0] == entorno.INFOSQL))
                {
                    mensaje = datosPrestamo[1];
                    //MessageBox.Show(mensaje);
                }
                else
                {
                    datosPrestamo = SepararLista(datosPrestamo);
                    int paso = 1; // ".\n"

                    // toma los elementos y los representa como un parrafo
                    for (int i = 0; i < datosPrestamo.Count; i++)
                    {
                        if (paso == 1)
                        {
                            mensaje = mensaje + "Se prestó: \n sigla: " + datosPrestamo[i];
                        }
                        else if (paso == 2)
                        {
                            mensaje = mensaje + "\n Cantidad: " + datosPrestamo[i];
                        }
                        else if (paso == 3)
                        {
                            mensaje = mensaje + "\n Observaciones: " + datosPrestamo[i];
                        }
                        else
                        {
                            mensaje = mensaje + "\n Fecha: " + datosPrestamo[i] + "\n" + "\n";
                            paso = 0;
                        }

                        paso += 1;

                    }
                }
                MessageBox.Show(mensaje);

            }
        }
    }
}
