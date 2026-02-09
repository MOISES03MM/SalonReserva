using BookingEventos.Application.DTOs;
using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Interfaces
{
    public interface IDjService
    {
        Task<string> AgregarDj(DjCreacionDto dj);
        Task<Dj?> ObtenerPorCedula(string cedula);

        Task<IEnumerable<Dj>> ObtenerTodos();
    }
}
