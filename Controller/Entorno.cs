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
    static class Entorno
    {

        private static ConnectionClass connection;


        public static bool login() {
            bool result;
            connection = new ConnectionClass();

            connection.Initialize();

            try
            {
            }
            catch
            {
            }

            
        }

    }
}
