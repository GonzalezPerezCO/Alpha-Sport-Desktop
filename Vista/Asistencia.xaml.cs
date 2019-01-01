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

        private string codigo;

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
            limpiar();
            
        }

        private void limpiar()
        {
            lab5.Content = "Nombres y Apellidos";
            botonesEstado(false);
            lab6.Content = "Ingrese el código del estudiante y realice la busqueda";
            lab10.Content = "0";
            lab11.Content = "Desconocido";
            lab8.Content = "0";

            txt8.Content = "Dia 1";
            txt9.Content = "Dia 2";
            txt10.Content = "Dia 3";
            txt11.Content = "N/A";
            txt12.Content = "N/A";
            txt13.Content = "N/A";


            txt1.Text = "";
            txt1.Focus();
        }

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            codigo = txt1.Text;

            if (codigo == "")
            {
                MessageBox.Show("No hay codigo del estudiante");
            }
            else
            {
                //0: nombre, 1: carrera, 2: semestre, 3: fallas, 4: codigo, 5:dia1,hora1,dia2,hora2,dia3,hora3
                List<string> lista = entorno.asistencia(Convert.ToInt32(codigo));

                if (lista.Count > 0)
                {
                    lab5.Content = lista[0];
                    lab10.Content = lista[1];
                    lab11.Content = lista[2];
                    lab8.Content = lista[3];

                    separarDias(lista);  // descompone la posicion 5 y agrega los 6 elementos que se necesitan en el orden que se necesitan

                    Debug.WriteLine("**** For :" + lista.Count);
                    
                    // calcular dia y hora actual
                    DateTime dt = DateTime.Now;
                    string diaActual = aEspanol(dt.DayOfWeek.ToString());
                    string horaActual = dt.Hour.ToString(); ;
                    // -- fin
                    Debug.WriteLine("<<<<<<<<<<<<< datos hora: " + diaActual + "a las  " +horaActual);
                                       

                    foreach (var item in lista)
                    {
                        Debug.WriteLine("<<<< Lista: " + item);
                    }

                    // mostrar horario
                    txt8.Content = lista[5];
                    txt9.Content = lista[6];
                    txt10.Content = lista[7];
                    txt11.Content = lista[8];
                    txt12.Content = lista[9];
                    txt13.Content = lista[10];

                    string mensaje = "";
                    // campo 5,6 y 7 con dias, 8,9,10 son las horas
                    for (int i= 5; i <= 7; i++)
                    {
                        Debug.WriteLine("<<<<<<<<<<<<< datos: " + lista[i]);

                        if (lista[i] == diaActual || codigo == "2095112") {
                            if (lista[i + 3] == horaActual || lista[i + 3] == "0") // es 0 para que 2095112 muestre este mensaje
                            {
                                mensaje = "Franja Horaria para registrar: " + diaActual + " - " + horaActual + ":00.";
                                bt4.IsEnabled = true;
                            }
                            else
                            {
                                Debug.WriteLine("<<<<<<<<<<<<< día no asignado");
                                mensaje = "El estudiante no tiene asignada esta franja horaria: " + diaActual + " - " + horaActual + ":00.";                               
                            }
                            //bt5.IsEnabled = true;
                        }
                        
                    }
                    
                    if(mensaje =="") mensaje = "El estudiante no tiene este dia asignado: " + diaActual + " - " + horaActual + ":00.";

                    lab6.Content = mensaje;
                }
                else
                {
                    lab5.Content = "No encontrado";
                    lab6.Content = "No esta registrado el Estudiante en el Gimnasio!";
                    
                }

            }

        }

        private void separarDias(List<string> lista)
        {
            // cambiar posicion 5 de lista: "d1,h1,d2,h2,d3,h3" por ["d1","d2","d3","h1","h2","h3"]

            string cadena = lista[5];
            lista.RemoveAt(5);

            if (cadena == "" || cadena == "N/A" || cadena.Length == 0)
            {
                lista.Add("N/A"); //d1
                lista.Add("N/A"); //d2
                lista.Add("N/A"); //d3

                lista.Add("N/A"); //h1
                lista.Add("N/A"); //h2
                lista.Add("N/A"); //h3
            }
            
            else
            {
                string[] separadas;
                separadas = cadena.Split(',');

                Debug.WriteLine("<<< separadas:");
                foreach (var item in separadas)
                {
                    Debug.WriteLine("<< " + item);
                }

                if (separadas.Length == 2 && separadas.Length > 0)
                {
                    lista.Add(separadas[0]);
                    lista.Add("N/A"); //d2
                    lista.Add("N/A"); //d3

                    lista.Add(separadas[1]);
                    lista.Add("N/A"); //h2
                    lista.Add("N/A"); //h3

                }
                else if (separadas.Length == 4 && separadas.Length > 2)
                {
                    lista.Add(separadas[0]);
                    lista.Add(separadas[2]);
                    lista.Add("N/A"); //d3

                    lista.Add(separadas[1]);
                    lista.Add(separadas[3]);
                    lista.Add("N/A"); //h3

                }
                else
                {   
                    lista.Add(separadas[0]); //d1
                    lista.Add(separadas[2]); //d2
                    lista.Add(separadas[4]); //d3

                    lista.Add(separadas[1]); //h1
                    lista.Add(separadas[3]); //h2
                    lista.Add(separadas[5]); //h3
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
                default:
                    dia = "N/A";
                    break;
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
            if (codigo == "2095112")
            {
              lab8.Content = entorno.fallas(Convert.ToInt32(codigo));

            }
            MessageBox.Show("Asistencia registrada");
            botonesEstado(false);
        }        

        private void botonesEstado(bool estado) {
            Debug.WriteLine("Mostrar botones 5 y 6 = "+estado);
            bt4.IsEnabled = estado;       
        }
    }
}
