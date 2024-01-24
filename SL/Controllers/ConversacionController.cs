using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [EnableCors("AnotherPolicy")]
    [Route("api/conversacion")]
    [ApiController]
    public class ConversacionController : ControllerBase
    {
        [HttpPost]
        [Route("add/{idUsuario}")]
        public IActionResult Add(int idUsuario, [FromBody] ML.Conversacion conversacion)
        {
            ML.Result result = BL.Conversacion.Add(conversacion);

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
