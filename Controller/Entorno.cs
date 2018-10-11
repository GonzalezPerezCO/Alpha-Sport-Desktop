using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deportes_WPF.Model;
using System.Diagnostics;
using System.Data;

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

        public readonly string PROYECTO = "Sistema Integrado Deportes de la Escuela";
        public readonly string GIMNASIO = "Sistema de Gimnasio";
        public readonly string DEPORTES = "Sistema de Deportes";
        public string PERIODO = "Periodo Académico";

        private static Entorno instance = null;
        private ConnectionClass connection;
        private User user;

        private Entorno() {
            connection = ConnectionClass.GetInstance();
            PERIODO = connection.getPeriodo();
        }

        public static Entorno GetInstance()
        {
            if (instance == null)
                instance = new Entorno();

            return instance;
        }

        public bool login(string email, string password) {            

            User result = connection.loginConnection(email, password);

            if (result != null)
            {
                setUser(result);
                Debug.WriteLine("User log: "+result.getNombreCompleto());
                return true;
            }
            else {
                return false;
            }
            
        }

        public List<string> asistencia(string codigo) {
            string query = "SELECT CONCAT(nombre, ' ', apellido) As nombre, testudiantes.codigo as codigo,  dia1, dia2, dia3, hora1, hora2, hora3 from testudiantes INNER JOIN thorarios on testudiantes.codigo = "+codigo+" and testudiantes.email = thorarios.email;";
            return connection.queryReader(query);
        }


        public DataTable mostrarTabla() {
           
            Debug.WriteLine("MOSTRAR TABLA");
            string query = "select nombre, apellido, codigo, carrera, semestre from testudiantes";
            DataTable dt = connection.queryTable(query);
            Debug.WriteLine("RECIBIR READER EN TABLE");

            return dt;
        }

        public User getUser() {
            return user;
        }

        public void setUser(User user) {
            this.user = user;
        }

    }
}
