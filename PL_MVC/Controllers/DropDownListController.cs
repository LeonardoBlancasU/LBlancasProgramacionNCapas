using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class DropDownListController : Controller
    {
        // GET: DropDownList
        public ActionResult DropDownList()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultEstados = BL.Estado.GetAll();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            if (resultEstados.Correct == true)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
            }
            else
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = new List<object>();
            }
            return View(usuario);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}