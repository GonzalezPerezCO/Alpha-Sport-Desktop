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
        private List<Button> botones = new List<Button>();     // lista de bontones
        private List<string> infoBotones = new List<string>(); // lista de datos botones {0:id_c, 1:diposnible, 2:seccion} 


        public Casilleros()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;

            listaBotones();
            actualizarColores(); // Btn1.Background = Brushes.HotPink; Btn41.Background = Brushes.DodgerBlue;

            codigo.Focus();
        }
        
        private void listaBotones()
        {
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

        public void actualizarColores()
        {
            string[] tagButton = null;
            
            infoBotones = separarIdSeccion(entorno.infoCasilleros());  // lista: [i+0]id_c,  [i+1]disponible, [i+2]seccion  

            for (int i = 0; i < botones.Count; i++)
            {
                tagButton = Convert.ToString(botones[i].Tag).Split(','); // convertir a string[id_c, seccion]

                if (infoBotones[i + 1] == "0")
                {
                    botones[i].Background = Brushes.LightGray;                    
                }
                else
                {
                    if (infoBotones[i+2] == "mujeres")
                    {
                        botones[i].Background = Brushes.HotPink;
                    }
                    else
                    {
                        botones[i].Background = Brushes.DodgerBlue;
                    }
                }
            }
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


        private List<string> separarIdSeccion(List<string> entrada)
        {
            // lista: [i+0]id_c,  [i+1]disponible, [i+2]seccion
            string cadena = entrada[0];

            List<string> lista = new List<string>();

            string[] separadas = cadena.Split(',');

            foreach (var item in separadas)
            {
                lista.Add(item);
            }

            for (int i = 0; i < lista.Count; i++)
            {
                Debug.WriteLine("<<< " + i + " sep ID SECCION: " + lista[i]);
            }

            return lista;

        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {

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
                    MessageBox.Show("Asignado a: " + lista[0] + ".\n" + "Código: " + lista[1] + ".\n" + "Casillero: " + lista[2] + ".\n" + "Entrada: " + lista[4] + ".");
                }
                else
                {                    
                    MessageBox.Show("No se encontraron coincidencias.");
                }
            }

        }

        private void cambiarEstado(Button btn, bool disp)
        {
            // boton, disponible True o false            
            // en TAG: genero: m y h

                                 // [0]id_button, [2]genero
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

                                                // [0]id_butto, [2]genero
            string[] tagButton = Convert.ToString(objeto.Tag).Split(',');
            List<string> lista = entorno.buscarCasilleroID(Convert.ToInt32(tagButton[0]));

            if (lista.Count > 0)
            {
                // caso para mostrar datos del prestamo                
                MessageBox.Show("Asignado a: " + lista[0] + ".\n" + "Código: " + lista[1] + ".\n" + "Casillero: " + lista[2] + ".\n" + "Entrada: " + lista[4] + ".");
            }
            else
            {
                // caso para agregar el prestamo                
                MessageBox.Show("Esta disponible este casillero.");
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            actualizarColores();
        }
    }
}
