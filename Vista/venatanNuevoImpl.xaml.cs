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
    /// Lógica de interacción para ventanaAdminImpl.xaml
    /// </summary>
    public partial class ventanaAdminImpl : Window
    {
        private Entorno entorno;
        private static ventanaAdminImpl instance;

        private ventanaAdminImpl()
        {
            InitializeComponent();
        }

        public static ventanaAdminImpl GetInstance()
        {
            if (instance == null)
                instance = new ventanaAdminImpl();

            return instance;
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Chbox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cmbox_Sigla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
