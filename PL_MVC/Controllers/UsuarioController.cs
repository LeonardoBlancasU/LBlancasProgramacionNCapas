using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAllEFLQ();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            usuario.Direccion = new ML.Direccion();
            
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

                ML.Result result = BL.Usuario.GetByIdEFLQ(IdUsuario.Value);

                if (result.Correct == true)
                {
                    usuario = (ML.Usuario)result.Object;
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

                        result = BL.Usuario.AddEFLQ(usuario);
                        if (result.Correct)
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
                    ML.Result resultDireccion = BL.Direccion.UpdateEFLQ(usuario);
                    if (!resultDireccion.Correct)
                    {
                        TempData["Error"] = "Error al actualizar la dirección: " + resultDireccion.ErrorMessage;
                        return View(usuario);
                    }
                }

                // Actualizar el usuario
                result = BL.Usuario.UpdateEFLQ(usuario);
                if (result.Correct)
                {
                    TempData["Agregado"] = "Usuario actualizado correctamente.";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["Error"] = "Error al actualizar el usuario: " + result.ErrorMessage;
                }
            }

            return View(usuario);
        }

        public ActionResult Delete(int IdUsuario)
        {
            ML.Result resultUsuario = BL.Usuario.DeleteEFLQ(IdUsuario);
            if (resultUsuario.Correct)
            {
                int IdDireccion = (int)resultUsuario.Object;
                if (IdDireccion > 0)
                {
                    ML.Result resultDireccion = BL.Direccion.DeleteEFLQ(IdDireccion);
                    if (resultDireccion.Correct)
                    {
                        TempData["Success"] = "Usuario Eliminado Correctamente.";
                    }
                }

                else
                {
                    TempData["Error"] = " Error al eliminar usuario" + resultUsuario.ErrorMessage;
                }
            }
            else
            {
                TempData["Error"] = " Error al eliminar usuario" + resultUsuario.ErrorMessage;
            }
            return RedirectToAction("GetAll");
        }

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


    }
}