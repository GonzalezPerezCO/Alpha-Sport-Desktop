using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * CLASE PARA TENER EL USER DEL ADMIN PARA CONTROLAR EL LOGIN
 * */
namespace AlphaSport.Model
{
    internal class User
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

        public string getNombreCompleto() {
            return nombre + " " + apellido;
        }

        public override string ToString()
        {            
            return "Email: " + email + ", Nombre: " + nombre + ", Apellido: " + apellido;
        }
    }
}
