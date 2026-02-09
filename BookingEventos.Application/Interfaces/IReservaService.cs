using BookingEventos.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Interfaces
{
    public interface IReservaService
    {
        Task<string> CrearReserva(ReservaCreacionDto datos);
    }
}
