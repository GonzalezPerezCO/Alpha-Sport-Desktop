using Deportes_WPF;
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

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para Casilleros.xaml
    /// </summary>
    public partial class Casilleros : Window
    {

        private Entorno entorno;
        private List<Button> botones; // lista de bontones
        private List<string> infoBotones; // lista de datos botones {0:id_c, 1:diposnible, 2:seccion} 


        public Casilleros()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;
            //listaBotones();

            actualizarColores();
            codigo.Focus();
        }

        private void actualizarColores()
        {
            List<string> infoBotones = entorno.infoCasilleros();
            separarIdSeccion(infoBotones);
            colorButtons();
        }

        private void colorButtons()
        {

            Btn1.Background = Brushes.HotPink;
            Btn2.Background = Brushes.HotPink;
            Btn3.Background = Brushes.HotPink;
            Btn4.Background = Brushes.HotPink;
            Btn5.Background = Brushes.HotPink;
            Btn6.Background = Brushes.HotPink;
            Btn7.Background = Brushes.HotPink;
            Btn8.Background = Brushes.HotPink;
            Btn9.Background = Brushes.HotPink;
            Btn10.Background = Brushes.HotPink;
            Btn11.Background = Brushes.HotPink;
            Btn12.Background = Brushes.HotPink;
            Btn13.Background = Brushes.HotPink;
            Btn14.Background = Brushes.HotPink;
            Btn15.Background = Brushes.HotPink;
            Btn16.Background = Brushes.HotPink;
            Btn17.Background = Brushes.HotPink;
            Btn18.Background = Brushes.HotPink;
            Btn19.Background = Brushes.HotPink;
            Btn20.Background = Brushes.HotPink;
            Btn21.Background = Brushes.HotPink;
            Btn22.Background = Brushes.HotPink;
            Btn23.Background = Brushes.HotPink;
            Btn24.Background = Brushes.HotPink;
            Btn25.Background = Brushes.HotPink;
            Btn26.Background = Brushes.HotPink;
            Btn27.Background = Brushes.HotPink;
            Btn28.Background = Brushes.HotPink;
            Btn29.Background = Brushes.HotPink;
            Btn30.Background = Brushes.HotPink;
            Btn31.Background = Brushes.HotPink;
            Btn32.Background = Brushes.HotPink;
            Btn33.Background = Brushes.HotPink;
            Btn34.Background = Brushes.HotPink;
            Btn35.Background = Brushes.HotPink;
            Btn36.Background = Brushes.HotPink;
            Btn37.Background = Brushes.HotPink;
            Btn38.Background = Brushes.HotPink;
            Btn39.Background = Brushes.HotPink;
            Btn40.Background = Brushes.HotPink;

            Btn41.Background = Brushes.DodgerBlue;
            Btn42.Background = Brushes.DodgerBlue;
            Btn43.Background = Brushes.DodgerBlue;
            Btn44.Background = Brushes.DodgerBlue;
            Btn45.Background = Brushes.DodgerBlue;
            Btn46.Background = Brushes.DodgerBlue;
            Btn47.Background = Brushes.DodgerBlue;
            Btn48.Background = Brushes.DodgerBlue;
            Btn49.Background = Brushes.DodgerBlue;
            Btn50.Background = Brushes.DodgerBlue;
            Btn51.Background = Brushes.DodgerBlue;
            Btn52.Background = Brushes.DodgerBlue;
            Btn53.Background = Brushes.DodgerBlue;
            Btn54.Background = Brushes.DodgerBlue;
            Btn55.Background = Brushes.DodgerBlue;
            Btn56.Background = Brushes.DodgerBlue;
            Btn57.Background = Brushes.DodgerBlue;
            Btn58.Background = Brushes.DodgerBlue;
            Btn59.Background = Brushes.DodgerBlue;
            Btn60.Background = Brushes.DodgerBlue;
            Btn61.Background = Brushes.DodgerBlue;
            Btn62.Background = Brushes.DodgerBlue;
            Btn63.Background = Brushes.DodgerBlue;
            Btn64.Background = Brushes.DodgerBlue;
            Btn65.Background = Brushes.DodgerBlue;
            Btn66.Background = Brushes.DodgerBlue;
            Btn67.Background = Brushes.DodgerBlue;
            Btn68.Background = Brushes.DodgerBlue;
            Btn69.Background = Brushes.DodgerBlue;
            Btn70.Background = Brushes.DodgerBlue;
            Btn71.Background = Brushes.DodgerBlue;
            Btn72.Background = Brushes.DodgerBlue;
            Btn73.Background = Brushes.DodgerBlue;
            Btn74.Background = Brushes.DodgerBlue;
            Btn75.Background = Brushes.DodgerBlue;
            Btn76.Background = Brushes.DodgerBlue;
            Btn77.Background = Brushes.DodgerBlue;
            Btn78.Background = Brushes.DodgerBlue;
            Btn79.Background = Brushes.DodgerBlue;
            Btn80.Background = Brushes.DodgerBlue;
        }

        private void listaBotones()
        {
            botones = new List<Button>();
            infoBotones = new List<string>();

            botones.Add(Btn1);
            botones.Add(Btn2);
            botones.Add(Btn3);
            botones.Add(Btn4);
            botones.Add(Btn5);
            botones.Add(Btn6);
            botones.Add(Btn7);
            botones.Add(Btn8);
            botones.Add(Btn9);
            botones.Add(Btn10);
            botones.Add(Btn11);
            botones.Add(Btn12);
            botones.Add(Btn13);
            botones.Add(Btn14);
            botones.Add(Btn15);
            botones.Add(Btn16);
            botones.Add(Btn17);
            botones.Add(Btn18);
            botones.Add(Btn19);
            botones.Add(Btn20);
            botones.Add(Btn21);
            botones.Add(Btn22);
            botones.Add(Btn23);
            botones.Add(Btn24);
            botones.Add(Btn25);
            botones.Add(Btn26);
            botones.Add(Btn27);
            botones.Add(Btn28);
            botones.Add(Btn29);
            botones.Add(Btn30);
            botones.Add(Btn31);
            botones.Add(Btn32);
            botones.Add(Btn33);
            botones.Add(Btn34);
            botones.Add(Btn35);
            botones.Add(Btn36);
            botones.Add(Btn37);
            botones.Add(Btn38);
            botones.Add(Btn39);
            botones.Add(Btn40);

            botones.Add(Btn41);
            botones.Add(Btn42);
            botones.Add(Btn43);
            botones.Add(Btn44);
            botones.Add(Btn45);
            botones.Add(Btn46);
            botones.Add(Btn47);
            botones.Add(Btn48);
            botones.Add(Btn49);
            botones.Add(Btn50);
            botones.Add(Btn51);
            botones.Add(Btn52);
            botones.Add(Btn53);
            botones.Add(Btn54);
            botones.Add(Btn55);
            botones.Add(Btn56);
            botones.Add(Btn57);
            botones.Add(Btn58);
            botones.Add(Btn59);
            botones.Add(Btn60);
            botones.Add(Btn61);
            botones.Add(Btn62);
            botones.Add(Btn63);
            botones.Add(Btn64);
            botones.Add(Btn65);
            botones.Add(Btn66);
            botones.Add(Btn67);
            botones.Add(Btn68);
            botones.Add(Btn69);
            botones.Add(Btn70);
            botones.Add(Btn71);
            botones.Add(Btn72);
            botones.Add(Btn73);
            botones.Add(Btn74);
            botones.Add(Btn75);
            botones.Add(Btn76);
            botones.Add(Btn77);
            botones.Add(Btn78);
            botones.Add(Btn79);
            botones.Add(Btn80);
        }
               

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();

            tabla.Show();
            this.Hide();

        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void bt5_Click(object sender, RoutedEventArgs e)
        {
            Window main = new Main();

            main.Show();
            this.Hide();
        }


        private void separarIdSeccion(List<string> lista)
        {
            // cambiar posicion 5 de lista: "d1,h1,d2,h2,d3,h3" por ["d1","d2","d3","h1","h2","h3"]

            string cadena = lista[0];

            lista = new List<string>();


            string[] separadas = cadena.Split(',');

            foreach (var item in separadas)
            {
                lista.Add(item);
            }

            for (int i = 0; i < lista.Count; i++)
            {
                Debug.WriteLine("<<< " + i + " sep ID SECCION: " + lista[i]);
            }

        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {

            actualizarColores();


            if (codigo.Text == "")
            {
                MessageBox.Show("Escriba un código para buscar!");
            }
            else
            {
                // Lista: nombre, codigo, casillero, disponible{0:no, 1:si}, entrada, salida
                List<string> lista = entorno.buscarCasilleroEstu(Convert.ToInt32(codigo.Text));


                if (lista.Count > 0)
                {
                    cambiarEstado(Btn1, false);
                    MessageBox.Show("Asignado a: " + lista[0] + ".\n" + "Código: " + lista[1] + ".\n" + "Casillero: " + lista[2] + ".\n" + "Entrada: " + lista[4] + ".");
                }
                else
                {
                    cambiarEstado(Btn1, true);
                    MessageBox.Show("No se encontraron coincidencias.");
                }
            }

        }

        private void cambiarEstado(Button btn, bool disp)
        {
            // boton, disponible True o false            
            // en TAG: genero: m y h

                                 // [0]id_button, [1]genero
            string[] tagButton = Convert.ToString(btn.Tag).Split(',');            

            if (disp)
            {
                if (tagButton[1]=="m")
                {
                    btn.Background = Brushes.HotPink;
                }
                else
                {
                    btn.Background = Brushes.DodgerBlue;
                }
            }
            else
            {
                btn.Background = Brushes.LightGray;
            }

        }

        private void Prestamo_Click(object sender, RoutedEventArgs e)
        {
            ventanaPrestamosCas prestamos = ventanaPrestamosCas.GetInstance();            
            //this.Hide();
            prestamos.Show();
            //this.Hide();
        }

        private void eventoClick(object sender, RoutedEventArgs e)
        {
            Button objeto = e.Source as Button;
            // Lista: nombre, codigo, casillero, disponible{0:no, 1:si}, entrada, salida  

                                                // [0]id_button, [1]genero
            string[] tagButton = Convert.ToString(objeto.Tag).Split(',');
            List<string> lista = entorno.buscarCasilleroID(Convert.ToInt32(tagButton[0]));

            if (lista.Count > 0)
            {
                // caso para mostrar datos del prestamo
                cambiarEstado(objeto, false);
                MessageBox.Show("Asignado a: " + lista[0] + ".\n" + "Código: " + lista[1] + ".\n" + "Casillero: " + lista[2] + ".\n" + "Entrada: " + lista[4] + ".");
            }
            else
            {
                // caso para agregar el prestamo
                cambiarEstado(objeto, true);
                MessageBox.Show("Esta disponible este casillero.");
            }
        }      


    }
}
