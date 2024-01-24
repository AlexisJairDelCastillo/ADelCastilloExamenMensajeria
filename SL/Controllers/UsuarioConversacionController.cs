using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [EnableCors("AnotherPolicy")]
    [Route("api/usuarioconversacion")]
    [ApiController]
    public class UsuarioConversacionController : ControllerBase
    {
        [HttpGet]
        [Route("getall/{idUsuario}")]
        public IActionResult GetAll(int idUsuario)
        {
            ML.Result result = BL.UsuarioConversacion.GetAll(idUsuario);

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
