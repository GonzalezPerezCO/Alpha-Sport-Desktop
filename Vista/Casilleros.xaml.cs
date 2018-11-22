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

            colorButtons();
        }

        private void colorButtons() {
            Btn0.Background = Brushes.Yellow;
            Btn1.Background = Brushes.Yellow;
            Btn2.Background = Brushes.Yellow;
            Btn3.Background = Brushes.Yellow;
            Btn4.Background = Brushes.Yellow;
            Btn5.Background = Brushes.Yellow;
            Btn6.Background = Brushes.Yellow;
            Btn7.Background = Brushes.Yellow;
            Btn8.Background = Brushes.Yellow;
            Btn9.Background = Brushes.Yellow;
            Btn10.Background = Brushes.Yellow;
            Btn11.Background = Brushes.Yellow;
            Btn12.Background = Brushes.Yellow;
            Btn13.Background = Brushes.Yellow;
            Btn14.Background = Brushes.Yellow;
            Btn15.Background = Brushes.Yellow;
            Btn16.Background = Brushes.Yellow;
            Btn17.Background = Brushes.Yellow;
            Btn18.Background = Brushes.Yellow;
            Btn19.Background = Brushes.Yellow;
            Btn20.Background = Brushes.Yellow;
            Btn21.Background = Brushes.Yellow;
            Btn22.Background = Brushes.Yellow;
            Btn23.Background = Brushes.Yellow;
            Btn24.Background = Brushes.Yellow;
            Btn25.Background = Brushes.Yellow;
            Btn26.Background = Brushes.Yellow;
            Btn27.Background = Brushes.Yellow;
            Btn28.Background = Brushes.Yellow;
            Btn29.Background = Brushes.Yellow;
            Btn30.Background = Brushes.Yellow;
            Btn31.Background = Brushes.Yellow;
            Btn32.Background = Brushes.Yellow;
            Btn33.Background = Brushes.Yellow;
            Btn34.Background = Brushes.Yellow;
            Btn35.Background = Brushes.Yellow;
            Btn36.Background = Brushes.Yellow;
            Btn37.Background = Brushes.Yellow;
            Btn38.Background = Brushes.Yellow;
            Btn39.Background = Brushes.Yellow;

            Btn40.Background = Brushes.DodgerBlue;
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
    }
}
