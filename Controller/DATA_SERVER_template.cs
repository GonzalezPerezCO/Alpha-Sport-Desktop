using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaSport.Controller
{
    class DATA_SERVER_template
    {
        private readonly string server = "estudiantes.is.escuelaing.edu.co";
        private readonly string database = "deportes";
        private readonly string user = "deportes";
        private readonly string password = "deportes20182";
        private readonly string port = "3306";
        private readonly string sslM = "none";

        public DATA_SERVER_template()
        {
            Debug.WriteLine("Read Data Server... \n Connection DATA from DATA_SERVER");
        }

        public string GET_server() { return server; }

        public string GET_database() { return database; }

        public string GET_user() { return user; }

        public string GET_password() { return password; }

        public string GET_port() { return port; }

        public string GET_sslM() { return sslM; }
    }
}