using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

// using R_Connection = Deportes_WPF.Controller.ConnectionClass;

namespace Deportes_WPF.Controller
{
    /**
     * CLASE PARA TENER PERIODO
     * CONSULTAR: CUPOS, HORARIOS, ESTUDIANTES
     * 
     * */
    class Entorno
    {

        private ConnectionClass connection;

        public Entorno() {

        }


        public bool login(string email, string password) {
            
            connection = new ConnectionClass();
            connection.Initialize();

            try
            {
                connection.OpenConnection();                
            }
            catch (Exception e)
            {
                //System.Windows.MessageBox.Show("Problemas al iniciar sesión + e");
                return false;
            }

            return connection.loginConnection(email, password);
            
        }

    }
}
