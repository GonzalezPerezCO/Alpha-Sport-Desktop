using Deportes_WPF;
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

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para Casilleros.xaml
    /// </summary>
    public partial class Casilleros : Window
    {

        private Entorno entorno;


        public Casilleros()
        {   
            InitializeComponent();
            entorno = Entorno.GetInstance();
            lab1.Content = entorno.PROYECTO;

            codigo.Focus();
            colorButtons();
        }

        private void colorButtons() {            
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

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (codigo.Text == "")
            {
                MessageBox.Show("Escriba un código para buscar!");
            }
            else
            {   
                // Lista: 0:nombre, 1:codigo, 2:casillero
                List<string> lista = entorno.buscarCasillero(Convert.ToInt32(codigo.Text));

                if (lista.Capacity > 0)
                {
                    MessageBox.Show("Asignado a: " + lista[0] + ".\n" + "Código: " + lista[1] + ".\n" + "Casillero: " + lista[2] + ".\n" + "Entrada: " + lista[4] + ".\n" + "Salida: " + lista[5] + ".");
                }
                else
                {
                    MessageBox.Show("No se encontraron coincidencias.");
                }   
            }

        }

        private void Btn1_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> lista = entorno.buscarCasillero(Convert.ToInt32(codigo.Text));

            if (lista.Capacity > 0)
            {
                // caso para mostrar datos del prestamo

                MessageBox.Show("Asignado a: " + lista[0] + ".\n" + "Código: " + lista[1] + ".\n" + "Casillero: " + lista[2] + ".\n"+"Entrada: "+lista[4] + ".\n" + "Salida: " +lista[5]+".");
            }
            else
            {
                // caso para agregar el prestamo

                MessageBox.Show("No se encontraron coincidencias.");
            }


        }
    }
}
