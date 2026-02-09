using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Services
{
    public class SalonService : ISalonService
    {
        public readonly ISalonRepository _salonRepository;

        public SalonService(ISalonRepository salonRepository)
        {
            this._salonRepository = salonRepository;
        }
        public async Task<string> AgregarSalon(SalonCreacionDto salon)
        {
            // validar si el salon ya existe
            if (await _salonRepository.ExisteSalon(salon.Nombre)) return "El salon ya esta registrado";

            Salon nuevoSalon = new Salon();
            nuevoSalon.Id = Guid.NewGuid();
            nuevoSalon.Nombre = salon.Nombre.ToLower();
            nuevoSalon.Capacidad = salon.Capacidad;
            nuevoSalon.PrecioPorHora = salon.PrecioPorHora;

            try
            {
                await _salonRepository.AgregarSalon(nuevoSalon);
                return "Salon creado correctamente";
            }
            catch (Exception ex) {
                return "Error crítico: No se pudo guardar la salon en la base de datos.";
            }

        }

        public Task<Salon> ObtenerPorNombre(Salon salon)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Salon>> ObtenerTodos()
        {
            return await _salonRepository.ObtenerTodos();
        }
    }
}
