using Deportes_WPF.Controller;
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

namespace Deportes_WPF.Vista
{
    /// <summary>
    /// Lógica de interacción para agregarEstud.xaml
    /// </summary>
    public partial class agregarEstud : Window
    {

        private Entorno entorno;

        public agregarEstud()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            limpiar();
            botonesEstado(true);

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
            if (txt1.Text == "" || txt2.Text == "" || txt3.Text == "" || txt4.Text == "" || txt5.Text == "" || txt6.Text == "")
            {
                MessageBox.Show("Llene todos los campos para poder continuar.");
            }
            else {
                string nombres = txt1.Text;
                string apellidos = txt2.Text;
                string codigo = txt3.Text;
                string carrera = txt4.Text;
                string semestre = txt5.Text;
                string email = txt6.Text;
                string obs = txt7.Text;
                entorno.agregarEstudiante(nombres, apellidos, Convert.ToInt32(codigo), carrera, Convert.ToInt32(semestre), email, obs);
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
            txt4.Text = "";
            txt5.Text = "";
            txt6.Text = "";
            txt7.Text = "";
        }
    }
}


