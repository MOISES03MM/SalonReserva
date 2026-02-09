using BookingEventos.Application.DTOs;
using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Interfaces
{
    public interface ISalonService
    {
        Task<string> AgregarSalon(SalonCreacionDto salon);
        Task<Salon> ObtenerPorNombre(Salon salon);

        Task<IEnumerable<Salon>> ObtenerTodos();
    }
}
