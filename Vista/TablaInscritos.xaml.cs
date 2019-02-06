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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlphaSport.Vista;
using AlphaSport.Controller;
using AlphaSport.Vista;

namespace AlphaSport
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class TablaInscritos : Window
    {
        private Entorno entorno;
        private bool activado;

        public TablaInscritos()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            //lab1.Content = entorno.PROYECTO;
            
            ActualizarActivar(); // actualiza el color del boton y el valor de "activado" de la pagina
            MostrarTabla();
        }

        private void ActualizarActivar()
        {
            List<string> lista = entorno.EstadoPaginaWEB();

            if (lista.Count != 0 && lista[0] == entorno.INFOSQL || lista[0] == entorno.ERRORSQL) // cuando si esta pero no tiene casillero
            {
                MessageBox.Show("Contacte con el admistrador: "+ lista[1]);
            }
            else
            {
                if( Convert.ToBoolean(lista[0]))
                {
                    bt9.Background = Brushes.Green;
                    activado = true;

                    bt9.Content = "Deshabilitar WEB";

                }
                else
                {
                    activado = false;
                    bt9.Background = Brushes.OrangeRed;

                    bt9.Content = "Habilitar Página WEB";
                }
            }            
        }

        public void MostrarTabla()
        {
           DataTable dt = entorno.TablaInscritos();           
           dtgrid1.ItemsSource = dt.DefaultView;           
        }

        private void Bt1_Click(object sender, RoutedEventArgs e)
        {
            Window asistencia = new Asistencia();

            asistencia.Show();
            this.Hide();            
        }        

        private void Bt2_Click(object sender, RoutedEventArgs e)
        {
            Window cupos = new Cupos();

            cupos.Show();
            this.Hide();            
        }              

        private void Bt3_Click(object sender, RoutedEventArgs e)
        {
            Window agregar = new AgregarEstud();
                        
            agregar.Show();
            this.Hide();
        }

        private void Bt4_Click(object sender, RoutedEventArgs e)
        {
            Window horario = new Horarios();
            
            horario.Show();
            this.Hide();
        }

        private void Bt5_Click(object sender, RoutedEventArgs e)
        {
            Window main = new Main();

            main.Show();
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

        private void Bt6_Click(object sender, RoutedEventArgs e)
        {
            Window horarioGym = new HorarioGym();

            horarioGym.Show();
            this.Hide();
        }

        private void Bt7_Click(object sender, RoutedEventArgs e)
        {
            MostrarTabla();
        }

        private void Bt8_Click(object sender, RoutedEventArgs e)
        {
            RegistroAsistencia registro = new RegistroAsistencia();

            registro.Show();
            this.Hide();
        }

        private void Bt9_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = "";

            List<string> lista = entorno.ActivacionPaginaWEB( Convert.ToInt16(!activado) ); // envi el valor contrario a activado, convetir a int16

            if (lista.Count != 0 && (lista[0] == entorno.INFOSQL || lista[0] == entorno.ERRORSQL)) // cuando si esta pero no tiene casillero
            {
                mensaje = lista[1];
            }
            else
            {
                ActualizarActivar();                

                if (activado) { mensaje = "La página web de inscripciones fue activada!";}
                else { mensaje = "La página web de inscripciones fue desactivada!"; }
            }

            MessageBox.Show(mensaje);
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
