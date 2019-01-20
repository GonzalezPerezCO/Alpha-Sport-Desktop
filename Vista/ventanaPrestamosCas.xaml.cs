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
    /// Lógica de interacción para ventanaPrestamosCas.xaml
    /// </summary>
    public partial class ventanaPrestamosCas : Window
    {

        private Entorno entorno;
        private static ventanaPrestamosCas instance = null;
        private static List<string> lista = new List<string>();

        private ventanaPrestamosCas()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

            codigo.Focus();

            lista = SepararIds(entorno.DisponiblesCasilleros());
            ActualizarListaDisp();
        }        

        public static ventanaPrestamosCas GetInstance()
        {
            if (instance == null)
                instance = new ventanaPrestamosCas();

            return instance;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private List<string> SepararIds(List<string> lista)
        {
            // a,b,c,...,x

            List<string> result = new List<string>();

            string[] separadas;
            separadas = lista[0].Split(',');

            foreach (var item in separadas)
            {
                result.Add(item);
                Debug.WriteLine("<< id a lista: " + item);
            }

            return result;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (codigo.Text == "" || !UInt64.TryParse(codigo.Text, out UInt64 abc))
            {
                MessageBox.Show("Ingrese un número de carnet valido!");
            }
            else
            {
                // Lista: nombre, codigo, casillero, disponible{0:no, 1:si}, entrada, salida
                UInt64 codigoEs = Convert.ToUInt64(codigo.Text);
                int codigoCas = Convert.ToInt32(cmbox.SelectedValue);
                List<string> busCod = entorno.BuscarCasilleroEstu(codigoEs);
                bool estudiante = entorno.BuscarEstudiante( Convert.ToUInt64(codigoEs), ""); // false: no existe en testudiantes

                if (busCod.Count != 0)
                {
                    if (cmbox.SelectedValue != null)
                    {
                        MessageBox.Show("El estudiante ya tiene un casillero asignado!");
                    }
                    else
                    {
                        MessageBox.Show("Estudiante encontrado! Casillero liberado.");
                        Debug.WriteLine("<<< Prestamo liberado: codigoEst = " + codigoEs + ".");
                        entorno.QuitarEstudianteCasillero(codigoEs);
                        ActualizarListaDisp();
                    }


                }
                else if (!estudiante)
                {
                    MessageBox.Show("Estudiante no encontrado!");
                }
                else if (estudiante)
                {
                    if (cmbox.Text == "")
                    {
                        MessageBox.Show("Agregue un casillero primero.");
                    }
                    else
                    {
                        entorno.AgregarEstudianteCasillero(codigoCas, codigoEs);
                        Debug.WriteLine("<<< Prestamo: id_c = " + codigoCas + ", codigoEst = " + codigoEs + ".");
                        MessageBox.Show("Casillero asignado!");
                        ActualizarListaDisp();
                    }
                }
            }

            Limpiar();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            Ocultar();
        }

        private void Limpiar() {
            codigo.Text = "";
            cmbox.SelectedValue = null;
            //this.Hide();
        }

        private void Ocultar() {
            Casilleros casilleros = Casilleros.GetInstance();
            casilleros.Show();
            this.Hide();
        }

        private void ActualizarListaDisp()
        {
            lista = SepararIds(entorno.DisponiblesCasilleros());
            cmbox.ItemsSource = lista;
            Ocultar();
        }        
    }
}
