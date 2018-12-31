using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes_WPF.Model
{
    internal class Periodo
    {
        private int id;
        private string nombre;

        public Periodo(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public int getId() {
            return id;
        }

        public string getNombre() {
            return nombre;
        }

    }
}
