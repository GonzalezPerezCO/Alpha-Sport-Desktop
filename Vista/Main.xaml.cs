using AlphaSport;
using AlphaSport.Controller;
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
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private Entorno entorno;

        public Main()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            //lab1.Content = entorno.PROYECTO;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn1_MouseEnter(object sender, MouseEventArgs e)
        {   /*         
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(@"/Recursos/gym.jpg", UriKind.RelativeOrAbsolute));
            btn1.Background = imgBrush;
            */
        }

        private void Btn1_MouseLeave(object sender, MouseEventArgs e)
        {
            /*
             * ImageBrush imgBrush = new ImageBrush();            
            imgBrush.ImageSource = new BitmapImage(new Uri(@"/Recursos/gym.jpg", UriKind.RelativeOrAbsolute));
            btn1.Background = imgBrush;          
             */
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Window tabla = new TablaInscritos();                    
            tabla.Show();
            this.Hide();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            TablaImplementos tabla = TablaImplementos.GetInstance();
            tabla.Show();
            this.Hide();
        }        
    }
}
