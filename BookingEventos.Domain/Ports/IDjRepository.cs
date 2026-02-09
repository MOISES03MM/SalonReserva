using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Ports
{
    public interface IDjRepository
    {
        Task AgregarDj(Dj dj);
        Task<Dj> ObtenerPorId(Guid id); // obtener por id
        Task<bool> ObtenerPorCedula(string  cedula); // obtener por id
        Task<IEnumerable<Dj>> ObtenerTodo(); // obtener por id
    }
}
