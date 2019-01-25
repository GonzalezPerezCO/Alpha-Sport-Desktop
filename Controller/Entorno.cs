using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaSport.Model;
using System.Diagnostics;
using System.Data;
using System.Windows;

// using R_Connection = Deportes_WPF.Controller.ConnectionClass;

namespace AlphaSport.Controller
{
    /**
     * CLASE PARA TENER PERIODO
     * CONSULTAR: CUPOS, HORARIOS, ESTUDIANTES
     * 
     * */
    internal class Entorno
    {

        public readonly string PROYECTO = "Alpha Sport";
        public readonly string GIMNASIO = "Alpha Sport: Sistema de Gimnasio";
        public readonly string DEPORTES = "Alpha Sport: Sistema de Deportes";
        public string PERIODO = "Periodo Académico";

        public readonly List<string> DIAS = new List<string> { "LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES" };
        public readonly List<string> HORAS = new List<string> { "N/A", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };

        private static Entorno instance = null;
        private ConnectionClass connection;
        private User user;

        private Entorno() {
            connection = ConnectionClass.GetInstance();
            PERIODO = connection.GetPeriodo();
        }



        public static Entorno GetInstance()
        {
            if (instance == null)
                instance = new Entorno();

            return instance;
        }

        public void LogOut(Window ventana)
        {
            user = null;

            Window login = new Login();

            ventana.Hide();
            login.Show();
        }

        public bool Login(string email, string password) {

            User result = connection.LoginConnection(email, password);

            if (result != null)
            {
                SetUser(result);
                Debug.WriteLine("User log: " + result.getNombreCompleto());
                return true;
            }
            else {
                return false;
            }

        }

        public List<string> CalcularHoy()
        {
            List<string> res = new List<string>();

            // calcular dia y hora actual
            DateTime dt = DateTime.Now;
            string diaActual = AEspanol(dt.DayOfWeek.ToString().ToUpper());
            string horaActual = dt.Hour.ToString(); ;
            // -- fin
            Debug.WriteLine("<<<<<<<<<<<<< datos hora: " + diaActual + "a las  " + horaActual);

            res.Add(diaActual);
            res.Add(horaActual);

            return res; // diaActual y horaActual
        }

        private string AEspanol(string day)
        {
            string dia = "";

            switch (day)
            {
                case "MONDAY":
                    dia = DIAS[0];
                    break;
                case "TUESDAY":
                    dia = DIAS[1];
                    break;
                case "WEDNESDAY":
                    dia = DIAS[2];
                    break;
                case "THURSDAY":
                    dia = DIAS[3];
                    break;
                case "FRIDAY":
                    dia = DIAS[4];
                    break;
                /*case "SATURDAY":  // NO FUNCIONA PORQUE NO HAY DÍAS DE GIMNASIO LOS SABADOS Y DOMINGOS
                    dia = "SABADO";
                    break;
                case "SUNDAY":
                    dia = "DOMINGO";
                    break;*/
                default:  // NO ESA NECESARIO PORQUÉ YA ESTA COMTEMPLADO EN LA LECTURA DEL QUERY
                    dia = "LUNES";
                    break;
            }

            return dia;
        }

        public List<string> Asistencia(UInt64 codigo)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido) As nombre, carrera, testudiantes.email as email, semestre, fallas, asistencias, GROUP_CONCAT(dia, ',', hora) as horario from testudiantes INNER JOIN thorarios on testudiantes.codigo = "+codigo+" and testudiantes.email = thorarios.email GROUP by nombre;";
            return connection.AsistenciaReader(query);
        }

        public List<string> DatosEstudiante(UInt64 codigo)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido) As nombre, carrera, email, semestre, fallas, asistencias, codigo from testudiantes WHERE codigo = "+codigo+";";
            return connection.DatosEstuReader(query);
        }

        public List<string> DisponiblesCasilleros()
        {
            string query = "SELECT GROUP_CONCAT(id_c) FROM tcasilleros WHERE disponible = 1";
            return connection.ListaUnicaReader(query);
        }

        public List<string> Cupos()
        {
            string query = "SELECT GROUP_CONCAT(id,',',Lunes,',',Martes,',',Miercoles,',',Jueves,',',Viernes) FROM tcupos LIMIT 1;";
            return connection.ListaUnicaReader(query);
        }

        public List<string> Carreras()
        {
            string query = "SELECT GROUP_CONCAT(carrera) FROM tcarreras;";
            return connection.ListaUnicaReader(query);
        }

        public List<string> BuscarCasilleroEstu(UInt64 codigo)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido), tcasilleros.codigo, id_c , disponible, entrada, salida FROM tcasilleros JOIN testudiantes on testudiantes.codigo =  tcasilleros.codigo WHERE tcasilleros.codigo = "+codigo+ " LIMIT 1;";
            return connection.BuscarCasilleroReader(query);
        }

        public List<string> BuscarCasilleroID(int casillero)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido), tcasilleros.codigo, id_c , disponible, entrada, salida FROM tcasilleros JOIN testudiantes on testudiantes.codigo =  tcasilleros.codigo WHERE tcasilleros.id_c = "+casillero+" LIMIT 1;";
            return connection.BuscarCasilleroReader(query);
        }

        public List<List<string>> InfoCasilleros()
        {
            // unico string con: id_c, disponible, seccion
            string queryDisp = "SELECT disponible FROM tcasilleros ORDER by id_c;";
            string querySecc = "SELECT seccion FROM tcasilleros ORDER by id_c;";
            return connection.CasillerosDisponiblesReader(queryDisp, querySecc);
        }

        public void AgregarEstudianteCasillero(int casillero, UInt64 codigo)
        {
            string query = "UPDATE tcasilleros SET disponible = 0, codigo = "+codigo+", entrada =  NOW() WHERE id_c = "+casillero+";";
            connection.QueryExecute(query);
        }

        public void QuitarEstudianteCasillero(UInt64 codigo)
        {
            string query = "UPDATE tcasilleros SET disponible = 1, codigo = NULL, entrada = '2018-01-01 00:00:00', salida = '2018-01-01 00:00:00' WHERE codigo = " + codigo + ";";
            connection.QueryExecute(query);
        }

        public void AgregarEstudiante(string nombre, string apellido, UInt32 reserva, UInt64 codigo, UInt64 documento, string carrera, UInt32 semestre, string email, string observacion, bool examen)
        {
            // nombres, apellidos, codigo, documento, carrera, semestre, email, obs
            string query = "call addEstudFull('" + nombre + "', '" + apellido + "', "+ reserva + ", " + codigo + ", " + documento + ", '" + carrera + "', " + semestre + ", '" + email + "', '" + observacion + "', "+examen+" );";
            connection.QueryExecute(query);
        }

        public List<string> Fallas(UInt64 codigo, string concepto, string mensaje)
        {
            //string query = "UPDATE testudiantes SET fallas = fallas+1 WHERE codigo="+codigo+ "; SELECT fallas from testudiantes WHERE codigo="+codigo+"; ";
            string query = "CALL fallaYasistencia("+codigo+", '"+concepto+"', '"+mensaje+ "'); SELECT fallas, asistencias from testudiantes WHERE codigo=" + codigo + "; ";
            
            List<string> result = connection.QuerySumarAsistencia(query);
            
            return result;
        }

        public DataTable TablaInscritos()
        {
            Debug.WriteLine("MOSTRAR TABLA INSCRITOS");
            string query = "SELECT sigla AS SIGLA, codigo AS CODIGO, prestados AS CANT, entrada AS Entrada, observacion AS Observaciones FROM tprestamos;";
            DataTable dt = connection.MostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE INSCRITOS");

            return dt;
        }

        public DataTable TablaPrestamos()
        {
            Debug.WriteLine("MOSTRAR TABLA PRESTAMOS");
            string query = "";
            DataTable dt = connection.MostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE INSCRITOS");

            return dt;
        }

        public DataTable TablaHorarioGym(string dia)
        {
            Debug.WriteLine("MOSTRAR TABLA HORARIO_GYM");
            string query = "SELECT hora AS TURNO, codigo AS CODIGO,  CONCAT(nombre, ' ', apellido) AS NOMBRE, carrera AS CARRERA, semestre AS SEMESTRE, fallas AS FALLAS, asistencias AS ASIST FROM testudiantes INNER JOIN thorarios on testudiantes.email = thorarios.email   WHERE dia = '"+dia+"' ORDER by hora;";
            DataTable dt = connection.MostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE HORARIO_GYM");

            return dt;
        }

        public DataTable TablaImplementos()
        {
            Debug.WriteLine("MOSTRAR TABLA INSCRITOS");
            string query = "SELECT nombre as NOMBRE, codigo as CODIGO, cantidad as CANTIDAD, disponibles as DISPONIBLES, prestados as PRESTADOS, no_devueltos as PERDIDOS FROM  timplementos;";
            DataTable dt = connection.MostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE IMPLEMENTOS");

            return dt;
        }

        /*public DataTable horarioEstudiante(int codigo)
        {
            Debug.WriteLine("MOSTRAR TABLA HORARIO ESTUDIANTE");
            string query = "SELECT GROUP_CONCAT(dia, ',', hora) as horario from testudiantes INNER JOIN thorarios on testudiantes.codigo = "+codigo+" and testudiantes.email = thorarios.email GROUP by nombre;";
            DataTable dt = connection.mostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN TABLE");

            return dt;
        }*/

        public DataTable MostrarCupos()
        {
            Debug.WriteLine("MOSTRAR CUPOS");
            string query = "select id as Hora, lunes as Lunes, martes as Martes, miercoles as Miercoles, jueves as Jueves, viernes as Viernes from tcupos";
            DataTable dt = connection.MostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN CUPOS");

            return dt;
        }       

        public bool BuscarEstudiante(UInt64 codigo, string email) {
            return connection.BuscarEstudiante(codigo, email);
        }
        
        public string CambiarHorario(int hora, string dia, string email)
        {
            return connection.CambiarHorario(hora, dia, email);
        }

        public User GetUser() {
            return user;
        }

        public void SetUser(User user) {
            this.user = user;
        }
    }
}
