using Deportes_WPF.Controller;
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

namespace Deportes_WPF.Vista
{
    /// <summary>
    /// Lógica de interacción para Asistencia.xaml
    /// </summary>
    public partial class Asistencia : Window
    {

        private Entorno entorno;

        public Asistencia()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            botonesEstado(false);
            lab6.Content = "Ingrese el código del estudiante y realice la busqueda";
            txt1.Focus();
        }       

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            tabla.Show();
            this.Hide();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            lab5.Content = "nombre y apellido";
            botonesEstado(false);
            lab6.Content = "Ingrese el código del estudiante y realice la busqueda";
            txt1.Focus();
        }

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            string codigo = txt1.Text;

            if (codigo == "")
            {
                MessageBox.Show("No hay codigo del estudiante");
            }
            else
            {
                //0: nombre, 1: carrera, 2: semestre, 3: fallas, 4: codigo, 5: dia1, 6: dia2, 7: dia3, 8: hora1, 9: hora2, 10: hora3
                List<string> lista = entorno.asistencia(Convert.ToInt32(codigo));

                if (lista.Capacity > 0)
                {
                    lab5.Content = lista[0];
                    lab10.Content = lista[1];
                    lab11.Content = lista[2];
                    lab8.Content = lista[3];

                    Debug.WriteLine("**** For :" + lista.Count);
                    // mostrar horario con tabla
                    DataTable tabla = entorno.horarioEstudiante(Convert.ToInt32(codigo));
                    dtgrid1.ItemsSource = tabla.DefaultView;
                    // -- fin

                    // calcular dia y hora actual
                    DateTime dt = DateTime.Now;
                    string diaActual = aEspanol(dt.DayOfWeek.ToString());
                    string horaActual = dt.Hour.ToString(); ;
                    // -- fin

                    string mensaje = "";
                    for (int i= 2; i <= 4; i++)
                    {
                        Debug.WriteLine("<<<<<<<<<<<<< datos: " + lista[i]);

                        if (lista[i] == diaActual) {
                            if (lista[i + 3] == horaActual)
                            {
                                mensaje = "Franja Horaria para registrar: " + diaActual + " - " + horaActual + ":00.";
                                bt4.IsEnabled = true;
                            }
                            else
                            {
                                Debug.WriteLine("<<<<<<<<<<<<< no asignado");
                                mensaje = "El estudiante no tiene asignada esta franja horaria: " + diaActual + " - " + horaActual + ":00.";                               
                            }
                            bt5.IsEnabled = true;
                        }
                        
                    }
                    
                    if(mensaje =="") mensaje = "El estudiante no tiene un horario para el Gimnasio.";

                    lab6.Content = mensaje;
                }
                else
                {
                    lab5.Content = "No encontrado";
                    lab6.Content = "Estudiante no encontrado!";
                    
                }

            }

        }

        private string aEspanol(string day) {
            string dia = "";

            switch (day)
            {
                case "Monday":
                    dia = "Lunes";
                    break;
                case "Tuesday":
                    dia = "Martes";
                    break;
                case "Wednesday":
                    dia = "Miercoles";
                    break;
                case "Thursday":
                    dia = "Jueves";
                    break;
                case "Friday":
                    dia = "Viernes";
                    break;
                case "Saturday":
                    dia = "Sabado";
                    break;
                case "Sunday":
                    dia = "Domingo";
                    break;
                //default:
                    //dia = "N/A";
                    //break;
            }

            return dia;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void bt4_Click(object sender, RoutedEventArgs e)
        {
            // falta colocar aceptar
            MessageBox.Show("Asistencia registrada");
            botonesEstado(false);
        }

        private void bt5_Click(object sender, RoutedEventArgs e)
        {
            // falta rechazar
            MessageBox.Show("Asistencia Rechazada");
            botonesEstado(false);
        }

        private void botonesEstado(bool estado) {
            Debug.WriteLine("Mostrar botones 5 y 6 = "+estado);
            bt4.IsEnabled = estado;
            bt5.IsEnabled = estado;            
        }
    }
}
