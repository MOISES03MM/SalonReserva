using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Ports
{
    public interface IReservaRepository
    {
        Task AgregarReserva(Reserva reserva);

        Task<IEnumerable<Reserva>> ObtenerPorSalonYFecha(Guid salonId, DateTime fechaInicio, DateTime fechaFin);

        Task<IEnumerable<Reserva>> ObtenerPorTrabajadorYFecha(Guid trabajadorId, DateTime fechaInicio, DateTime fechaFin);
    }
}
