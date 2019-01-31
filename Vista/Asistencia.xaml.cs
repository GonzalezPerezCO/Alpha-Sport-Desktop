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

            Limpiar();
        }       

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            tabla.Show();
            this.Hide();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();            
        }

        /// <summary>
        /// Limpia los campos a Default y activa/desactiva campos.
        /// </summary>
        private void Limpiar()
        {
            lab5.Content = "------";
            BotonesEstado(false);
            lab6.Content = "Ingrese el código del estudiante y realice la busqueda.";
            lab10.Content = "------";
            lab11.Content = "--";
            lab8.Content = "--";
            lab14.Content = "---";

            txt8.Content = "Dia 1";
            txt9.Content = "Dia 2";
            txt10.Content = "Dia 3";
            txt11.Content = "N/A";
            txt12.Content = "N/A";
            txt13.Content = "N/A";

            txt1.Text = "";
            txt1.Focus();
        }

        private void Bt3_Click(object sender, RoutedEventArgs e)
        {
            codigo = txt1.Text;

            if (codigo == "" || (!UInt64.TryParse(codigo, out UInt64 abc)) )
            {
                MessageBox.Show("El código del estudiante es incorrecto!");
            }
            else
            {
                //0: nombre, 1: carrera, 2: email, 3: semestre, 4:  fallas, 5: asistencias, 6: dia1,hora1,dia2,hora2,dia3,hora3
                List<string> lista = entorno.Asistencia(Convert.ToUInt64(codigo));
                
                if (lista.Count > 0)
                {
                    lab5.Content = lista[0];
                    lab10.Content = lista[1];
                    lab11.Content = lista[3];
                    lab8.Content = lista[4];
                    lab14.Content = lista[5];

                    SepararDias(lista);  // descompone la posicion 6 y agrega los 6 elementos que se necesitan en el orden que se necesitan

                    Debug.WriteLine("**** For :" + lista.Count);

                    List<string> datosFechaActual = entorno.CalcularHoy();

                    string diaActual = datosFechaActual[0];
                    string horaActual = datosFechaActual[1];

                    foreach (var item in lista)
                    {
                        Debug.WriteLine("<<<< Lista: " + item);
                    }

                    // mostrar horario
                    txt8.Content = lista[6];
                    txt9.Content = lista[7];
                    txt10.Content = lista[8];
                    txt11.Content = lista[9];
                    txt12.Content = lista[10];
                    txt13.Content = lista[11];

                    string mensaje = "";
                    // campo 5,6 y 7 con dias, 8,9,10 son las horas
                    for (int i= 5; i <= 7; i++)
                    {
                        Debug.WriteLine("<<<<<<<<<<<<< datos: " + lista[i]); // estoy hay que quitar en la implementacion

                        if (lista[i] == diaActual /*|| codigo == "2095112"*/) {
                            if (lista[i + 3] == horaActual /*|| lista[i + 3] == "0"*/) // es 0 para que 2095112 muestre este mensaje
                            {
                                mensaje = "Franja Horaria para registrar: " + diaActual + " - " + horaActual + ":00.";

                               // BotonesEstado(true);
                            }
                            else
                            {
                                Debug.WriteLine("<<<<<<<<<<<<< día no asignado");
                                mensaje = "El estudiante no tiene asignada esta franja horaria: " + diaActual + " - " + horaActual + ":00.";                               
                            }

                        }
                        
                    }
                    
                    if(mensaje =="") mensaje = "El estudiante no tiene este dia asignado: " + diaActual + " - " + horaActual + ":00.";

                    lab6.Content = mensaje;
                    BotonesEstado(true);
                }
                else
                {
                    lab5.Content = "No encontrado";
                    lab6.Content = "No esta registrado el Estudiante en el Gimnasio!";
                    
                }

            }

        }

        private void SepararDias(List<string> lista)
        {
            // cambiar posicion 6 de lista: "d1,h1,d2,h2,d3,h3" por ["d1","d2","d3","h1","h2","h3"]

            string cadena = lista[6];
            lista.RemoveAt(6);

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

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Bt4_Click(object sender, RoutedEventArgs e)
        {
            BotonesEstado(false);


            List<string> fallYasis = entorno.Fallas(Convert.ToUInt64(codigo), "ASISTENCIA", "Asistencia estudiantes.");
            lab8.Content = fallYasis[0];
            lab14.Content = fallYasis[1];

            MessageBox.Show("Asistencia registrada");
            
        }

        /// <summary>
        /// Deshabilita la Busqueda y el campo de codigo, activa la Asistencia , y VS. Mantiendolos contrarios para obligar a usar limpiar y evitar cambios en el código.
        /// </summary>
        /// <param name="estado"></param>
        private void BotonesEstado(bool estado) {
            Debug.WriteLine("Mostrar botone btn4 = "+estado);
            txt1.IsEnabled = !estado;
            bt3.IsEnabled = !estado;
            bt4.IsEnabled = estado;       
        }
    }
}
