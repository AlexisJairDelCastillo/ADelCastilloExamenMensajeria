using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [EnableCors("AnotherPolicy")]
    [Route("api/mensaje")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        [HttpGet]
        [Route("getall/{idUsuarioConversacion}")]
        public IActionResult GetAll(int idUsuarioConversacion)
        {
            ML.Result result = BL.Mensaje.GetAll(idUsuarioConversacion);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ML.Mensaje mensaje)
        {
            ML.Result result = BL.Mensaje.Add(mensaje);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
