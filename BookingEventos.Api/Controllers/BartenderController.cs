using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingEventos.Api.Controllers
{
    [ApiController]
    [Route("api/bartender")]
    public class BartenderController : ControllerBase
    {
        private readonly IBartenderService _bartenderService;

        public BartenderController(IBartenderService bartenderService)
        {
            this._bartenderService = bartenderService;
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> CrearBartender([FromBody] BartenderCreacionDto datos)
        {
            var respuesta = await _bartenderService.AgregarBartender(datos);

            if (respuesta.Contains("Error") || respuesta.Contains("ya existe"))
            {
                return BadRequest(new { mensaje = respuesta });
            }

            return Ok(new { mensaje = respuesta });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
           var lista = await _bartenderService.ObtenerTodos();

            return Ok(lista);
        }
    }
}
