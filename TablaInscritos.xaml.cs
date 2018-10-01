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
using Deportes_WPF.Controller;
using MySql.Data.MySqlClient;

namespace Deportes_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class TablaInscritos : Window
    {
        private string query;
        private MySqlCommand cmd;
        private MySqlDataReader reader;

        private Entorno entorno;
        private ConnectionClass connection;

        public TablaInscritos()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
            connection = ConnectionClass.GetInstance();
            mostrarTabla();
        }


        public void mostrarTabla() {
            query = "select nombre, apellido, codigo, carrera, semestre from testudiantes";

            reader = connection.queryTable(query);

            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dtgrid1.ItemsSource = dt.DefaultView;
            }
            else {
                MessageBox.Show(reader.ToString());
            }
        }

      
    }
}
