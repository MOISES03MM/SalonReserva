using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.DTOs
{
    public class UsuarioRegistroDto
    {
        public string Nombre {  get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Rol? Rol { get; set; }
    }
}
