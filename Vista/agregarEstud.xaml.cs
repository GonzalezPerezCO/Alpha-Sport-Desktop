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
    /// Lógica de interacción para agregarEstud.xaml
    /// </summary>
    public partial class AgregarEstud : Window
    {

        private Entorno entorno;
        private static List<string> carreras;

        public AgregarEstud()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            PrepararCarrerasTYsemestre();
            Limpiar();
            BotonesEstado(true);
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

        private void PrepararCarrerasTYsemestre()
        {
            List<string> lista = entorno.Carreras();
            
            if (lista.Count != 0 && (lista[0] == entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
            {
                MessageBox.Show("Error critico leyendo la base de datos!" + "\n" + "Contacte con el administrador " + lista[1]);
                Application.Current.Shutdown();
            }
            carreras = SepararIds(lista);
            cmbox.ItemsSource = carreras;

            cmbox_semestre.ItemsSource = new List<UInt32>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Window inscritos = new TablaInscritos();

            inscritos.Show();
            this.Hide();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            if (txt1.Text == "" || txt2.Text == "" || txt3.Text == "" || cmbox.SelectedItem == null || cmbox_semestre.SelectedValue == null || txt6.Text == "" || txt9.Text == "")
            {
                MessageBox.Show("Llene todos los campos para poder continuar.");
            }
            else {
                // validación de tipo de dato correcto, que los numericos sean numeros
               
                if (!UInt64.TryParse(txt3.Text, out UInt64 abc) || !UInt64.TryParse(txt8.Text, out UInt64 def) || !UInt32.TryParse(txt9.Text, out UInt32 jkl))
                {
                    MessageBox.Show("Hay campos númericos con texto. Escriba solo numéros en el Código, el Documento, Reverva y en el Semestre.");
                }
                else
                {
                    string nombres = txt1.Text.TrimStart().TrimEnd();
                    string apellidos = txt2.Text.TrimStart().TrimEnd();
                    UInt32 reserva = Convert.ToUInt32(txt9.Text);
                    UInt64 codigo = Convert.ToUInt64(txt3.Text);
                    UInt64 documento = Convert.ToUInt64(txt8.Text);
                    string carrera = Convert.ToString(cmbox.SelectedValue).TrimStart().TrimEnd();
                    UInt32 semestre = Convert.ToUInt32(cmbox_semestre.SelectedValue.ToString());
                    string email = txt6.Text.TrimStart().TrimEnd();                    
                    string obs = txt7.Text.TrimStart().TrimEnd();
                    bool examen = chbx.IsChecked ?? false;

                    List<string> lista = entorno.AgregarEstudiante(nombres, apellidos, reserva, codigo, documento, carrera, semestre, email, obs, examen);

                    if (lista.Count != 0 && (lista[0]==entorno.ERRORSQL || lista[0] == entorno.INFOSQL))
                    {
                        MessageBox.Show(lista[1]);
                    }
                    else
                    {
                        MessageBox.Show("El estudiante " + nombres + " " + apellidos + " fue agregado.");
                        Limpiar();
                    }


                    /*
                    bool existe = entorno.BuscarEstudiante(codigo, email);

                    if (!existe) // sino esta creado
                    {
                        string nombres = txt1.Text.TrimStart().TrimEnd();
                        string apellidos = txt2.Text.TrimStart().TrimEnd();
                        UInt32 reserva = Convert.ToUInt32(txt9.Text);
                        UInt64 documento = Convert.ToUInt64(txt8.Text);
                        string carrera = Convert.ToString(cmbox.SelectedValue).TrimStart().TrimEnd();
                        UInt32 semestre = Convert.ToUInt32(cmbox_semestre.SelectedValue.ToString());
                        string obs = txt7.Text.TrimStart().TrimEnd();
                        bool examen = chbx.IsChecked ?? false;

                        entorno.AgregarEstudiante(nombres, apellidos, reserva, codigo, documento, carrera, semestre, email, obs, examen);
                        MessageBox.Show("El estudiante " + nombres + " " + apellidos + " fue agregado.");
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("El estudiante ya existe!");
                    }*/
                }
            }
         
        }

        public void BotonesEstado(bool estado) {
            btn3.IsEnabled = estado;
        }

        public void Limpiar() {
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            cmbox.SelectedValue = null;
            cmbox_semestre.SelectedValue = null;
            txt6.Text = "";
            txt7.Text = "";
            txt8.Text = "";
            txt9.Text = "";
            chbx.IsChecked = false;
        }
    }
}


