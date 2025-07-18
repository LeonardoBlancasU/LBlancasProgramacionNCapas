using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace BL
{
    public class Direccion
    {
        public static ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DireccionAdd", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Calle", usuario.Direccion.Calle);
                    cmd.Parameters.AddWithValue("@NumeroExterior", usuario.Direccion.NumeroExterior);
                    cmd.Parameters.AddWithValue("@NumeroInterior", usuario.Direccion.NumeroInterior);
                    cmd.Parameters.AddWithValue("@IdColonia", usuario.Direccion.Colonia.IdColonia);

                    SqlParameter outIdDireccion = new SqlParameter("IdDireccion", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outIdDireccion);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Object = (int)outIdDireccion.Value;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result UpdateSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DireccionUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDireccion", usuario.Direccion.IdDireccion);
                    cmd.Parameters.AddWithValue("@Calle", usuario.Direccion.Calle);
                    cmd.Parameters.AddWithValue("@NumeroExterior", usuario.Direccion.NumeroExterior);
                    cmd.Parameters.AddWithValue("@NumeroInterior", usuario.Direccion.NumeroInterior);
                    cmd.Parameters.AddWithValue("@IdColonia", usuario.Direccion.Colonia.IdColonia);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result GetById(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            int? IdDireccion = usuario.Direccion.IdDireccion;

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DireccionGetById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDireccion", IdDireccion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        usuario.Direccion.Calle = (row["Calle"].ToString());
                        usuario.Direccion.NumeroExterior = (row["NumeroExterior"].ToString());
                        usuario.Direccion.NumeroInterior = (row["NumeroInterior"].ToString());
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = Convert.ToInt32((row["IdColonia"].ToString()));

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro la Direccion.";
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

        public static ML.Result DeleteSP(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DireccionDelete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDireccion", IdDireccion);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result AddEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    ObjectParameter idDireccion = new ObjectParameter("IdDireccion", typeof(int));
                    var rowsAffected = context.DireccionAdd(usuario.Direccion.Calle, usuario.Direccion.NumeroExterior, usuario.Direccion.NumeroInterior, usuario.Direccion.Colonia.IdColonia, idDireccion);
                    if (Convert.ToInt32(rowsAffected) > 0)
                    {
                        result.Object = (int)idDireccion.Value; 
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result UpdateEFSP(ML.Usuario usuario) 
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var rowsAffected = context.DireccionUpdate(usuario.Direccion.IdDireccion, usuario.Direccion.Calle, usuario.Direccion.NumeroExterior, usuario.Direccion.NumeroInterior, usuario.Direccion.Colonia.IdColonia);
                    
                    if (rowsAffected > 0)
                    {
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar la direccion";
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

        public static ML.Result DeleteEFSP(int IdUsuario) 
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var rowsAffected = context.DireccionDelete(IdUsuario);
                    if (rowsAffected > 0)
                    {
                        result.Correct=true;
                    }
                    else 
                    { 
                        result.Correct = false;
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

        public static ML.Result GetByIdEFSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try 
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var direccionDB = context.DireccionGetById(IdUsuario).FirstOrDefault();
                    if(direccionDB != null)
                    {
                        ML.Direccion direccion = new ML.Direccion();
                        direccion = new ML.Direccion();
                        direccion.Calle = direccionDB.Calle;
                        direccion.NumeroExterior = direccionDB.NumeroExterior;
                        direccion.NumeroInterior = direccionDB.NumeroInterior;
                        direccion.Colonia = new ML.Colonia();
                        direccion.Colonia.IdColonia = (int)direccionDB.IdColonia;
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
        public static ML.Result AddEFLQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try 
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    DL_EF.Direccion direccionDL = new DL_EF.Direccion();
                    direccionDL.Calle = usuario.Direccion.Calle;
                    direccionDL.NumeroExterior = usuario.Direccion.NumeroExterior;
                    direccionDL.NumeroInterior = usuario.Direccion.NumeroInterior;
                    direccionDL.IdColonia = usuario.Direccion.Colonia.IdColonia;
                    context.Direccions.Add(direccionDL);
                    int rowsAffected = context.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Object = direccionDL.IdDireccion;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result UpdateEFLQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from direccionDB in context.Direccions
                                 where direccionDB.IdDireccion == usuario.Direccion.IdDireccion
                                 select direccionDB).SingleOrDefault();
                    if (query != null)
                    {
                        query.IdDireccion = usuario.Direccion.IdDireccion.Value;
                        query.Calle = usuario.Direccion.Calle;
                        query.NumeroExterior = usuario.Direccion.NumeroExterior;
                        query.NumeroInterior = usuario.Direccion.NumeroInterior;
                        query.IdColonia = usuario.Direccion.Colonia.IdColonia;
                        var rowsAffected = context.SaveChanges();
                        if (rowsAffected != null)
                        {
                            result.Correct = true;
                            result.Object = query.IdDireccion;
                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro la direccion";
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
        public static ML.Result DeleteEFLQ(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from direccionDB in context.Direccions
                                 where direccionDB.IdDireccion == IdDireccion
                                 select direccionDB).First();
                    context.Direccions.Remove(query);
                    int rowsAffected = context.SaveChanges();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;             
                    }
                    else
                    {
                        result.Correct = false;
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
