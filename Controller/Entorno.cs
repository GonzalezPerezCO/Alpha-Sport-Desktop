﻿using System;
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
    internal class Entorno
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

        public List<string> disponiblesCasilleros()
        {
            string query = "SELECT GROUP_CONCAT(id_c) FROM tcasilleros WHERE disponible = 1";
            return connection.listaUnicaReader(query);
        }

        public List<string> carreras()
        {
            string query = "SELECT GROUP_CONCAT(carrera) FROM tcarreras;";
            return connection.listaUnicaReader(query);
        }

        public List<string> buscarCasilleroEstu(int codigo)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido), tcasilleros.codigo, id_c , disponible, entrada, salida FROM tcasilleros JOIN testudiantes on testudiantes.codigo =  tcasilleros.codigo WHERE tcasilleros.codigo = "+codigo+ " LIMIT 1;";
            return connection.buscarCasilleroReader(query);
        }

        public List<string> buscarCasilleroID(int casillero)
        {
            string query = "SELECT CONCAT(nombre, ' ', apellido), tcasilleros.codigo, id_c , disponible, entrada, salida FROM tcasilleros JOIN testudiantes on testudiantes.codigo =  tcasilleros.codigo WHERE tcasilleros.id_c = "+casillero+" LIMIT 1;";
            return connection.buscarCasilleroReader(query);
        }

        public List<List<string>> infoCasilleros()
        {
            // unico string con: id_c, disponible, seccion
            string queryDisp = "SELECT disponible FROM tcasilleros ORDER by id_c;";
            string querySecc = "SELECT seccion FROM tcasilleros ORDER by id_c;";
            return connection.casillerosDisponiblesReader(queryDisp, querySecc);
        }

        public void agregarEstudianteCasillero(int casillero, int codigo)
        {
            string query = "UPDATE tcasilleros SET disponible = 0, codigo = "+codigo+", entrada =  NOW() WHERE id_c = "+casillero+";";
            connection.queryExecute(query);
        }

        public void quitarEstudianteCasillero(int codigo)
        {
            string query = "UPDATE tcasilleros SET disponible = 1, codigo = NULL, entrada = '2018-01-01 00:00:00', salida = '2018-01-01 00:00:00' WHERE codigo = " + codigo + ";";
            connection.queryExecute(query);
        }

        public void agregarEstudiante(string nombre, string apellido, int codigo, int documento, string carrera, int semestre, string email, string observacion)
        {
            // nombres, apellidos, codigo, documento, carrera, semestre, email, obs
            string query = "call addEstudFull('" + nombre + "', '" + apellido + "', " + codigo + ", " + documento + ", '" + carrera + "', " + semestre + ", '" + email + "', '" + observacion + "');";
            connection.queryExecute(query);
        }

        public string fallas(int codigo)
        {
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
            string query = "SELECT nombre as NOMBRE, codigo as CODIGO, cantidad as CANTIDAD, disponibles as DISPONIBLES, prestados as PRESTADOS, no_devueltos as PERDIDOS FROM  timplementos;";
            DataTable dt = connection.mostrarTabla(query);
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

        public DataTable mostrarCupos()
        {
            Debug.WriteLine("MOSTRAR CUPOS");
            string query = "select id as Hora, lunes as Lunes, martes as Martes, miercoles as Miercoles, jueves as Jueves, viernes as Viernes from tcupos";
            DataTable dt = connection.mostrarTabla(query);
            Debug.WriteLine("RECIBIR READER EN CUPOS");

            return dt;
        }       

        public bool buscarEstudiante(int codigo, string email) {
            return connection.buscarEstudiante(codigo, email);
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
