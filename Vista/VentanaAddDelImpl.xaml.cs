using AlphaSport.Controller;
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
    /// Lógica de interacción para VentanaAddDelImpl.xaml
    /// </summary>
    public partial class VentanaAddDelImpl : Window
    {
        private Entorno entorno;
        private static VentanaAddDelImpl instance;

        private string nombreIn;
        private string siglaIn;
        private UInt32 cantidadIn;
        private bool nuevoImpl;

        private VentanaAddDelImpl()
        {
            InitializeComponent();
            Limpiar();
            Debug.WriteLine("******* 1");
        }

        public static VentanaAddDelImpl GetInstance()
        {
            Debug.WriteLine("******* 2");
            if (instance == null)
                instance = new VentanaAddDelImpl();

            return instance;
        }

        

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        

        

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Chbox_Click_nuevo(object sender, RoutedEventArgs e)
        {
            
        }


        

        

        private void Chbox_Click_eliminar(object sender, RoutedEventArgs e)
        {
           
        }

        private void Cmbox_Sigla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
