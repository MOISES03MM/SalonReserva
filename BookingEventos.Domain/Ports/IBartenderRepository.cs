using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Ports
{
    public interface IBartenderRepository
    {
        Task AgregarBartender(Bartender bartender);
        Task<Bartender> obtenerPorId(Guid id); // metodo para obtener por id
        Task<bool> ObtenerPorCedula(string cedula);

        Task<IEnumerable<Bartender>> obtenerTodos(); // metodo para obtener por id
    }
}
