using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Conexion
    {
        public static string Get()
        {
            string connection = ConfigurationManager.ConnectionStrings["LBlancasProgramacionNCapas"].ConnectionString;
            return connection;
        }
    }
}

