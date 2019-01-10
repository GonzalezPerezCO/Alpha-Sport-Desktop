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
    /// Lógica de interacción para Horarios.xaml
    /// </summary>
    public partial class Horarios : Window
    {
        private Entorno entorno;
        private string codigo;

        private static List<string> cuposL = new List<string>();
        private static List<Label> labelsY = new List<Label>();
        private readonly List<string> DIAS = new List<string> { "LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES" };
        private readonly List<string> HORAS = new List<string> { "N/A", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };

        public Horarios()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;

            IniciarLabelsY(); // crear Lista de Labels para mostrar cupos
            MostrarCupos(); // mostar cupos disponibles
            ActualizarCmbox(); // inicializa los Cmbox

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
             
        private void Limpiar() {
            
            txt3.Text = "";
            txt4.Content = "------";
            txt5.Content = "------";
            txt6.Content = "--";
            txt7.Content = "--";
            txt8.Content = "Dia 1";
            txt9.Content = "Dia 2";
            txt10.Content = "Dia 3";
            txt11.Content = "N/A";
            txt12.Content = "N/A";
            txt13.Content = "N/A";

            cmbx1.SelectedValue = null;
            cmbx2.SelectedValue = null;
            cmbx3.SelectedValue = null;
            cmbx4.SelectedValue = null;
            cmbx5.SelectedValue = null;
            cmbx6.SelectedValue = null;

            bt_mod.IsEnabled = false;
            txt3.IsEnabled = true;
            txt3.Focus();
        }

        private void Click_bt2(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            tabla.Show();
            this.Hide();
        }

        private void Click_bt1(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Click_bt3(object sender, RoutedEventArgs e)
        {
            codigo = txt3.Text;
            Limpiar();

            if (codigo == "" || (!UInt32.TryParse(codigo, out UInt32 abc)))
            {
                MessageBox.Show("El código del estudiante es incorrecto!");
            }
            else
            {
                //0: nombre, 1: carrera, 2: semestre, 3: fallas, 4: codigo, 5: dia1,hora1,dia2,hora2,dia3,hora3
                List<string> lista = entorno.Asistencia(Convert.ToInt32(codigo));

                bool buscarEstudiante = entorno.BuscarEstudiante(Convert.ToUInt32(codigo), "");

                if (buscarEstudiante && lista.Count > 0)
                {
                    SepararDias(lista);  // descompone la posicion 5 y agrega los 6 elementos que se necesitan en el orden que se necesitan     

                    foreach (var item in lista)
                    {
                        Debug.WriteLine("<<<< Lista horario: " + item);
                    }

                    txt4.Content = lista[0];
                    txt5.Content = lista[1];
                    txt6.Content = lista[2];
                    txt7.Content = lista[3];

                    txt8.Content = lista[5];
                    txt9.Content = lista[6];
                    txt10.Content = lista[7];
                    txt11.Content = lista[8];
                    txt12.Content = lista[9];
                    txt13.Content = lista[10];

                    txt3.IsEnabled = false;
                    bt_mod.IsEnabled = true;
                }
                else if (buscarEstudiante && lista.Count == 0)
                {
                    //0: nombre, 1: carrera, 2: semestre, 3: fallas, 4: codigo
                    lista = entorno.DatosEstudiante(Convert.ToInt32(codigo));

                    MessageBox.Show("No esta registrado el Estudiante en el Gimnasio!");
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            Debug.WriteLine("<<<< Lista datos estu: " + item);
                        }

                        txt4.Content = lista[0];
                        txt5.Content = lista[1];
                        txt6.Content = lista[2];
                        txt7.Content = lista[3];

                        txt8.Content = "N/A";
                        txt9.Content = "N/A";
                        txt10.Content = "N/A";
                        txt11.Content = "N/A";
                        txt12.Content = "N/A";
                        txt13.Content = "N/A";

                        txt3.IsEnabled = false;
                        bt_mod.IsEnabled = true;
                    }
                }                
                else
                {   
                    MessageBox.Show("No esta registrado en la Base de Datos de Deportes!");
                }               
            }
        }

        private void SepararDias(List<string> lista)
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

                if (separadas.Length == 2 && separadas.Length > 0)
                {
                    Debug.WriteLine("<<< case  2,0");
                    lista.Add(separadas[0]);
                    lista.Add("N/A"); //d2
                    lista.Add("N/A"); //d3

                    lista.Add(separadas[1]);
                    lista.Add("N/A"); //h2
                    lista.Add("N/A"); //h3

                }
                else if (separadas.Length == 4 && separadas.Length > 2)
                {
                    Debug.WriteLine("<<< case  4,2");
                    lista.Add(separadas[0]);
                    lista.Add(separadas[2]);
                    lista.Add("N/A"); //d3

                    lista.Add(separadas[1]);
                    lista.Add(separadas[3]);
                    lista.Add("N/A"); //h3

                }
                else
                {
                    Debug.WriteLine("<<< case  all");
                    lista.Add(separadas[0]); //d1
                    lista.Add(separadas[2]); //d2
                    lista.Add(separadas[4]); //d3

                    lista.Add(separadas[1]); //h1
                    lista.Add(separadas[3]); //h2
                    lista.Add(separadas[5]); //h3
                }
            }

        }


        private List<string> SepararIds(List<string> lista)
        {
            // a,b,c,...,x  ==> {id, Lunes, Martes, Miercoles, Jueves, Viernes}

            List<string> result = new List<string>();

            string[] separadas;
            separadas = lista[0].Split(',');

            foreach (var item in separadas)
            {
                result.Add(item);
                Debug.WriteLine("<< a lista: " + item);
            }
            Debug.WriteLine("<< size : " + result.Count());
            return result;
        }


        private void Bt_mod_Click(object sender, RoutedEventArgs e)
        {
            MostrarHorarioEstudiante();
            cuposL = SepararIds(entorno.Cupos());           

            ActualizarCmbox();
        }

        private void MostrarHorarioEstudiante()
        {
            string result = entorno.CambiarHorario(9, "Lunes", "MANUEL.PEREZ-E@MAIL.ESCUELAING.EDU.CO");

            if (result == "")
            {
                MessageBox.Show("Agregado correctamnente!");
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void ActualizarCmbox()
        {        
            cmbx1.ItemsSource = DIAS;
            cmbx2.ItemsSource = DIAS;
            cmbx3.ItemsSource = DIAS;

            cmbx4.ItemsSource = HORAS;
            cmbx5.ItemsSource = HORAS;
            cmbx6.ItemsSource = HORAS;
        }

        private void MostrarCupos()
        {
            cuposL = SepararIds(entorno.Cupos());

            labY0.Content = cuposL[0];
            labY1.Content = cuposL[1];
            labY2.Content = cuposL[2];
            labY3.Content = cuposL[3];
            labY4.Content = cuposL[4];
            labY5.Content = cuposL[5];

            labY6.Content = cuposL[6];
            labY7.Content = cuposL[7];
            labY8.Content = cuposL[8];
            labY9.Content = cuposL[9];
            labY10.Content = cuposL[10];
            labY11.Content = cuposL[11];

            labY12.Content = cuposL[12];
            labY13.Content = cuposL[13];
            labY14.Content = cuposL[14];
            labY15.Content = cuposL[15];
            labY16.Content = cuposL[16];
            labY17.Content = cuposL[17];

            labY18.Content = cuposL[18];
            labY19.Content = cuposL[19];
            labY20.Content = cuposL[20];
            labY21.Content = cuposL[21];
            labY22.Content = cuposL[22];
            labY23.Content = cuposL[23];

            labY24.Content = cuposL[24];
            labY25.Content = cuposL[25];
            labY26.Content = cuposL[26];
            labY27.Content = cuposL[27];
            labY28.Content = cuposL[28];
            labY29.Content = cuposL[29];

            labY30.Content = cuposL[30];
            labY31.Content = cuposL[31];
            labY32.Content = cuposL[32];
            labY33.Content = cuposL[33];
            labY34.Content = cuposL[34];
            labY35.Content = cuposL[35];

            labY36.Content = cuposL[36];
            labY37.Content = cuposL[37];
            labY38.Content = cuposL[38];
            labY39.Content = cuposL[39];
            labY40.Content = cuposL[40];
            labY41.Content = cuposL[41];

            labY42.Content = cuposL[42];
            labY43.Content = cuposL[43];
            labY44.Content = cuposL[44];
            labY45.Content = cuposL[45];
            labY46.Content = cuposL[46];
            labY47.Content = cuposL[47];

            labY48.Content = cuposL[48];
            labY49.Content = cuposL[49];
            labY50.Content = cuposL[50];
            labY51.Content = cuposL[51];
            labY52.Content = cuposL[52];
            labY53.Content = cuposL[53];
        }

        private void IniciarLabelsY()
        {
            // agregar Labels que muestran el horario
            labelsY.Add(labY0);
            labelsY.Add(labY1);
            labelsY.Add(labY2);
            labelsY.Add(labY3);
            labelsY.Add(labY4);
            labelsY.Add(labY5);
            // -----
            labelsY.Add(labY6);
            labelsY.Add(labY7);
            labelsY.Add(labY8);
            labelsY.Add(labY9);
            labelsY.Add(labY10);
            labelsY.Add(labY11);
            // -----
            labelsY.Add(labY12);
            labelsY.Add(labY13);
            labelsY.Add(labY14);
            labelsY.Add(labY15);
            labelsY.Add(labY16);
            labelsY.Add(labY17);
            // -----
            labelsY.Add(labY18);
            labelsY.Add(labY19);
            labelsY.Add(labY20);
            labelsY.Add(labY21);
            labelsY.Add(labY22);
            labelsY.Add(labY23);
            // -----
            labelsY.Add(labY24);
            labelsY.Add(labY25);
            labelsY.Add(labY26);
            labelsY.Add(labY27);
            labelsY.Add(labY28);
            labelsY.Add(labY29);
            // -----
            labelsY.Add(labY30);
            labelsY.Add(labY31);
            labelsY.Add(labY32);
            labelsY.Add(labY33);
            labelsY.Add(labY34);
            labelsY.Add(labY35);

            // -----
            labelsY.Add(labY36);
            labelsY.Add(labY37);
            labelsY.Add(labY38);
            labelsY.Add(labY39);
            labelsY.Add(labY40);
            labelsY.Add(labY41);
            // -----
            labelsY.Add(labY42);
            labelsY.Add(labY43);
            labelsY.Add(labY44);
            labelsY.Add(labY45);
            labelsY.Add(labY46);
            labelsY.Add(labY47);
            // -----
            labelsY.Add(labY48);
            labelsY.Add(labY49);
            labelsY.Add(labY50);
            labelsY.Add(labY51);
            labelsY.Add(labY52);
            labelsY.Add(labY53);
        }
    }
}
