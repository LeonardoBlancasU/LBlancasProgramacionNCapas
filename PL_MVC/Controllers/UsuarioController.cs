using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.XPath;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            //ML.Result result = BL.Usuario.GetAllEFLQ();
            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //}

            //UsuarioServiceReference.UsuarioClient soap = new UsuarioServiceReference.UsuarioClient();
            //var respuesta = soap.GetAll();
            //var respuesta = GetAllSOAP();
            var respuesta = GetAllREST();

            if (respuesta.Correct)
            {
                usuario.Usuarios = respuesta.Objects.ToList();
            }
            usuario.Direccion = new ML.Direccion();
            ML.Result resultRoles = BL.Rol.GetAllEFLQ();
            usuario.Rol.Roles = resultRoles.Correct ? resultRoles.Objects : new List<object>();
            //ML.Result resultRoles = BL.Rol.GetAllEFSP();
            usuario.Rol.Roles = resultRoles.Objects;
            return View(usuario);
        }
        
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            ML.Result result = BL.Usuario.GetAllEFSP(usuario);
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            ML.Result resultRoles = BL.Rol.GetAll();
            usuario.Rol.Roles = resultRoles.Objects;
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Formulario(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            if (IdUsuario != null && IdUsuario > 0)
            {

                //ML.Result result = BL.Usuario.GetByIdEFLQ(IdUsuario.Value);

                //if (result.Correct == true)
                //{
                //    usuario = (ML.Usuario)result.Object;
                //}
                //UsuarioServiceReference.UsuarioClient soap = new UsuarioServiceReference.UsuarioClient();
                //var respuesta = soap.GetById(IdUsuario.Value);
                var respuesta = GetByIdREST(IdUsuario.Value);
                if (respuesta.Correct)
                {
                    usuario = (ML.Usuario)respuesta.Object;
                }
            }
            ML.Result resultRoles = BL.Rol.GetAllEFLQ();
            usuario.Rol.Roles = resultRoles.Correct ? resultRoles.Objects : new List<object>();
            ML.Result resultEstados = BL.Estado.GetAllEFLQ();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Correct ? resultEstados.Objects : new List<object>();
            ML.Result resultMunicipios = BL.Municipio.GetByIdEstadoEFLQ(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Correct ? resultMunicipios.Objects : new List<object>();
            ML.Result resultColonias = BL.Colonia.GetByIdMunicipioEFLQ(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            usuario.Direccion.Colonia.Colonias = resultColonias.Correct ? resultColonias.Objects : new List<object>();

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario, HttpPostedFileBase ImagenFile)
        {
            if (ModelState.IsValid)
            {
                UsuarioServiceReference.UsuarioClient soap = new UsuarioServiceReference.UsuarioClient();
                ML.Result result = new ML.Result();
                if (ImagenFile != null && ImagenFile.ContentLength > 0)
                {
                    MemoryStream target = new MemoryStream();
                    ImagenFile.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    usuario.Imagen = data;
                }
                else
                {
                    // Si no se selecciona una imagen, puedes asignar una imagen predeterminada
                    string defaultImagePath = Server.MapPath("~/Img/Default.png");
                    byte[] defaultImageData = System.IO.File.ReadAllBytes(defaultImagePath);
                    usuario.Imagen = defaultImageData;
                }

                if (usuario.IdUsuario == 0) // Nuevo usuario
                {
                    ML.Result resultEmail = BL.Usuario.GetByIdEmailEFLQ(usuario.Email);
                    ML.Result resultUserName = BL.Usuario.GetByIdUserNameEFLQ(usuario.UserName);
                    ML.Result resultCURP = BL.Usuario.GetByIdCURPEFLQ(usuario.CURP);
                    if (resultEmail.Correct == false && resultUserName.Correct == false && resultCURP.Correct == false)
                    {
                        ML.Result resultDireccion = BL.Direccion.AddEFLQ(usuario);
                        if (resultDireccion.Correct)
                        {
                            usuario.Direccion.IdDireccion = (int)resultDireccion.Object;

                            //result = BL.Usuario.AddWidthCURP(usuario);
                            //if (result.Correct)
                            //{
                            //    TempData["Agregado"] = "Usuario agregado correctamente.";
                            //    return RedirectToAction("GetAll");
                            //}
                            //var request = soap.Add(usuario);
                            //var request = AddSOAP(usuario);
                            var request = AddREST(usuario);
                            if (request.Correct)
                            {
                                TempData["Agregado"] = "Usuario agregado correctamente.";
                                return RedirectToAction("GetAll");
                            }
                            else
                            {
                                TempData["Error"] = "Error al agregar el usuario: " + result.ErrorMessage;
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Error al agregar la dirección: " + resultDireccion.ErrorMessage;
                        }
                    }
                    else
                    {
                        ML.Result resultRoles = BL.Rol.GetAllEFLQ();
                        usuario.Rol.Roles = resultRoles.Correct ? resultRoles.Objects : new List<object>();
                        ML.Result resultEstados = BL.Estado.GetAllEFLQ();
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Correct ? resultEstados.Objects : new List<object>();
                        ML.Result resultMunicipios = BL.Municipio.GetByIdEstadoEFLQ(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Correct ? resultMunicipios.Objects : new List<object>();
                        ML.Result resultColonias = BL.Colonia.GetByIdMunicipioEFLQ(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        usuario.Direccion.Colonia.Colonias = resultColonias.Correct ? resultColonias.Objects : new List<object>();
                        // Pasar los errores específicos a la vista
                        ViewBag.EmailError = resultEmail.Correct == true ? "El correo ya está registrado" : null;
                        ViewBag.UserNameError = resultUserName.Correct == true ? "El usuario ya existe" : null;
                        ViewBag.CURPError = resultCURP.Correct == true ? "La CURP ya está registrada" : null;

                        // Mantener los datos ingresados para no perderlos
                        ViewBag.Usuario = usuario;
                        return View(usuario);
                    }
                }
                else // Si es una actualización de usuario
                {
                    ML.Result resultEmail = BL.Usuario.GetByIdEmailAndUsuarioEFLQ(usuario.IdUsuario, usuario.Email);
                    ML.Result resultUserName = BL.Usuario.GetByIdUserNameAndUsuarioEFLQ(usuario.IdUsuario, usuario.UserName);
                    ML.Result resultCURP = BL.Usuario.GetByIdCurpAndUsuarioEFLQ(usuario.IdUsuario, usuario.CURP);
                    if (resultEmail.Correct == false && resultUserName.Correct == false && resultCURP.Correct == false)
                    {
                        if (usuario.Direccion.IdDireccion == 0) // Si no tiene dirección, agregar una nueva
                        {
                            ML.Result resultDireccion = BL.Direccion.AddEFLQ(usuario);
                            if (resultDireccion.Correct)
                            {
                                usuario.Direccion.IdDireccion = (int)resultDireccion.Object;
                            }
                            else
                            {
                                TempData["Error"] = "Error al agregar la dirección: " + resultDireccion.ErrorMessage;
                                return View(usuario);
                            }
                        }
                        else // Si ya tiene dirección, actualizarla
                        {
                            ML.Result resultDireccion = BL.Direccion.UpdateEFSP(usuario);
                            if (!resultDireccion.Correct)
                            {
                                TempData["Error"] = "Error al actualizar la dirección: " + resultDireccion.ErrorMessage;
                                return View(usuario);
                            }
                        }

                        // Actualizar el usuario
                        //result = BL.Usuario.UpdateEFSP(usuario);
                        //if (result.Correct)
                        //{
                        //    TempData["Agregado"] = "Usuario actualizado correctamente.";
                        //    return RedirectToAction("GetAll");
                        //}
                        //var request = soap.Update(usuario);
                        var request = UpdateREST(usuario.IdUsuario, usuario);
                        if (request.Correct)
                        {
                            TempData["Agregado"] = "Usuario actualizado correctamente.";
                            return RedirectToAction("GetAll");
                        }
                        else
                        {
                            TempData["Error"] = "Error al actualizar el usuario: " + result.ErrorMessage;
                        }
                    }
                    else
                    {
                        ML.Result resultRoles = BL.Rol.GetAllEFLQ();
                        usuario.Rol.Roles = resultRoles.Correct ? resultRoles.Objects : new List<object>();
                        ML.Result resultEstados = BL.Estado.GetAllEFLQ();
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Correct ? resultEstados.Objects : new List<object>();
                        ML.Result resultMunicipios = BL.Municipio.GetByIdEstadoEFLQ(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Correct ? resultMunicipios.Objects : new List<object>();
                        ML.Result resultColonias = BL.Colonia.GetByIdMunicipioEFLQ(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        usuario.Direccion.Colonia.Colonias = resultColonias.Correct ? resultColonias.Objects : new List<object>();
                        // Pasar los errores específicos a la vista
                        ViewBag.EmailError = resultEmail.Correct == true ? "El correo ya está registrado" : null;
                        ViewBag.UserNameError = resultUserName.Correct == true ? "El usuario ya existe" : null;
                        ViewBag.CURPError = resultCURP.Correct == true ? "La CURP ya está registrada" : null;

                        // Mantener los datos ingresados para no perderlos
                        ViewBag.Usuario = usuario;
                        return View(usuario);
                    }
                }
            }
            else 
            {
                ML.Result resultRoles = BL.Rol.GetAllEFLQ();
                usuario.Rol.Roles = resultRoles.Correct ? resultRoles.Objects : new List<object>();
                ML.Result resultEstados = BL.Estado.GetAllEFLQ();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Correct ? resultEstados.Objects : new List<object>();
                ML.Result resultMunicipios = BL.Municipio.GetByIdEstadoEFLQ(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Correct? resultMunicipios.Objects : new List<object>();
                ML.Result resultColonias = BL.Colonia.GetByIdMunicipioEFLQ(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonias.Correct? resultColonias.Objects : new List<object>();
            }
            return View(usuario);
        }

        public ActionResult Delete(int IdUsuario)
        {
            //UsuarioServiceReference.UsuarioClient soap = new UsuarioServiceReference.UsuarioClient(); 
            //var request = soap.Delete(IdUsuario);
            var request = DeleteREST(IdUsuario);
            if (request.Correct)
            {  
               TempData["Success"] = "Usuario Eliminado Correctamente.";
            }
            else
            {
                TempData["Error"] = " Error al eliminar usuario" + request.ErrorMessage;
            }
            return RedirectToAction("GetAll");
        }

        //public ActionResult Delete(int IdUsuario)
        //{
        //    ML.Result resultUsuario = BL.Usuario.DeleteEFLQ(IdUsuario);
        //    if (resultUsuario.Correct)
        //    {
        //        int IdDireccion = (int)resultUsuario.Object;
        //        if (IdDireccion > 0)
        //        {
        //            ML.Result resultDireccion = BL.Direccion.DeleteEFLQ(IdDireccion);
        //            if (resultDireccion.Correct)
        //            {
        //                TempData["Success"] = "Usuario Eliminado Correctamente.";
        //            }
        //        }

        //        else
        //        {
        //            TempData["Error"] = " Error al eliminar usuario" + resultUsuario.ErrorMessage;
        //        }
        //    }
        //    else
        //    {
        //        TempData["Error"] = " Error al eliminar usuario" + resultUsuario.ErrorMessage;
        //    }
        //    return RedirectToAction("GetAll");
        //}

        public JsonResult MunicipioGetByIdEstado(byte IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstadoEFLQ(IdEstado);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipioEFLQ(IdMunicipio);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateIdEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result result = BL.Usuario.UpdateIdEstatusEFSP(IdUsuario, Estatus);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public ML.Result GetAllSOAP()
        {
            ML.Result result = new ML.Result();
            string action = "http://tempuri.org/IUsuario/GetAll";
            string url = "http://localhost:61175/Usuario.svc";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; //Cambiar a POST porque estas usando un servicio SOAP

            //Crear el sobre SOAP
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:tem=""http://tempuri.org/"">
                <soapenv:Header/>
                <soapenv:Body>
                <tem:GetAll/>
                </soapenv:Body> 
                </soapenv:Envelope>";

            //Enviar la Solicitud
            using(Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            //Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string resultado = reader.ReadToEnd();
                        
                        //Deserializar el XML
                        var usuarios = GetAllUsuarios(resultado);
                        result.Correct = true;
                        result.Objects = usuarios;
                    }
                }
            }
            catch (WebException ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        [NonAction]
        public ML.Result GetAllREST()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            string url = ConfigurationManager.AppSettings["UrlApi"].ToString();
            try
            {
                using(var client  = new HttpClient()) 
                {
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.GetAsync("Usuario/GetAll");
                    responseTask.Wait();

                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach(var item in readTask.Result.Objects)
                        {
                            ML.Usuario usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
                            result.Objects.Add(usuario);
                        }
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

        private static List<object> GetAllUsuarios(string xml)
        {
            ML.Result resultUsuarios = new ML.Result();
            resultUsuarios.Objects = new List<object>();
            var xdoc = XDocument.Parse(xml);
            // Acceder a GetAllUsuarioResult 
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            foreach (var elem in objects)
            {
                //IdUsuario;
                var usuario = new ML.Usuario();
                int idUsuario;
                if(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value != null)
                {
                    idUsuario = int.Parse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value);
                }
                else
                {
                    idUsuario=0;
                }
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out idUsuario); //0
                usuario.IdUsuario = idUsuario;

                //UserName
                usuario.UserName= (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ??string.Empty);
                //Nombre
                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                //Direccion
                usuario.Direccion = new ML.Direccion();
                //Calle
                var direccion = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                string calle = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuario.Direccion.Calle = calle;
                //NumeroInterior
                string numeroInterior = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroInterior = numeroInterior;
                //NumeroExterior
                string numeroExterior = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroExterior = numeroExterior;
                //Colonia
                usuario.Direccion.Colonia = new ML.Colonia();
                var colonia = direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                string nombreColonia = (string)(colonia.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.Nombre = nombreColonia;
                //CodigoPostal
                string codigoPostal = (string)(colonia.Element("{http://schemas.datacontract.org/2004/07/ML}CodigoPostal")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.CodigoPostal = codigoPostal;
                //Municipio
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                var municipio = colonia.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                string nombreMunicipio = (string)(municipio.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.Municipio.Nombre= nombreMunicipio;
                //Estado
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                var estado = municipio.Element("{http://schemas.datacontract.org/2004/07/ML}Estado");
                string nombreEstado = (string)(estado.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.Municipio.Estado.Nombre = nombreEstado;
                //Email
                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);
                //Password
                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                //FechaNacimiento
                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);
                //Sexo
                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);
                //Telefono
                usuario.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                //Celular
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                //Curp
                usuario.CURP = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty);
                //Rol
                usuario.Rol= new ML.Rol();
                var rol = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");
                string nombreRol = (string)(rol.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.Rol.Nombre= nombreRol;

                resultUsuarios.Objects.Add(usuario);
            }
                return resultUsuarios.Objects;
        }
        [NonAction]
        public ML.Result AddSOAP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string action = "http://tempuri.org/IUsuario/Add";
            string url = "http://localhost:61175/Usuario.svc";

            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
            <soapenv:Header/>
            <soapenv:Body>
                <tem:Add>
                    <tem:usuario>
                        <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
                        <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
                        <ml:CURP>{usuario.CURP}</ml:CURP>
                        <ml:Celular>{usuario.Celular}</ml:Celular>
                        <ml:Direccion>
                            <ml:IdDireccion>{usuario.Direccion.IdDireccion}</ml:IdDireccion>
                        </ml:Direccion>
                        <ml:Email>{usuario.Email}</ml:Email>
                        <ml:Estatus>{usuario.Estatus.ToString().ToLower()}</ml:Estatus>
                        <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                        <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>
                        <ml:Nombre>{usuario.Nombre}</ml:Nombre>
                        <ml:Password>{usuario.Password}</ml:Password>
                        <ml:Rol>
                            <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                        </ml:Rol>
                        <ml:Sexo>{usuario.Sexo}</ml:Sexo>
                        <ml:Telefono>{usuario.Telefono}</ml:Telefono>
                        <ml:UserName>{usuario.UserName}</ml:UserName>
                    </tem:usuario>
                </tem:Add>
             </soapenv:Body>
            </soapenv:Envelope>";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            // Enviar la solicitud 
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }
            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string resultado = reader.ReadToEnd();
                        result.Correct = true;
                    }
                }

            }

            catch (WebException ex)

            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        [NonAction]
        public ML.Result AddREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string url = ConfigurationManager.AppSettings["UrlApi"].ToString();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    //HTTP POST 
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Agregar", usuario); //Serializar
                    postTask.Wait();

                    var request = postTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        [NonAction]
        public ML.Result UpdateREST(int IdUsuario, ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string url = ConfigurationManager.AppSettings["UrlApi"].ToString();
            try {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PutAsJsonAsync<ML.Usuario>("Usuario/Actualizar/" + IdUsuario, usuario);
                    postTask.Wait();

                    var request = postTask.Result;
                    if (request.IsSuccessStatusCode)
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
                result.Correct= false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        [NonAction]
        public ML.Result DeleteREST(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            string url = ConfigurationManager.AppSettings["UrlApi"].ToString();
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    //HTTP PUT
                    var postTask = client.DeleteAsync("Usuario/Eliminar/" + IdUsuario);
                    postTask.Wait();

                    var request = postTask.Result;
                    if(request.IsSuccessStatusCode)
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

        [NonAction]
        public static ML.Result GetByIdREST(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            string url = ConfigurationManager.AppSettings["UrlApi"].ToString();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.GetAsync("Usuario/GetById/" + IdUsuario);
                    responseTask.Wait();
                    var request = responseTask.Result;
                    if(request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>(); //Deserializando Json Result
                        readTask.Wait();
                        ML.Usuario usuario = new ML.Usuario();
                        usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro al usuario.";
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