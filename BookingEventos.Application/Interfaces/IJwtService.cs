using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerarToken(string email, Rol rol);
    }
}
