using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Estado
    {
        [Required(ErrorMessage ="Este Campo es requerido")]
        [Display(Name ="Estado")]
        public byte IdEstado { get; set; }
        public string Nombre { get; set; }
        public List<object> Estados { get; set; }

    }
}
