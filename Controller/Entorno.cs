using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deportes_WPF.Model;
using System.Diagnostics;

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
        private User user;

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

            User result = connection.loginConnection(email, password);

            if (result != null)
            {
                setUser(result); Debug.WriteLine("User log: "+result.getNombreCompleto());
                return true;
            }
            else {
                return false;
            }
            
        }

        public User getUser() {
            return user;
        }

        public void setUser(User user) {
            this.user = user;
        }

    }
}
