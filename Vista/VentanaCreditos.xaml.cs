using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Lógica de interacción para VentanaCreditos.xaml
    /// </summary>
    public partial class VentanaCreditos : Window
    {
        string version;
        string descripcion = "AlphaSport es una herramienta informática desarrollada para permitir y agilizar el proceso de  registro y  seguimiento de  horarios en  el Gimnasio  y además  gestionar los servicios que ofrece la Oficina de Deportes.";
        string nombre1 = "Directora de proyecto: \n Ing. CLAUDIA PATRICIA CASTAÑEDA BERMÚDEZ";
        string nombre2 = "JUAN FRANCISCO GONZÁLEZ ROJAS";
        string nombre3 = "MANUEL SERGIO PÉREZ ESPITIA";
        string desarrollo = "El desarrollo hizo parte del proyecto SISTEMA DE GESTIÓN DE RECURSOS DEPORTIVOS DE LA ESCUELA (PGR).";
        string apoyo = "Con el apoyo de la Decanatura de Ingeniería de Sistemas.";
        string fecha = "2018-i - 2019";

        string inicial;

        public VentanaCreditos()
        {
            InitializeComponent();
            Creditos();
            Debug.WriteLine(version);
        }

        private string GetRunningVersion()
        {
            return "Version del Ensamblador: " + Application.ResourceAssembly.GetName(true).Version.ToString();
        }

        private void Creditos()
        {           
            version = string.Format("{0}\n \n {1}\n \n Desarrollado por:\n {2}\n, Estudiantes: \n{3}, {4}\n \n \n {5}\n {6}\n \n {7} ", GetRunningVersion(), descripcion, nombre1, nombre2, nombre3, desarrollo, apoyo, fecha);
            txt1.Text = version + txt1.Text;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {

        }
    }
}
