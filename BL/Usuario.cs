
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;



namespace BL
{
    public class Usuario
    {
        //Metodos Add, Update y Delete con Query, es decir sin Stored Procedures
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Usuario]([Nombre],[Edad],[Direccion],[Curp],[Costo])VALUES (@Nombre, @Edad, @Direccion, @Curp, @Costo)", conn);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Costo", usuario.Costo);
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
            }

            return result;


        }


        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Usuario] SET Nombre = @Nombre, Edad = @Edad, Curp = @Curp, Direccion = @Direccion, Costo = @Costo WHERE IdUsuario = @IdUsuario", conn);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Costo", usuario.Costo);
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
            }

            return result;


        }

        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Usuario] WHERE IdUsuario = @IdUsuario", conn);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                  
                        result.Correct = false;
                    }
                }
           
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;


        }

        //Metodos Add, Update y Delete con Stored Procedures
        public static ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioAdd", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Costo", usuario.Costo);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
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
                    SqlCommand cmd = new SqlCommand("UsuarioUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Costo", usuario.Costo);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
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
            }

            return result;


        }

        public static ML.Result DeleteSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioDelete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else

                        result.Correct = false;
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;


        }

        //Metodos GetAll y GetById con Reader
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioGetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
            
                            result.Objects = new List<object>();
                            while (reader.Read())
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol = new ML.Rol();
                                usuario.IdUsuario = reader.GetInt32(0);
                                usuario.Nombre = reader.GetString(1);
                                usuario.Edad = reader.GetByte(2);
                                usuario.Direccion = reader.GetString(3);
                                usuario.Curp = reader.GetString(4);
                                usuario.Costo = reader.GetDecimal(5);
                                usuario.Rol.IdRol = reader.GetByte(6);

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron registros.";
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

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioGetByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.IdUsuario = reader.GetInt32(0);
                            usuario.Nombre = reader.GetString(1);
                            usuario.Edad = reader.GetByte(2);
                            usuario.Direccion = reader.GetString(3);
                            usuario.Curp = reader.GetString(4);
                            usuario.Costo = reader.GetDecimal(5);
                            usuario.Rol.IdRol = reader.GetByte(6);
                            result.Object = usuario;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron el usuario.";
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

        //Metodos GetAll y GetById con Adapter
        public static ML.Result GetAllAdapter()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioGetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    { result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.Nombre = (row[1].ToString());
                            usuario.Edad = Convert.ToByte(row[2].ToString());
                            usuario.Direccion = (row[3].ToString());
                            usuario.Curp = (row[4].ToString());
                            usuario.Costo = Convert.ToDecimal(row[5].ToString());
                            usuario.Rol.IdRol = Convert.ToByte(row[6].ToString());
                            result.Objects.Add(usuario);
                        }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron registros.";
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
        public static ML.Result GetByIdAdapter(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conn = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand("UsuarioGetByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);
                    
                        if (dataTable.Rows.Count >0)
                        {
                        DataRow row = dataTable.Rows[0];
                            
                            
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.Nombre = Convert.ToString(row[1].ToString());
                            usuario.Edad = Convert.ToByte(row[2].ToString());
                            usuario.Direccion = Convert.ToString(row[3].ToString());
                            usuario.Curp = Convert.ToString(row[4].ToString());
                            usuario.Costo = Convert.ToDecimal(row[5].ToString());
                            usuario.Rol.IdRol = Convert.ToByte(row[6].ToString());
                            result.Object = usuario;
                            result.Correct = true;

                        
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontro el usuario.";
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
    }
    }

