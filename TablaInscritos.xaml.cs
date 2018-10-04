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
using Deportes_WPF.Controller;
using MySql.Data.MySqlClient;

namespace Deportes_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class TablaInscritos : Window
    {
        private ConnectionClass connection;
        private string query;

        public TablaInscritos()
        {
            InitializeComponent();
            connection = ConnectionClass.GetInstance();
            mostrarTabla();
        }


        public void mostrarTabla() {
            Debug.WriteLine("MOSTRAR TABLA");
            query = "select nombre, apellido, codigo, carrera, semestre from testudiantes";
            DataTable dt = connection.queryTable(query);
            Debug.WriteLine("RECIBIR READER EN TABLE");
           
            dtgrid1.ItemsSource = dt.DefaultView;
           
        }

      
    }
}
