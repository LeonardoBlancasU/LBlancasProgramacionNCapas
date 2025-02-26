using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario {  get; set; }
        public string Nombre { get; set; }
        public byte Edad { get; set; }
        public string Direccion { get; set; }
        public string Curp { get; set; }

        public decimal Costo {  get; set; }

    }
}
