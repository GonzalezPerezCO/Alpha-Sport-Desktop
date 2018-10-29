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

        public List<string> asistencia(string codigo)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido) As nombre, carrera, semestre, fallas, testudiantes.codigo as codigo from testudiantes INNER JOIN thorarios on testudiantes.codigo = " + codigo + " and testudiantes.email = thorarios.email;";
            return connection.asistenciaReader(query);
        }
       

        public DataTable tablaInscritos()
        {
            Debug.WriteLine("MOSTRAR TABLA INSCRITOS");
            string query = "select nombre, apellido, codigo, carrera, semestre from testudiantes";
            DataTable dt = connection.mostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE INSCRITOS");

            return dt;
        }

        public DataTable horarioEstudiante(int codigo)
        {
            Debug.WriteLine("MOSTRAR TABLA HORARIO ESTUDIANTE");
            string query = "select  dia1 as Dia1, hora1 as Hora1, dia2 as Dia2, hora2 as Hora2, dia3 as Dia3, hora3 as Hora3 from testudiantes INNER JOIN thorarios on testudiantes.email = thorarios.email where testudiantes.codigo ="+codigo+"";
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

        public List<string> agregarEstudiante(int reserva, string nombre, string apellido, int codigo, string carrera, int semestre, string email, int documento, string password, string observacion) {
            string query = "cell addEstudFull("+ reserva + ", '"+ nombre + "', '"+ apellido + "', "+ codigo + ", '"+ carrera + "', "+ semestre + ", '"+ email + "', "+ documento + ", '"+password+"', '"+ observacion + "');";
            return connection.queryReader(query);
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
