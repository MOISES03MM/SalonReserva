using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingEventos.Api.Controllers
{
    [Route("api/djs")]
    [ApiController]
    public class DjController : ControllerBase
    {
        private readonly IDjService _djService;

        public DjController(IDjService djService)
        {
            _djService = djService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodo() => Ok(await _djService.ObtenerTodos());

        [Authorize(Roles = "ADMINISTRADOR")]// solo el administyrador puede ejecutar esto
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] DjCreacionDto dto)
        {
            await _djService.AgregarDj(dto);
            return Ok(new { mensaje = "DJ registrado exitosamente" });
        }
    }
}
