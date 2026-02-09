using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Ports
{
    public interface ISalonRepository
    {
        Task AgregarSalon(Salon salon);
        Task<Salon> ObtenerPorId(Guid id);

        Task<bool> ExisteSalon(string nombre);


        Task<IEnumerable<Salon>> ObtenerTodos();
    }
}
