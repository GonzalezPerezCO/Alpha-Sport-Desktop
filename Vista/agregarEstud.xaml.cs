using Deportes_WPF.Controller;
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

namespace Deportes_WPF.Vista
{
    /// <summary>
    /// Lógica de interacción para agregarEstud.xaml
    /// </summary>
    public partial class agregarEstud : Window
    {

        private Entorno entorno;
        private static List<string> carreras;

        public agregarEstud()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            prepararCarreras();
            limpiar();
            botonesEstado(true);
        }

        private List<string> separarIds(List<string> lista)
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

        private void prepararCarreras()
        {
            carreras = separarIds(entorno.carreras());
            cmbox.ItemsSource = carreras;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Window inscritos = new TablaInscritos();

            inscritos.Show();
            this.Hide();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (txt1.Text == "" || txt2.Text == "" || txt3.Text == "" || cmbox.SelectedItem == null || txt5.Text == "" || txt6.Text == "")
            {
                MessageBox.Show("Llene todos los campos para poder continuar.");
            }
            else {
                // validación de tipo de dato correcto, que los numericos sean numeros
               
                if (!int.TryParse(txt3.Text, out int abc) || !int.TryParse(txt8.Text, out int def) || !int.TryParse(txt5.Text, out int ghi))
                {
                    MessageBox.Show("Hay campos númericos con texto, ");
                }
                else
                {
                    int codigo = Convert.ToInt32(txt3.Text);
                    string email = txt6.Text.TrimStart().TrimEnd();

                    bool existe = entorno.buscarEstudiante(codigo, email);

                    if (!existe)
                    {
                        string nombres = txt1.Text.TrimStart().TrimEnd();
                        string apellidos = txt2.Text.TrimStart().TrimEnd();
                        int documento = Convert.ToInt32(txt8.Text);
                        string carrera = Convert.ToString(cmbox.SelectedValue).TrimStart().TrimEnd();
                        int semestre = Convert.ToInt32(txt5.Text);
                        string obs = txt7.Text.TrimStart().TrimEnd();
                        entorno.agregarEstudiante(nombres, apellidos, codigo, documento, carrera, semestre, email, obs);
                        MessageBox.Show("El estudiante " + nombres + " " + apellidos + " fue agregado.");
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El estudiante ya existe!");
                    }
                }
            }
         
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            Window inscritos = new TablaInscritos();

            inscritos.Show();
            this.Hide();
        }

        public void botonesEstado(bool estado) {
            btn3.IsEnabled = estado;
            btn4.IsEnabled = estado;
        }

        public void limpiar() {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            cmbox.SelectedValue = null;
            txt5.Text = "";
            txt6.Text = "";
            txt7.Text = "";
            txt8.Text = "";
        }
    }
}


