
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
using System.Data.Entity.Core.Objects;
using ML;
using System.Globalization;
using DL_EF;



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
                    cmd.Parameters.AddWithValue("@IdDireccion", usuario.Direccion.IdDireccion);

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
                    cmd.Parameters.AddWithValue("@IdDireccion", usuario.Direccion.IdDireccion);
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
                    SqlParameter outIdDireccion = new SqlParameter("IdDireccion", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outIdDireccion);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        int IdDireccion = (int)outIdDireccion.Value;
                        result.Object = IdDireccion;
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
                                usuario.FechaNacimiento = reader.GetDateTime(9).ToString("dd-MM-yyyy");
                                usuario.Sexo = reader.GetString(10);
                                usuario.Telefono = reader.GetString(11);
                                usuario.Celular = reader.GetString(12);
                                usuario.Estatus = reader.GetBoolean(13);
                                if (reader["Imagen"] != DBNull.Value)
                                {
                                    usuario.Imagen = (byte[])reader["Imagen"];
                                }
                                usuario.Rol.Nombre = reader.GetString(15);
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = reader["Calle"] != DBNull.Value ? (reader["Calle"].ToString()) : "";
                                usuario.Direccion.NumeroExterior = reader["NumeroExterior"] != DBNull.Value ? (reader["NumeroExterior"].ToString()) : "";
                                usuario.Direccion.NumeroInterior = reader["NumeroInterior"] != DBNull.Value ? (reader["NumeroInterior"].ToString()) : "";
                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.Nombre = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                                usuario.Direccion.Colonia.CodigoPostal = reader["CodigoPostal"] != DBNull.Value ? (reader["CodigoPostal"].ToString()) : "";
                                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                                usuario.Direccion.Colonia.Municipio.Nombre = !reader.IsDBNull(21) ? reader.GetString(21) : "";
                                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                                usuario.Direccion.Colonia.Municipio.Estado.Nombre = !reader.IsDBNull(22) ? reader.GetString(22) : "";

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

                            usuario.IdUsuario = reader.GetInt32(0);
                            usuario.Nombre = reader.GetString(1);
                            usuario.CURP = reader.GetString(2);
                            usuario.Rol = new ML.Rol();
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
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = reader["IdDireccion"] != DBNull.Value ? Convert.ToInt32(reader["IdDireccion"]) : 0;
                            usuario.Direccion.Calle = reader["Calle"] != DBNull.Value ? (reader["Calle"].ToString()) : "";
                            usuario.Direccion.NumeroExterior = reader["NumeroExterior"] != DBNull.Value ? (reader["NumeroExterior"].ToString()) : "";
                            usuario.Direccion.NumeroInterior = reader["NumeroInterior"] != DBNull.Value ? (reader["NumeroInterior"].ToString()) : "";
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = reader["IdColonia"] != DBNull.Value ? Convert.ToInt32(reader["IdColonia"]) : 0;
                            usuario.Direccion.Colonia.Nombre = !reader.IsDBNull(20) ? reader.GetString(20) : "";
                            usuario.Direccion.Colonia.CodigoPostal = reader["CodigoPostal"] != DBNull.Value ? (reader["CodigoPostal"].ToString()) : "";
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = reader["IdMunicipio"] != DBNull.Value ? Convert.ToInt32(reader["IdMunicipio"]) : 0;
                            usuario.Direccion.Colonia.Municipio.Nombre = !reader.IsDBNull(23) ? reader.GetString(23) : "";
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = reader.GetByte(24);
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = !reader.IsDBNull(25) ? reader.GetString(25) : "";
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
                    {
                        result.Objects = new List<object>();

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

                    if (dataTable.Rows.Count > 0)
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
        public static ML.Result AddEFSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var rowsAffected = context.UsuarioAdd(usuario.Nombre, usuario.CURP, usuario.Rol.IdRol, usuario.UserName, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.Imagen, usuario.Direccion.IdDireccion);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Mo se pudo agregar el usuario";
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
                    var rowsAffected = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.CURP, usuario.Rol.IdRol, usuario.UserName, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.Imagen, usuario.Direccion.IdDireccion);
                    if(rowsAffected > 0 )
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

        public static ML.Result DeleteEFSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    ObjectParameter idDireccion = new ObjectParameter("IdDireccion", typeof(int));
                    var rowsAffected = context.UsuarioDelete(IdUsuario, idDireccion);

                    if (rowsAffected > 0)
                    {
                        result.Object = (int)idDireccion.Value;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el Usuario";
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
        public static ML.Result GetAllEFSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var listaUsuarios = context.UsuarioGetAll().ToList();
                    if (listaUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var usuarioDB in listaUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = usuarioDB.IdUsuario;
                            usuario.Nombre = usuarioDB.Nombre;
                            usuario.UserName = usuarioDB.UserName;
                            usuario.Email = usuarioDB.Email;
                            usuario.Password = usuarioDB.Password;
                            usuario.FechaNacimiento = usuarioDB.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.Sexo = usuarioDB.Sexo;
                            usuario.Telefono = usuarioDB.Telefono;
                            usuario.Celular = usuarioDB.Celular;
                            usuario.Estatus = usuarioDB.Estatus;
                            usuario.CURP = usuarioDB.CURP;
                            usuario.Imagen = usuarioDB.Imagen;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.Nombre = usuarioDB.NombreRol;
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Calle = usuarioDB.Calle;
                            usuario.Direccion.NumeroExterior = usuarioDB.NumeroExterior;
                            usuario.Direccion.NumeroInterior = usuarioDB.NumeroInterior;
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Nombre = usuarioDB.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = usuarioDB.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Nombre = usuarioDB.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = usuarioDB.NombreEstado;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = true;
                        result.ErrorMessage = "No se encontraron Usuarios";
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
                    var usuarioDB = context.UsuarioGetByID(IdUsuario).FirstOrDefault();
                    if (usuarioDB != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = usuarioDB.IdUsuario;
                        usuario.Nombre = usuarioDB.Nombre;
                        usuario.ApellidoPaterno = usuarioDB.ApellidoPaterno;
                        usuario.ApellidoMaterno = usuarioDB.ApellidoMaterno;
                        usuario.UserName = usuarioDB.UserName;
                        usuario.Email = usuarioDB.Email;
                        usuario.Password = usuarioDB.Password;
                        usuario.FechaNacimiento = usuarioDB.FechaNacimiento;
                        usuario.Sexo = usuarioDB.Sexo;
                        usuario.Telefono = usuarioDB.Telefono;
                        usuario.Celular = usuarioDB.Celular;
                        usuario.Estatus = usuarioDB.Estatus;
                        usuario.CURP = usuarioDB.CURP;
                        usuario.Imagen = usuarioDB.Imagen;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = Convert.ToByte(usuarioDB.IdRol);
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = (int)usuarioDB.IdDireccion;
                        usuario.Direccion.Calle = usuarioDB.Calle;
                        usuario.Direccion.NumeroExterior = usuarioDB.NumeroExterior;
                        usuario.Direccion.NumeroInterior = usuarioDB.NumeroInterior;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = (int)(usuarioDB.IdColonia);
                        usuario.Direccion.Colonia.Nombre = usuarioDB.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = usuarioDB.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = (int)usuarioDB.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = usuarioDB.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = (byte)usuarioDB.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = usuarioDB.NombreEstado;
                        result.Object = usuario;
                        result.Correct = true;
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
            Result result = new Result();
            try
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    DL_EF.Usuario usuarioDL = new DL_EF.Usuario();
                    usuarioDL.Nombre = usuario.Nombre;
                    usuarioDL.CURP = usuario.CURP;
                    usuarioDL.IdRol = usuario.Rol.IdRol;
                    usuarioDL.UserName = usuario.UserName;
                    usuarioDL.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioDL.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioDL.Email = usuario.Email;
                    usuarioDL.Password = usuario.Password;
                    usuarioDL.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
                    usuarioDL.Sexo = usuario.Sexo;
                    usuarioDL.Telefono = usuario.Telefono;
                    usuarioDL.Celular = usuario.Celular;
                    usuarioDL.Estatus = usuario.Estatus;
                    usuarioDL.Imagen = usuario.Imagen;
                    usuarioDL.IdDireccion = usuario.Direccion.IdDireccion;
                    context.Usuarios.Add(usuarioDL);
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
            catch(Exception ex) 
            {
                //Error.Add(ex);
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result UpdateEFLQ(ML.Usuario usuario)
        {
            Result result = new Result();

            try
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.IdUsuario == usuario.IdUsuario
                                 select usuarioDB).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.CURP = usuario.CURP;
                        query.Rol.IdRol = usuario.Rol.IdRol;
                        query.UserName = usuario.UserName;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.Email = usuario.Email;
                        query.Password = usuario.Password;
                        query.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
                        query.Sexo = usuario.Sexo;
                        query.Telefono = usuario.Telefono;
                        query.Celular = usuario.Celular;
                        query.Estatus = usuario.Estatus;
                        query.Imagen = usuario.Imagen;
                        query.Direccion.IdDireccion = usuario.Direccion.IdDireccion;
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
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro al Usuario";
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

        public static ML.Result DeleteEFLQ(int IdUsuario)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.IdUsuario == IdUsuario
                                 select usuarioDB).SingleOrDefault();
                    result.Object = query.IdDireccion;
                    context.Usuarios.Remove(query);
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
        public static ML.Result GetAllEFLQ()
        {
            Result result = new Result();

            try
            {
                using(DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuario in context.Usuarios
                                 join direccion in context.Direccions on usuario.IdDireccion equals direccion.IdDireccion
                                 join rol in context.Rols on usuario.IdRol equals rol.IdRol
                                 join colonia in context.Colonias on direccion.IdColonia equals colonia.IdColonia
                                 join municipio in context.Municipios on colonia.IdMunicipio equals municipio.IdMunicipio
                                 join estado in context.Estadoes on municipio.IdEstado equals estado.IdEstado
                                 select new {IdUsuario = usuario.IdUsuario, 
                                     Nombre = usuario.Nombre, 
                                     CURP = usuario.CURP, 
                                     IdRol = usuario.Rol.IdRol, 
                                     UserName = usuario.UserName, 
                                     ApellidoPaterno = usuario.ApellidoPaterno, 
                                     ApellidoMaterno = usuario.ApellidoMaterno, 
                                     Email = usuario.Email, 
                                     Password = usuario.Password, 
                                     FechaNacimiento = usuario.FechaNacimiento, 
                                     Sexo = usuario.Sexo, 
                                     Telefono = usuario.Telefono, 
                                     Celular = usuario.Celular, 
                                     Estatus = usuario.Estatus, 
                                     Imagen = usuario.Imagen, 
                                     NombreRol = rol.Nombre, 
                                     Calle = direccion.Calle, 
                                     NumeroExterior = direccion.NumeroExterior, 
                                     NumeroInterior = direccion.NumeroInterior, 
                                     NombreColonia = colonia.Nombre, 
                                     CodigoPostal = colonia.CodigoPostal, 
                                     NombreMunicipio = municipio.Nombre, 
                                     NombreEstado = estado.Nombre});
                    result.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuarioDL = new ML.Usuario();
                            usuarioDL.IdUsuario = obj.IdUsuario;
                            usuarioDL.Nombre = obj.Nombre;
                            usuarioDL.UserName = obj.UserName;
                            usuarioDL.ApellidoMaterno = obj.ApellidoMaterno;
                            usuarioDL.ApellidoPaterno = obj.ApellidoPaterno;
                            usuarioDL.Email = obj.Email;
                            usuarioDL.Password = obj.Password;
                            usuarioDL.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            usuarioDL.Sexo = obj.Sexo;
                            usuarioDL.Telefono = obj.Telefono;
                            usuarioDL.Celular = obj.Celular;
                            usuarioDL.Estatus = obj.Estatus;
                            usuarioDL.CURP = obj.CURP;
                            usuarioDL.Imagen = obj.Imagen;
                            usuarioDL.Rol = new ML.Rol();
                            usuarioDL.Rol.IdRol= obj.IdRol;
                            usuarioDL.Rol.Nombre = obj.NombreRol;
                            usuarioDL.Direccion = new ML.Direccion();
                            usuarioDL.Direccion.Calle = obj.Calle;
                            usuarioDL.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuarioDL.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuarioDL.Direccion.Colonia = new ML.Colonia();
                            usuarioDL.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuarioDL.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            usuarioDL.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioDL.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                            usuarioDL.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioDL.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;
                            result.Objects.Add(usuarioDL);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron usuarios";
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

        public static ML.Result GetByIdEFLQ(int IdUsuario)
        {
            Result result = new Result();

            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuario in context.Usuarios
                                 join direccion in context.Direccions on usuario.IdDireccion equals direccion.IdDireccion
                                 join rol in context.Rols on usuario.IdRol equals rol.IdRol
                                 join colonia in context.Colonias on direccion.IdColonia equals colonia.IdColonia
                                 join municipio in context.Municipios on colonia.IdMunicipio equals municipio.IdMunicipio
                                 join estado in context.Estadoes on municipio.IdEstado equals estado.IdEstado
                                 where usuario.IdUsuario == IdUsuario
                                 select new { idUsuario = usuario.IdUsuario, 
                                     Nombre = usuario.Nombre, 
                                     CURP = usuario.CURP, 
                                     IdRol = usuario.Rol.IdRol, 
                                     UserName = usuario.UserName, 
                                     ApellidoPaterno = usuario.ApellidoPaterno, 
                                     ApellidoMaterno = usuario.ApellidoMaterno, 
                                     Email = usuario.Email, 
                                     Password = usuario.Password, 
                                     FechaNacimiento = usuario.FechaNacimiento, 
                                     Sexo = usuario.Sexo, 
                                     Telefono = usuario.Telefono, 
                                     Celular = usuario.Celular, 
                                     Estatus = usuario.Estatus, 
                                     Imagen = usuario.Imagen, 
                                     IdDireccion = direccion.IdDireccion, 
                                     Calle = direccion.Calle, 
                                     NumeroExterior = direccion.NumeroExterior, 
                                     NumeroInterior = direccion.NumeroInterior, 
                                     IdColonia = colonia.IdColonia,
                                     NombreColonia = colonia.Nombre, 
                                     CodigoPostal = colonia.CodigoPostal, 
                                     IdMunicipio = municipio.IdMunicipio, 
                                     NombreMunicipio = municipio.Nombre, 
                                     IdEstado = estado.IdEstado, 
                                     NombreEstado = estado.Nombre }).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuarioDL = new ML.Usuario();
                        usuarioDL.IdUsuario = query.idUsuario;
                        usuarioDL.Nombre = query.Nombre;
                        usuarioDL.ApellidoMaterno = query.ApellidoMaterno;
                        usuarioDL.ApellidoPaterno = query.ApellidoPaterno;
                        usuarioDL.UserName = query.UserName;
                        usuarioDL.Email = query.Email;
                        usuarioDL.Password = query.Password;
                        usuarioDL.FechaNacimiento = query.FechaNacimiento.ToString("yyyy-MM-dd");
                        usuarioDL.Sexo = query.Sexo.Trim();
                        usuarioDL.Telefono = query.Telefono;
                        usuarioDL.Celular = query.Celular;
                        usuarioDL.Estatus = query.Estatus;
                        usuarioDL.CURP = query.CURP;
                        usuarioDL.Imagen = query.Imagen;
                        usuarioDL.Rol = new ML.Rol();
                        usuarioDL.Rol.IdRol = query.IdRol;
                        usuarioDL.Direccion = new ML.Direccion();
                        usuarioDL.Direccion.IdDireccion = query.IdDireccion;
                        usuarioDL.Direccion.Calle = query.Calle;
                        usuarioDL.Direccion.NumeroExterior = query.NumeroExterior;
                        usuarioDL.Direccion.NumeroInterior = query.NumeroInterior;
                        usuarioDL.Direccion.Colonia = new ML.Colonia();
                        usuarioDL.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuarioDL.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuarioDL.Direccion.Colonia.CodigoPostal = query.CodigoPostal;
                        usuarioDL.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuarioDL.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuarioDL.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;
                        usuarioDL.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuarioDL.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuarioDL.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;
                        result.Object = usuarioDL;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron usuarios";
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

        public static ML.Result GetByIdEmailEFLQ(string Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.Email == Email
                                 select usuarioDB);
                    if (query != null && query.Count() > 0)
                    {
                        result.ErrorMessage = "Email ya registrado";
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el Email";
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
        public static ML.Result GetByIdUserNameEFLQ(string UserName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.UserName == UserName
                                 select usuarioDB);
                    if (query != null && query.Count() > 0)
                    {
                        result.ErrorMessage = "UserName ya registrado";
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el UserName";
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
        public static ML.Result GetByIdCURPEFLQ(string CURP)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.CURP == CURP
                                 select usuarioDB);
                    if (query != null && query.Count() > 0)
                    {
                        result.Correct = true;
                        result.ErrorMessage = "CURP ya registrado";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el CURP";
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
        public static ML.Result GetByIdEmailAndUsuarioEFLQ(int IdUsuario,string Email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.Email == Email
                                 select new { idUsuario = usuarioDB.IdUsuario }).SingleOrDefault();
                    if (query != null)
                    {
                        if (IdUsuario == query.idUsuario)
                        {
                            result.Correct = false;
                            //No hay cambios en el Correo
                        }
                        else
                        {
                            result.Correct = true;
                            result.ErrorMessage = "Otro usuario ya registro ese correo";
                        }
                        }
                    else
                    {
                        result.Correct = false;
                        //Nuevo Email
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

        public static ML.Result GetByIdUserNameAndUsuarioEFLQ(int IdUsuario, string UserName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.UserName == UserName
                                 select new { idUsuario = usuarioDB.IdUsuario }).SingleOrDefault();
                    if (query != null)
                    {
                        if (IdUsuario == query.idUsuario)
                        {
                            result.Correct = false;
                            //No hay cambios en el UserName
                        }
                        else
                        {
                            result.Correct = true;
                            result.ErrorMessage = "Otro usuario ya registro ese UserName";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        //Nuevo UserName
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

        public static ML.Result GetByIdCurpAndUsuarioEFLQ(int IdUsuario, string Curp)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuarios
                                 where usuarioDB.CURP == Curp
                                 select new { idUsuario = usuarioDB.IdUsuario }).SingleOrDefault();
                    if (query != null)
                    {
                        if (IdUsuario == query.idUsuario)
                        {
                            result.Correct = false;
                            //No hay cambios en el CURP
                        }
                        else
                        {
                            result.Correct = true;
                            result.ErrorMessage = "Otro usuario ya registro ese CURP";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        //Nuevo CURP
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

        public static ML.Result UpdateIdEstatusEFSP(int IdUsuario, bool Estatus)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var rowsAffected = context.UpdateUsuarioIdEstatus(IdUsuario, Estatus);
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

        public static ML.Result GetAllEFLQBA(string Nombre, string ApellidoPaterno, string ApellidoMaterno, int IdRol)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LBlancasProgramacionNCapasEntities context = new DL_EF.LBlancasProgramacionNCapasEntities())
                {
                    var query = (from usuario in context.Usuarios
                                 join direccion in context.Direccions on usuario.IdDireccion equals direccion.IdDireccion
                                 join rol in context.Rols on usuario.IdRol equals rol.IdRol
                                 join colonia in context.Colonias on direccion.IdColonia equals colonia.IdColonia
                                 join municipio in context.Municipios on colonia.IdMunicipio equals municipio.IdMunicipio
                                 join estado in context.Estadoes on municipio.IdEstado equals estado.IdEstado
                                 select new
                                 {
                                     IdUsuario = usuario.IdUsuario,
                                     Nombre = usuario.Nombre,
                                     CURP = usuario.CURP,
                                     IdRol = usuario.Rol.IdRol,
                                     UserName = usuario.UserName,
                                     ApellidoPaterno = usuario.ApellidoPaterno,
                                     ApellidoMaterno = usuario.ApellidoMaterno,
                                     Email = usuario.Email,
                                     Password = usuario.Password,
                                     FechaNacimiento = usuario.FechaNacimiento,
                                     Sexo = usuario.Sexo,
                                     Telefono = usuario.Telefono,
                                     Celular = usuario.Celular,
                                     Estatus = usuario.Estatus,
                                     Imagen = usuario.Imagen,
                                     NombreRol = rol.Nombre,
                                     Calle = direccion.Calle,
                                     NumeroExterior = direccion.NumeroExterior,
                                     NumeroInterior = direccion.NumeroInterior,
                                     NombreColonia = colonia.Nombre,
                                     CodigoPostal = colonia.CodigoPostal,
                                     NombreMunicipio = municipio.Nombre,
                                     NombreEstado = estado.Nombre
                                 });
                    var queryBA = (from usuario in context.Usuarios
                                   join direccion in context.Direccions on usuario.IdDireccion equals direccion.IdDireccion
                                   join rol in context.Rols on usuario.IdRol equals rol.IdRol
                                   join colonia in context.Colonias on direccion.IdColonia equals colonia.IdColonia
                                   join municipio in context.Municipios on colonia.IdMunicipio equals municipio.IdMunicipio
                                   join estado in context.Estadoes on municipio.IdEstado equals estado.IdEstado
                                   where usuario.Nombre.Contains(Nombre) && usuario.ApellidoPaterno.Contains(ApellidoPaterno) &&
                                   usuario.ApellidoMaterno.Contains(ApellidoMaterno)
                                   select new
                                   {
                                       IdUsuario = usuario.IdUsuario,
                                       Nombre = usuario.Nombre,
                                       CURP = usuario.CURP,
                                       IdRol = usuario.Rol.IdRol,
                                       UserName = usuario.UserName,
                                       ApellidoMaterno = usuario.ApellidoMaterno,
                                       ApellidoPaterno = usuario.ApellidoPaterno,
                                       Email = usuario.Email,
                                       Password = usuario.Password,
                                       FechaNacimiento = usuario.FechaNacimiento,
                                       Sexo = usuario.Sexo,
                                       Telefono = usuario.Telefono,
                                       Celular = usuario.Celular,
                                       Estatus = usuario.Estatus,
                                       Imagen = usuario.Imagen,
                                       NombreRol = rol.Nombre,
                                       Calle = direccion.Calle,
                                       NumeroExterior = direccion.NumeroExterior,
                                       NumeroInterior = direccion.NumeroInterior,
                                       NombreColonia = colonia.Nombre,
                                       CodigoPostal = colonia.CodigoPostal,
                                       NombreMunicipio = municipio.Nombre,
                                       NombreEstado = estado.Nombre
                                   });

                    var queryBARol = (from usuario in context.Usuarios
                                      join direccion in context.Direccions on usuario.IdDireccion equals direccion.IdDireccion
                                      join rol in context.Rols on usuario.IdRol equals rol.IdRol
                                      join colonia in context.Colonias on direccion.IdColonia equals colonia.IdColonia
                                      join municipio in context.Municipios on colonia.IdMunicipio equals municipio.IdMunicipio
                                      join estado in context.Estadoes on municipio.IdEstado equals estado.IdEstado
                                      where usuario.Nombre.Contains(Nombre) && usuario.ApellidoPaterno.Contains(ApellidoPaterno) &&
                                      usuario.ApellidoMaterno.Contains(ApellidoMaterno) && rol.IdRol == IdRol
                                      select new
                                      {
                                          IdUsuario = usuario.IdUsuario,
                                          Nombre = usuario.Nombre,
                                          CURP = usuario.CURP,
                                          IdRol = usuario.Rol.IdRol,
                                          UserName = usuario.UserName,
                                          ApellidoMaterno = usuario.ApellidoMaterno,
                                          ApellidoPaterno = usuario.ApellidoPaterno,
                                          Email = usuario.Email,
                                          Password = usuario.Password,
                                          FechaNacimiento = usuario.FechaNacimiento,
                                          Sexo = usuario.Sexo,
                                          Telefono = usuario.Telefono,
                                          Celular = usuario.Celular,
                                          Estatus = usuario.Estatus,
                                          Imagen = usuario.Imagen,
                                          NombreRol = rol.Nombre,
                                          Calle = direccion.Calle,
                                          NumeroExterior = direccion.NumeroExterior,
                                          NumeroInterior = direccion.NumeroInterior,
                                          NombreColonia = colonia.Nombre,
                                          CodigoPostal = colonia.CodigoPostal,
                                          NombreMunicipio = municipio.Nombre,
                                          NombreEstado = estado.Nombre
                                      });
                    result.Objects = new List<object>();
                    if (Nombre == "" && ApellidoMaterno == "" && ApellidoPaterno == "" && IdRol == 0)
                    {
                        if (query != null && query.ToList().Count() > 0)
                        {
                            foreach (var obj in query)
                            {
                                ML.Usuario usuarioDL = new ML.Usuario();
                                usuarioDL.IdUsuario = obj.IdUsuario;
                                usuarioDL.Nombre = obj.Nombre;
                                usuarioDL.UserName = obj.UserName;
                                usuarioDL.ApellidoMaterno = obj.ApellidoMaterno;
                                usuarioDL.ApellidoPaterno = obj.ApellidoPaterno;
                                usuarioDL.Email = obj.Email;
                                usuarioDL.Password = obj.Password;
                                usuarioDL.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                                usuarioDL.Sexo = obj.Sexo;
                                usuarioDL.Telefono = obj.Telefono;
                                usuarioDL.Celular = obj.Celular;
                                usuarioDL.Estatus = obj.Estatus;
                                usuarioDL.CURP = obj.CURP;
                                usuarioDL.Imagen = obj.Imagen;
                                usuarioDL.Rol = new ML.Rol();
                                usuarioDL.Rol.IdRol = obj.IdRol;
                                usuarioDL.Rol.Nombre = obj.NombreRol;
                                usuarioDL.Direccion = new ML.Direccion();
                                usuarioDL.Direccion.Calle = obj.Calle;
                                usuarioDL.Direccion.NumeroExterior = obj.NumeroExterior;
                                usuarioDL.Direccion.NumeroInterior = obj.NumeroInterior;
                                usuarioDL.Direccion.Colonia = new ML.Colonia();
                                usuarioDL.Direccion.Colonia.Nombre = obj.NombreColonia;
                                usuarioDL.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                                usuarioDL.Direccion.Colonia.Municipio = new ML.Municipio();
                                usuarioDL.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                                usuarioDL.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                                usuarioDL.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;
                                result.Objects.Add(usuarioDL);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron Usuarios";
                        }
                    }
                    else
                    {
                        if (IdRol == 0)
                        {
                            if (queryBA != null && queryBA.ToList().Count() > 0)
                            {
                                foreach (var obj in queryBA)
                                {
                                    ML.Usuario usuarioDL = new ML.Usuario();
                                    usuarioDL.IdUsuario = obj.IdUsuario;
                                    usuarioDL.Nombre = obj.Nombre;
                                    usuarioDL.UserName = obj.UserName;
                                    usuarioDL.ApellidoMaterno = obj.ApellidoMaterno;
                                    usuarioDL.ApellidoPaterno = obj.ApellidoPaterno;
                                    usuarioDL.Email = obj.Email;
                                    usuarioDL.Password = obj.Password;
                                    usuarioDL.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                                    usuarioDL.Sexo = obj.Sexo;
                                    usuarioDL.Telefono = obj.Telefono;
                                    usuarioDL.Celular = obj.Celular;
                                    usuarioDL.Estatus = obj.Estatus;
                                    usuarioDL.CURP = obj.CURP;
                                    usuarioDL.Imagen = obj.Imagen;
                                    usuarioDL.Rol = new ML.Rol();
                                    usuarioDL.Rol.IdRol = obj.IdRol;
                                    usuarioDL.Rol.Nombre = obj.NombreRol;
                                    usuarioDL.Direccion = new ML.Direccion();
                                    usuarioDL.Direccion.Calle = obj.Calle;
                                    usuarioDL.Direccion.NumeroExterior = obj.NumeroExterior;
                                    usuarioDL.Direccion.NumeroInterior = obj.NumeroInterior;
                                    usuarioDL.Direccion.Colonia = new ML.Colonia();
                                    usuarioDL.Direccion.Colonia.Nombre = obj.NombreColonia;
                                    usuarioDL.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                                    usuarioDL.Direccion.Colonia.Municipio = new ML.Municipio();
                                    usuarioDL.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                                    usuarioDL.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                                    usuarioDL.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;
                                    result.Objects.Add(usuarioDL);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron Usuarios";
                            }
                        }
                        else
                        {
                            if (queryBARol != null && queryBARol.ToList().Count() > 0)
                            {
                                foreach (var obj in queryBARol)
                                {
                                    ML.Usuario usuarioDL = new ML.Usuario();
                                    usuarioDL.IdUsuario = obj.IdUsuario;
                                    usuarioDL.Nombre = obj.Nombre;
                                    usuarioDL.UserName = obj.UserName;
                                    usuarioDL.ApellidoMaterno = obj.ApellidoMaterno;
                                    usuarioDL.ApellidoPaterno = obj.ApellidoPaterno;
                                    usuarioDL.Email = obj.Email;
                                    usuarioDL.Password = obj.Password;
                                    usuarioDL.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                                    usuarioDL.Sexo = obj.Sexo;
                                    usuarioDL.Telefono = obj.Telefono;
                                    usuarioDL.Celular = obj.Celular;
                                    usuarioDL.Estatus = obj.Estatus;
                                    usuarioDL.CURP = obj.CURP;
                                    usuarioDL.Imagen = obj.Imagen;
                                    usuarioDL.Rol = new ML.Rol();
                                    usuarioDL.Rol.IdRol = obj.IdRol;
                                    usuarioDL.Rol.Nombre = obj.NombreRol;
                                    usuarioDL.Direccion = new ML.Direccion();
                                    usuarioDL.Direccion.Calle = obj.Calle;
                                    usuarioDL.Direccion.NumeroExterior = obj.NumeroExterior;
                                    usuarioDL.Direccion.NumeroInterior = obj.NumeroInterior;
                                    usuarioDL.Direccion.Colonia = new ML.Colonia();
                                    usuarioDL.Direccion.Colonia.Nombre = obj.NombreColonia;
                                    usuarioDL.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                                    usuarioDL.Direccion.Colonia.Municipio = new ML.Municipio();
                                    usuarioDL.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                                    usuarioDL.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                                    usuarioDL.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;
                                    result.Objects.Add(usuarioDL);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron Usuarios";
                            }
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
    }
}

