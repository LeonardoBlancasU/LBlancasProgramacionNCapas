
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;



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
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
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
                result.Ex = ex;
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
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Usuario] SET Nombre = @Nombre, UserName = @UserName, ApellidoPaterno = @ApellidoPaterno, \r\nCURP = @CURP, ApellidoMaterno = @ApellidoMaterno, IdRol = @IdRol,\r\nEmail = @Email, Password = @Password, FechaNacimiento = @FechaNacimiento, Sexo = @Sexo, Telefono = @Telefono, Celular = @Celular, Estatus = @Estatus, Imagen = @Imagen", conn);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNaciemiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
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
                result.Ex = ex;
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
                result.Ex = ex;
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
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
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
                    SqlCommand cmd = new SqlCommand("UsuarioUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
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
                result.Ex = ex;

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
                                usuario.CURP = reader.GetString(2);
                                usuario.Rol.IdRol = reader.GetByte(3);
                                usuario.UserName = reader.GetString(4);
                                usuario.ApellidoPaterno = reader.GetString(5);
                                usuario.ApellidoMaterno = reader.GetString(6);
                                usuario.Email = reader.GetString(7);
                                usuario.Password = reader.GetString(8);
                                usuario.FechaNacimiento = reader.GetDateTime(9).ToString("yyyy-MM-dd");
                                usuario.Sexo = reader.GetString(10);
                                usuario.Telefono = reader.GetString(11);
                                usuario.Celular = reader.GetString(12);
                                usuario.Estatus = reader.GetBoolean(13);
                                if (reader["Imagen"] != DBNull.Value)
                                {
                                    usuario.Imagen = (byte[])reader["Imagen"];
                                }
                                usuario.Rol.Nombre = reader.GetString(15);

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
                result.Ex = ex;
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
                            usuario.CURP = reader.GetString(2);
                            usuario.Rol.IdRol = reader.GetByte(3);
                            usuario.UserName = reader.GetString(4);
                            usuario.ApellidoPaterno = reader.GetString(5);
                            usuario.ApellidoMaterno = reader.GetString(6);
                            usuario.Email = reader.GetString(7);
                            usuario.Password = reader.GetString(8);
                            usuario.FechaNacimiento = reader.GetString(9);
                            usuario.Sexo = reader.GetString(10);
                            usuario.Telefono = reader.GetString(11);
                            usuario.Celular = reader.GetString(12);
                            usuario.Estatus = reader.GetBoolean(13);
                            if (reader["Imagen"] != DBNull.Value)
                            {
                                usuario.Imagen = (byte[])reader["Imagen"];
                            }
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
                result.Ex = ex;
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
                            usuario.UserName = (row[2].ToString());
                            usuario.ApellidoPaterno = (row[3].ToString()); 
                            usuario.ApellidoMaterno = (row[4].ToString()); 
                            usuario.Email = (row[5].ToString()); 
                            usuario.Password = (row[6].ToString()); 
                            usuario.FechaNacimiento = (row[7].ToString()); 
                            usuario.Sexo = (row[8].ToString()); 
                            usuario.Telefono = (row[9].ToString()); 
                            usuario.Celular = (row[10].ToString()); 
                            usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                            usuario.CURP = (row[12].ToString()); ;
                            usuario.Imagen = row["Imagen"] != DBNull.Value ? (byte[])row["Imagen"] : null;
                            usuario.Rol.IdRol = Convert.ToByte(row[14].ToString());
                            usuario.Rol.Nombre = row[15].ToString();
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
                result.Ex = ex;
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
                            usuario.IdUsuario = Convert.ToInt32(row["IdUsuario"].ToString());
                            usuario.Nombre = (row["Nombre"].ToString());
                            usuario.UserName = (row["UserName"].ToString());
                            usuario.ApellidoPaterno = (row["ApellidoPaterno"].ToString()); 
                            usuario.ApellidoMaterno = (row["ApellidoMaterno"].ToString()); 
                            usuario.Email = (row["Email"].ToString()); 
                            usuario.Password = (row["Password"].ToString()); 
                            usuario.FechaNacimiento = (row["FechaNacimiento"].ToString()); 
                            usuario.Sexo = (row["Sexo"].ToString()); 
                            usuario.Telefono = (row["Telefono"].ToString()); 
                            usuario.Celular = (row["Celular"].ToString()); 
                            usuario.Estatus = Convert.ToBoolean(row["Estatus"]);
                            usuario.CURP = (row["CURP"].ToString());
                            usuario.Imagen = row["Imagen"] != DBNull.Value ? (byte[])row["Imagen"] : null;
                            usuario.Rol.IdRol = Convert.ToByte(row["IdRol"].ToString());
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
                result.Ex = ex;
            }

            return result;
        }
    }
    }

