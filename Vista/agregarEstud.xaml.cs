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
            if (txt1.Text == "" || txt2.Text == "" || txt3.Text == "" || cmbox.SelectedItem != null || txt5.Text == "" || txt6.Text == "")
            {
                MessageBox.Show("Llene todos los campos para poder continuar.");
            }
            else {
                string nombres = txt1.Text;
                string apellidos = txt2.Text;
                string codigo = txt3.Text;
                string documento = txt8.Text;
                string carrera = Convert.ToString(cmbox.SelectedValue);
                string semestre = txt5.Text;
                string email = txt6.Text;
                string obs = txt7.Text;
                entorno.agregarEstudiante(nombres, apellidos, Convert.ToInt32(codigo), Convert.ToInt32(documento), carrera, Convert.ToInt32(semestre), email, obs);
                MessageBox.Show("El estudiante "+nombres+" "+apellidos+" fue agregado.");
                limpiar();
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


