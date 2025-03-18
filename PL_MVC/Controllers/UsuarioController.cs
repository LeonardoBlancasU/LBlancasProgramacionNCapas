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
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Formulario(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result resultRol = BL.Rol.GetAll();
            usuario.Rol.Roles = resultRol.Objects;

            if (IdUsuario == 0)
            {
                BL.Usuario.AddSP(usuario);
                TempData["Agregado"] = "Usuario Agregado Correctamente.";
            }
            else 
                if (IdUsuario != null && IdUsuario > 0)
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects;
                    
                }
            }

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario, HttpPostedFileBase ImagenFile) {
            

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

            if (usuario.IdUsuario == 0)
            {
               
                result = BL.Usuario.AddSP(usuario);
                TempData["Agregado"] = "Usuario Agregado Correctamente.";
            }
            else
            {

                BL.Usuario.UpdateSP(usuario);
                TempData["Actualizado"] = "Usuario Actualizado Correctamente.";

            }

            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            return View(usuario);
        }

        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteSP(IdUsuario);
            if (result.Correct)
            {
                TempData["Success"] = "Usuario Eliminado Correctamente.";
            }
            else
            {
                TempData["Error"] = " Error al eliminar usuario" + result.ErrorMessage;
            }
            return RedirectToAction("GetAll");
        }

        //var _img = document.getElementById('id1');
        //var newImg = new Image;
        //newImg.onload = function()
        //{
        //    _img.src = this.src;
        //}
        //newImg.src = 'http://whatever';
    }
}