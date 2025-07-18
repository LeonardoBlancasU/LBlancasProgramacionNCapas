using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UsuarioDireccion
    {
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    int rowsAffected = context.UsuarioDireccionDelete(IdUsuario);
                    if(rowsAffected >=1) 
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se pudo eliminar el Usuario y Direccion";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
