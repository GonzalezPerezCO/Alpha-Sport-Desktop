using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * CLASE PARA TENER EL USER DEL ADMIN PARA CONTROLAR EL LOGIN
 * */
namespace Deportes_WPF.Model
{
    class User
    {
        private string email;
        private string nombre;
        private string apellido;


        public User(string email = "", string nombre = "", string apellido = "") {
            this.email = email;
            this.nombre = nombre;
            this.apellido = apellido;
        }

        public string getEmail() {
            return email;
        }

        public string getNombre() {
            return nombre;
        }

        public string getApellido(){
            return apellido;
        }

    }
}
