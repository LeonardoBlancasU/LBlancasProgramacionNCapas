using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Direccion
    {
        public int? IdDireccion { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name ="Calle")]
        public string Calle { get; set; }
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name ="Numero Exterior")]
        public string NumeroExterior { get; set; }
        [Display(Name ="Numero Interior (Opcional)")]
        public string NumeroInterior { get; set; }
        public ML.Colonia Colonia { get; set; }
    }
}
