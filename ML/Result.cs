using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    //primitivo
    //bool, int, string, decimal


    public class Result //Complejo
    {
        public string ErrorMessage { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; }

        public Exception Ex { get; set; }

        public bool Correct { get; set; }
    }
}
