using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingEventos.Api.Controllers
{
    [Route("api/reservas")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] ReservaCreacionDto dto)
        {
            try
            {
                var resultado = await _reservaService.CrearReserva(dto);
                return Ok(new { reservaId = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

       
    }
}
