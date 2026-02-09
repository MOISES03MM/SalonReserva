using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingEventos.Api.Controllers
{
    [Route("api/salones")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly ISalonService _salonService;

        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodo() => Ok(await _salonService.ObtenerTodos());

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] SalonCreacionDto dto)
        {
            await _salonService.AgregarSalon(dto);
            return Ok(new { mensaje = "Salón creado correctamente" });
        }
    }
}
