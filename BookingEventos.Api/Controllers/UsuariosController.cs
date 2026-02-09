using BookingEventos.Application.Interfaces;
using BookingEventos.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookingEventos.Api.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController: ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController (IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto registro)
        {
            var resultado = await _usuarioService.RegistroUsuario(registro);

            if (resultado.Contains("Error") || resultado.Contains("ya existe"))
            {
                return BadRequest(new { mensaje = resultado });
            }

            return Ok(new { mensaje = resultado });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            // El servicio valida y nos devuelve el TOKEN o null
            var token = await _usuarioService.Login(loginDto.Email, loginDto.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos" });
            }

            // Devolvemos el token en un objeto JSON para que el cliente lo guarde
            return Ok(new { token = token });
        }
    }
}
