using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult CargaMasiva()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Errores = new List<object>();
            usuario.Correctos = new List<object>();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult CargaMasiva(HttpPostedFileBase inputCargaMasiva, string accion)
        {
            ML.Result result = new ML.Result();
            Session["RutaErrores"] = null;
            string fecha = DateTime.Now.ToString("dd-MM-yyyy-hhmmsstt");
            string ErroresTXT = $"Errores{fecha}.txt";
            string OkTXT = $"Ok{fecha}.txt";
            string RutaErroresExcelWebConfig = ConfigurationManager.AppSettings["CargaMasivaExcelErrores"];
            string RutaOkExcelWebConfig = ConfigurationManager.AppSettings["CargaMasivaExcelCorrectos"];
            string RutaSaveExcelWebConfig = ConfigurationManager.AppSettings["CargaMasivaExcelSave"];
            string RutaErroresTxtWebConfig = ConfigurationManager.AppSettings["CargaMasivaTxtErrores"];
            string RutaOkTxtWebConfig = ConfigurationManager.AppSettings["CargaMasivaTxtCorrectos"];

            string rutaErrores = Server.MapPath(@RutaErroresTxtWebConfig + ErroresTXT);
            string rutaOk = Server.MapPath(RutaOkTxtWebConfig + OkTXT);
            string NoGuardados = "";

            ML.Usuario usuarioView = new ML.Usuario();
            usuarioView.Errores = new List<object>();
            usuarioView.Correctos = new List<object>();
            string row = "";
            try
            {
                if (accion == "validar")
                {
                    Session["RutaTXT"] = null;
                    string extension = inputCargaMasiva.FileName.Split('.')[1];
                    string NoInsertados = "";
                    if (extension == "txt")
                    {
                        try
                        {
                            using (StreamReader file = new StreamReader(inputCargaMasiva.InputStream))
                            {
                                string salto = "";
                                string line = file.ReadLine();
                                while ((line = file.ReadLine()) != null)
                                {
                                    string[] datos = line.Split('|');
                                    ML.Usuario usuario = new ML.Usuario();
                                    usuario.Nombre = datos[0];
                                    usuario.ApellidoPaterno = datos[1];
                                    usuario.ApellidoMaterno = datos[2];
                                    usuario.Email = datos[3];
                                    usuario.UserName = datos[4];
                                    usuario.FechaNacimiento = datos[5];
                                    usuario.Sexo = datos[6];
                                    usuario.CURP = datos[7];
                                    usuario.Estatus = Convert.ToBoolean(datos[8]);
                                    usuario.Telefono = datos[9];
                                    usuario.Celular = datos[10];
                                    usuario.Password = datos[11];
                                    usuario.Rol = new ML.Rol();
                                    usuario.Rol.IdRol = Convert.ToByte(datos[12]);
                                    usuario.Direccion = new ML.Direccion();
                                    string Errores = ValidarDatos(usuario);
                                    if (Errores != "")
                                    {
                                        string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
                                        NoInsertados = NoInsertados + $"No se pudo insertar el usuario {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno} debido a que: " + Errores;
                                        string NoAgregados = $"{NombreUsuario}| No se pudo insertar el usuario debido a: {Errores}";
                                        usuarioView.Errores.Add(NoAgregados);

                                    }
                                    else //Nombre|ApellidoPaterno|ApellidoMaterno|Email|UserName|FechaNacimiento|Sexo|CURP|Estatus|Telefono|Celular|Password|IdRol
                                    {
                                        row = row + salto + $"{usuario.Nombre}|{usuario.ApellidoPaterno}|{usuario.ApellidoMaterno}|{usuario.Email}|{usuario.UserName}|{usuario.FechaNacimiento}|{usuario.Sexo.Trim()}|{usuario.CURP}|{usuario.Estatus}|{usuario.Telefono}|{usuario.Celular}|{usuario.Password}|{usuario.Rol.IdRol}";
                                        string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";

                                        string Validados = $"{NombreUsuario}|Usuario Valido";
                                        usuarioView.Correctos.Add(Validados);
                                        salto = "\n";
                                    }
                                }
                            }
                        }                       
                        catch (Exception e)
                        {
                            TempData["Errorr"] = "Error al Leer el archivo TXT" + e;
                        }
                        if (NoInsertados == "")
                        {
                            try
                            {
                                using (StreamWriter Guardar = new StreamWriter(rutaOk))
                                {
                                    Guardar.WriteLine("Nombre|ApellidoPaterno|ApellidoMaterno|Email|UserName|FechaNacimiento|Sexo|CURP|Estatus|Telefono|Celular|Password|IdRol");
                                    Guardar.WriteLine(row);
                                }
                                //inputCargaMasiva.SaveAs(rutaOk);
                                Session["RutaTXT"] = rutaOk;
                                TempData["Success"] = "Datos Validados.Presiona Cargar para Subirlos a la Base";
                                //Txt Ok
                            }
                            catch (Exception e)
                            {
                                TempData["Errorr"] = "Error al Escribir el Archivo de Guardados" + e;
                            }
                        }
                        else
                        {
                            try
                            {
                                using (StreamWriter noInsertados = new StreamWriter(rutaErrores))
                                {
                                    noInsertados.WriteLine("No se agregaron los siguientes usuarios:");
                                    noInsertados.WriteLine(NoInsertados);
                                }
                                Session["RutaErrores"] = rutaErrores;
                                TempData["Error"] = "Se encontraron errores, favor de corregirlos para insertarlos a la Base";
                            }
                            catch (Exception e)
                            {
                                TempData["Errorr"] = "Error al Escribir el Archivo de No Insertados" + e;
                            }
                        }
                    }
                    else
                    {
                        string NombreArchivo = Path.GetFileNameWithoutExtension(inputCargaMasiva.FileName);
                        string rutaErroresExcel = Server.MapPath(@RutaErroresExcelWebConfig + NombreArchivo + $"{fecha}.txt");
                        string rutaOkExcel = Server.MapPath(RutaOkExcelWebConfig + NombreArchivo + $"{fecha}.txt");
                        string rutaSave = Server.MapPath(@RutaSaveExcelWebConfig + NombreArchivo+fecha+$".{extension}");
                        inputCargaMasiva.SaveAs(rutaSave);
                        string connection = ConfigurationManager.ConnectionStrings["OleDbConnection"].ConnectionString + rutaSave;
                        result = LeerExcel(connection);
                        if (result.Correct)
                        {
                            usuarioView.Usuarios = result.Objects;
                            foreach (ML.Usuario usuario in usuarioView.Usuarios)
                            {
                                string Errores = ValidarDatos(usuario);
                                if (Errores != "")
                                {
                                    string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
                                    NoInsertados = NoInsertados + $"No se pudo insertar el usuario {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno} debido a que: " + Errores;
                                    string NoAgregados = $"{NombreUsuario}| No se pudo insertar el usuario debido a: {Errores}";
                                    usuarioView.Errores.Add(NoAgregados);
                                }
                                else
                                {
                                    row = row + $"{usuario.Nombre}|{usuario.ApellidoPaterno}|{usuario.ApellidoMaterno}|{usuario.Email}|{usuario.UserName}|{usuario.FechaNacimiento}|{usuario.Sexo.Trim()}|{usuario.CURP}|{usuario.Estatus}|{usuario.Telefono}|{usuario.Celular}|{usuario.Password}|{usuario.Rol.IdRol}\n";
                                    string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";

                                    string Validados = $"{NombreUsuario}|Usuario Valido";
                                    usuarioView.Correctos.Add(Validados);
                                }
                            }

                            if (NoInsertados == "")
                            {
                                try
                                {
                                    using (StreamWriter Guardar = new StreamWriter(rutaOkExcel))
                                    {
                                        Guardar.WriteLine("Nombre|ApellidoPaterno|ApellidoMaterno|Email|UserName|FechaNacimiento|Sexo|CURP|Estatus|Telefono|Celular|Password|IdRol");
                                        Guardar.WriteLine(row);
                                    }
                                    Session["RutaEXCEL"] = rutaSave;
                                    TempData["Success"] = "Datos Validados.Presiona Cargar para Subirlos a la Base";
                                }
                                catch (Exception e)
                                {
                                    TempData["Errorr"] = "Error al Escribir el Archivo de Guardados" + e;
                                }
                            }
                            else
                            {
                                try
                                {
                                    using (StreamWriter noInsertados = new StreamWriter(rutaErroresExcel))
                                    {
                                        noInsertados.WriteLine("No se agregaron los siguientes usuarios:");
                                        noInsertados.WriteLine(NoInsertados);
                                    }
                                    Session["RutaErrores"] = rutaErroresExcel;
                                    TempData["Error"] = "Se encontraron errores, favor de corregirlos para insertarlos a la Base";
                                }
                                catch (Exception e)
                                {
                                    TempData["Errorr"] = "Error al Escribir el Archivo de No Insertados" + e;
                                }
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Hubo un Error al Guardar el Excel";
                        }
                        

                    }
                }
                if (Session["RutaTXT"] != null && accion == "cargar")
                {
                    string ArchivoGuardado = Session["RutaTXT"].ToString();
                    try
                    {
                        using (StreamReader file = new StreamReader(ArchivoGuardado))
                        {
                            string line = file.ReadLine();
                            line = file.ReadLine();
                            while (line != null)
                            {
                                string[] datos = line.Split('|');
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = datos[0];
                                usuario.ApellidoPaterno = datos[1];
                                usuario.ApellidoMaterno = datos[2];
                                usuario.Email = datos[3];
                                usuario.UserName = datos[4];
                                usuario.FechaNacimiento = datos[5];
                                usuario.Sexo = datos[6];
                                usuario.CURP = datos[7];
                                usuario.Estatus = Convert.ToBoolean(datos[8]);
                                usuario.Telefono = datos[9];
                                usuario.Celular = datos[10];
                                usuario.Password = datos[11];
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = Convert.ToByte(datos[12]);
                                usuario.Direccion = new ML.Direccion();
                                result = BL.Usuario.AddEFSP(usuario);
                                if (!result.Correct)
                                {
                                    string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
                                    string NoAgregados = $"{NombreUsuario} | No se pudo insertar el usuario debido a: {result.ErrorMessage}";
                                    NoGuardados = NoGuardados + NoAgregados + "\n";
                                    usuarioView.Errores.Add(NoAgregados);
                                }
                                else
                                {
                                    string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
                                    string Agregados = $"{NombreUsuario} |Usuario Agregado Correctamente";
                                    usuarioView.Correctos.Add(Agregados);
                                }
                                line = file.ReadLine();
                            }
                        }
                    } catch (Exception e)
                    {
                        TempData["Error"] = "Error al Leer el Archivo TXT" + e;
                    }
                    if (NoGuardados != "")
                    {
                        try {
                            string rutaNoGuardados = Server.MapPath(RutaErroresTxtWebConfig + $"NoGuardados{fecha}.txt");
                            using (StreamWriter noGuardados = new StreamWriter(rutaNoGuardados))
                            {
                                noGuardados.WriteLine("No se agregaron los siguientes usuarios:");
                                noGuardados.WriteLine(NoGuardados);
                            }
                            Session["RutaErrores"] = rutaNoGuardados;
                            TempData["Error"] = "No se pudieron insertar todos los datos:";
                        } catch(Exception e)
                        {
                            TempData["Error"] = "Error al Escribir el Archivo de No agregados a la Base"+e;
                        }
                    }
                    else
                    {
                        TempData["Success"] = "Datos agregados a la Base";
                    }

                    Session["RutaTXT"] = null;
                }
                if (Session["RutaEXCEL"] != null && accion == "cargar")
                {
                    string ArchivoGuardado = Session["RutaEXCEL"].ToString();
                    string connection = ConfigurationManager.ConnectionStrings["OleDbConnection"].ConnectionString + ArchivoGuardado;
                    result = LeerExcel(connection);
                    if (!result.Correct)
                    {
                        TempData["Error"] = "Hubo un Error al Leer el Excel" + result.ErrorMessage;
                    }
                    else
                    {
                        usuarioView.Usuarios = result.Objects;
                        foreach(ML.Usuario usuario in usuarioView.Usuarios)
                        {
                            ML.Result resultAdd = BL.Usuario.AddEFSP(usuario);
                            if (!resultAdd.Correct)
                            {
                                string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
                                string NoAgregados = $"{NombreUsuario} | No se pudo insertar el usuario debido a: {resultAdd.ErrorMessage}";
                                NoGuardados = NoGuardados + NoAgregados + "\n";
                                usuarioView.Errores.Add(NoAgregados);
                            }
                            else
                            {
                                string NombreUsuario = $"{usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
                                string Agregados = $"{NombreUsuario} |Usuario Agregado Correctamente";
                                usuarioView.Correctos.Add(Agregados);
                            }
                        }
                        if (NoGuardados != "")
                        {
                            try {
                                string rutaNoGuardados = Server.MapPath(RutaErroresExcelWebConfig + $"NoGuardados{fecha}.txt");
                                using (StreamWriter noGuardados = new StreamWriter(rutaNoGuardados))
                                {
                                    noGuardados.WriteLine("No se agregaron los siguientes usuarios:");
                                    noGuardados.WriteLine(NoGuardados);
                                }
                                Session["RutaErrores"] = rutaNoGuardados;
                                TempData["Error"] = "No se pudieron insertar todos los datos:";
                            }
                            catch
                            {
                                TempData["Error"] = "Error al Escribir el Archivo de No Agregados a la Base";
                            }
                        }
                        else
                        {
                            TempData["Success"] = "Datos agregados a la Base";
                        }
                        Session["RutaEXCEL"] = null;
                    }
                }
                }
            catch (Exception e)
            {  
                    TempData["Error"] = "Error al Validar o Cargar los Datos" + e;
            }
            return View(usuarioView);
        }
        [NonAction]
        public static string ValidarDatos(ML.Usuario usuario)
        {
            string Errores = "";

            if (!Regex.IsMatch(usuario.Nombre, @"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$"))
            {
                Errores = "Para el nombre solo se permiten Letras, además cada uno debe iniciar con Mayuscula.\n";
            }
            if (!Regex.IsMatch(usuario.ApellidoPaterno, @"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$"))
            {
                Errores = Errores + "Para el apellido paterno solo se permiten Letras, además cada uno debe iniciar con Mayuscula.\n";
            }
            if (!Regex.IsMatch(usuario.ApellidoMaterno, @"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$"))
            {
                Errores = Errores + "Para el apellido materno solo se permiten Letras, además cada uno debe iniciar con Mayuscula.\n";
            }
            if (!Regex.IsMatch(usuario.Password, @"^(?!.*(\d)\1)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$"))
            {
                Errores = Errores + "La Contraseña debe tener una Minuscula, una Mayuscula, un Numero, un Caracter Especial (@$!%*?&), no debe contener numeros repetidos.\n";
            }
            if (!Regex.IsMatch(usuario.UserName, @"^(?=.*\d)[A-ZÁÉÍÓÚÑ][A-Za-záéíóúñÁÉÍÓÚÑ\d-]*$"))
            {
                Errores = Errores + "El Nombre de Usuario debe iniciar con Mayuscula y tener al menos un numero.\n";
            }
            if (!Regex.IsMatch(usuario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                Errores = Errores + "El Email no es valido";
            }
            if (!Regex.IsMatch(usuario.FechaNacimiento, @"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$"))
            {
                Errores = Errores + "La Fecha de Nacimiento es invalida, el formato es dd-mm-aaaa.\n";
            }
            if (!Regex.IsMatch(usuario.Sexo, @"^[MF]{1}$"))
            {
                Errores = Errores + "El Sexo es invalido solo se permite M: masculino o F: femenino.\n";
            }
            if (!Regex.IsMatch(usuario.Telefono, @"^\d{10}$"))
            {
                Errores = Errores + "El Telefono solo debe tener numeros y exactamente 10 digitos.\n";
            }
            if (!Regex.IsMatch(usuario.Celular, @"^\d{10}$"))
            {
                Errores = Errores + "El Celular solo debe tener numeros y exactamente 10 digitos.\n";
            }
            if (!Regex.IsMatch(usuario.CURP, @"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$"))
            {
                Errores = Errores + "El Curp no es valido.\n";
            }
            return Errores;
        }
        [NonAction]
        public static ML.Result LeerExcel(string connectionString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(OleDbConnection context = new OleDbConnection(connectionString))
                {
                    DataTable dt = new DataTable();
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        using (OleDbDataAdapter da = new OleDbDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            da.Fill(dt);
                            result.Objects = new List<object>();
                            foreach(DataRow row in dt.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row["Nombre"].ToString();
                                usuario.ApellidoPaterno = row["Apellido Paterno"].ToString();
                                usuario.ApellidoMaterno = row["Apellido Materno"].ToString();
                                usuario.UserName = row["UserName"].ToString();
                                usuario.FechaNacimiento = row["Fecha Nacimiento"].ToString();
                                usuario.Email = row["Email"].ToString();
                                usuario.Sexo = row["Sexo"].ToString();
                                usuario.CURP = row["CURP"].ToString();
                                usuario.Estatus = Convert.ToBoolean(row["Estatus"]);
                                usuario.Telefono = row["Telefono"].ToString() ;
                                usuario.Celular = row["Celular"].ToString();
                                usuario.Password = row["Password"].ToString();
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = Convert.ToByte(row["Rol"]);
                                usuario.Direccion = new ML.Direccion();
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex) {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public FileResult DescargarTXT()
        {
            string rutaTXT = Session["RutaErrores"].ToString();
            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaTXT);
            string fileName = "Errores.ext";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }
    }
}
