using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("RolGetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            result.Objects = new List<object>();
                            while (reader.Read())
                            {
                                ML.Rol rol = new ML.Rol();
                                
                                rol.IdRol = reader.GetByte(0);
                                rol.Nombre = reader.GetString(1);
                                result.Objects.Add(rol);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron roles.";
                        }
                    }
                }


            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetAllEFSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var rolesDB = context.RolGetAll().ToList();
                    if(rolesDB.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(var rolDB in rolesDB) 
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.IdRol = rolDB.IdRol;
                            rol.Nombre = rolDB.Nombre;
                            result.Objects.Add(rol);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se encontraron roles";
                    }

                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAllEFLQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from rolDB in context.Rols
                                 select new { rolDB.IdRol, rolDB.Nombre});
                    result.Objects = new List<object>();
                    if(query != null && query.ToList().Count > 0)
                    {
                        foreach(var item in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.Nombre = item.Nombre;
                            rol.IdRol = item.IdRol;
                            result.Objects.Add(rol);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron roles";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
    
