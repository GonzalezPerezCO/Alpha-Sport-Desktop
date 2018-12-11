using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deportes_WPF.Model;
using System.Diagnostics;
using System.Data;
using System.Windows;

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

        public readonly string PROYECTO = "Alpha Sport";
        public readonly string GIMNASIO = "Alpha Sport: Sistema de Gimnasio";
        public readonly string DEPORTES = "Alpha Sport: Sistema de Deportes";
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

        public void logOut(Window ventana)
        {
            user = null;

            Window login = new Login();

            ventana.Hide();
            login.Show();

            // falta usar en las venatnas
        }

        public bool login(string email, string password) {

            User result = connection.loginConnection(email, password);

            if (result != null)
            {
                setUser(result);
                Debug.WriteLine("User log: " + result.getNombreCompleto());
                return true;
            }
            else {
                return false;
            }

        }

        public List<string> asistencia(int codigo)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido) As nombre, carrera, semestre, fallas, testudiantes.codigo as codigo, GROUP_CONCAT(dia, ',', hora) as horario from testudiantes INNER JOIN thorarios on testudiantes.codigo = "+codigo+" and testudiantes.email = thorarios.email GROUP by nombre;";
            return connection.asistenciaReader(query);
        }

        public void agregarEstudiante(string nombre, string apellido, int codigo, string carrera, int semestre, string email, string observacion)
        {
            string query = "call addEstudFull('" + nombre + "', '" + apellido + "', " + codigo + ", '" + carrera + "', " + semestre + ", '" + email + "', '" + observacion + "');";
            connection.queryAddEstuFull(query);
        }

        public string fallas(int codigo) {
            string query = "UPDATE testudiantes SET fallas = fallas+1 WHERE codigo="+codigo+ "; SELECT fallas from testudiantes WHERE codigo="+codigo+"; ";
            string result;
            List<string> lista = connection.querySumarAsistencia(query);
            result = lista[0];
            return result;
        }

        public DataTable tablaInscritos()
        {
            Debug.WriteLine("MOSTRAR TABLA INSCRITOS");
            string query = "select nombre, apellido, codigo, carrera, semestre from testudiantes;";
            DataTable dt = connection.mostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE INSCRITOS");

            return dt;
        }

        public DataTable tablaImplementos()
        {
            Debug.WriteLine("MOSTRAR TABLA INSCRITOS");
            string query = "select nombre, cantidad from timplementos;";
            DataTable dt = connection.mostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE IMPLEMENTOS");

            return dt;
        }

        public DataTable horarioEstudiante(int codigo)
        {
            Debug.WriteLine("MOSTRAR TABLA HORARIO ESTUDIANTE");
            string query = "SELECT GROUP_CONCAT(dia, ',', hora) as horario from testudiantes INNER JOIN thorarios on testudiantes.codigo = "+codigo+" and testudiantes.email = thorarios.email GROUP by nombre;";
            DataTable dt = connection.mostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE");

            return dt;
        }

        public DataTable mostrarCupos()
        {
            Debug.WriteLine("MOSTRAR CUPOS");
            string query = "select id as Hora, lunes as Lunes, martes as Martes, miercoles as Miercoles, jueves as Jueves, viernes as Viernes from tcupos";
            DataTable dt = connection.mostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN CUPOS");

            return dt;
        }       

        public bool buscarEstudiante() {
            return false;
        }
        
        public bool cambiarHorario()
        {
            return false;
        }

        public User getUser() {
            return user;
        }

        public void setUser(User user) {
            this.user = user;
        }
    }
}
