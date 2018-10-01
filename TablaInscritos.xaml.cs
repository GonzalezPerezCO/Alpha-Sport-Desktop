using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace Deportes_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class TablaInscritos : Window
    {
        string query;
        MySqlCommand cmd;
        MySqlDataReader reader;


        public TablaInscritos()
        {
            InitializeComponent();

            mostrarTabla();
        }


        public void mostrarTabla() {
            query = "select nombre, apellido, codigo, carrera, semestre from testudiantes";
            
            reader = 

            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dtgrid1.ItemsSource = dt.DefaultView;
            }
            else {
                MessageBox.Show(reader.ToString());
            }


            connection.Close();
        }

      
    }
}
