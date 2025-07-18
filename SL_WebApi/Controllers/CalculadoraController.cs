using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api/Calculadora")]
    public class CalculadoraController : ApiController
    {
        [HttpPost]
        [Route("Suma")]
        //[Route("Suma/{Numero1}/{Numero2}")]
        public IHttpActionResult Suma([FromBody] Models.Calculadora calculadora)
        {
            return Content(HttpStatusCode.OK, calculadora.Numero1 + calculadora.Numero2);
        }

        [HttpPost]
        [Route("Resta")]
        //[Route("Resta/{Numero1}/{Numero2}")]
        public IHttpActionResult Resta([FromBody] Models.Calculadora calculadora)
        {
            return Content(HttpStatusCode.OK, calculadora.Numero1 - calculadora.Numero2);
        }

        [HttpPost]
        [Route("Multiplicacion")]
        //[Route("Multiplicacion/{Numero1}/{Numero2}")]
        public IHttpActionResult Multiplicacion([FromBody] Models.Calculadora calculadora)
        {
            return Content(HttpStatusCode.OK, calculadora.Numero1 * calculadora.Numero2);
        }

        [HttpPost]
        [Route("Division")]
        //[Route("Division/{Numero1}/{Numero2}")]
        public IHttpActionResult Division([FromBody] Models.Calculadora calculadora)
        {
            return Content(HttpStatusCode.OK, calculadora.Numero1 / calculadora.Numero2);
        }
    }
}
