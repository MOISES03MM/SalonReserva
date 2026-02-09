using BookingEventos.Application.DTOs;
using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Interfaces
{
    public interface IBartenderService
    {
        Task<string> AgregarBartender(BartenderCreacionDto bartender);

        Task<Bartender> ObtenerPorCedula(string cedula);

        Task<IEnumerable<Bartender>> ObtenerTodos();


    }
}
