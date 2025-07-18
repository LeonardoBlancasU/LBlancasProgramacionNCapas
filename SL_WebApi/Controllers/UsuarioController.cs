using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("Agregar")]
        public IHttpActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.AddEFSP(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);

            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPut]
        [Route("Actualizar/{IdUsuario}")]
        public IHttpActionResult Update(int IdUsuario, [FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = IdUsuario;

            ML.Result result = new ML.Result();
            result = BL.Usuario.UpdateEFSP(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);

            } else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdUsuario}")]
        public IHttpActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.Delete(IdUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);

            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.GetAllEFSP();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);

            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.GetByIdEFSP(IdUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);

            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
