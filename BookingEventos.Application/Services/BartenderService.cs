using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Services
{
    public class BartenderService : IBartenderService
    {
        public readonly IBartenderRepository _bartenderRepository;
        public BartenderService(IBartenderRepository bartenderRepository) { 
        this._bartenderRepository = bartenderRepository;
        }


        public async Task<string> AgregarBartender(BartenderCreacionDto bartender)
        {
            // validar si existe
            if (await _bartenderRepository.ObtenerPorCedula(bartender.Cedula)) return "Bartender ya esta registrado";

            Bartender nuevoBartender = new Bartender();
            nuevoBartender.Id = Guid.NewGuid();
            nuevoBartender.Nombre = bartender.Nombre;
            nuevoBartender.NombreArtistico = bartender.NombreArtistico;
            nuevoBartender.Cedula = bartender.Cedula;
            nuevoBartender.PrecioPorHora = bartender.PrecioPorHora;
            nuevoBartender.Copteles = bartender.TipoCoptel.Select(coptel => new TipoCoptel
            {
                Id = coptel
            }).ToList();

            try
            {
                await _bartenderRepository.AgregarBartender(nuevoBartender);
                return "Bartender agregado con exito";
            }
            catch (Exception ex) {
                return "Error crítico: No se pudo guardar el DJ. Detalles: " + ex.Message;
            }
        }

        public Task<Bartender> ObtenerPorCedula(string cedula)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bartender>> ObtenerTodos()
        {
            return await _bartenderRepository.obtenerTodos();
        }
    }
}
