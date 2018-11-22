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
    /// Lógica de interacción para Horarios.xaml
    /// </summary>
    public partial class Horarios : Window
    {
        private Entorno entorno;
        private string codigo;

        public Horarios()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            limpiar();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }            
             
        private void limpiar() {
            txt3.Text = "";
            txt4.Content = "Nombre y Apellido";
            txt5.Content = "Desconocido";
            txt6.Content = "0";
            txt7.Content = "0";
            txt8.Content = "Dia 1";
            txt9.Content = "Dia 2";
            txt10.Content = "Dia 3";
            txt11.Content = "N/A";
            txt12.Content = "N/A";
            txt13.Content = "N/A";

            txt3.Focus();
        }

        private void click_bt2(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            tabla.Show();
            this.Hide();
        }

        private void click_bt1(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void click_bt3(object sender, RoutedEventArgs e)
        {
            codigo = txt3.Text;

            if (codigo == "")
            {
                MessageBox.Show("No hay codigo del estudiante");
            }
            else
            {
                //0: nombre, 1: carrera, 2: semestre, 3: fallas, 4: codigo, 5: dia1,hora1,dia2,hora2,dia3,hora3
                List<string> lista = entorno.asistencia(Convert.ToInt32(codigo));

                if (lista.Capacity > 0)
                {
                    separarDias(lista);  // descompone la posicion 5 y agrega los 6 elementos que se necesitan en el orden que se necesitan     

                    foreach (var item in lista)
                    {
                        Debug.WriteLine("<<<< Lista: " + item);
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

    }
}
