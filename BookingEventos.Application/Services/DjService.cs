using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Services
{
    public class DjService : IDjService
    {

        public readonly IDjRepository _djRepository;

        public DjService(IDjRepository djRepository)
        {
            this._djRepository = djRepository;
        }
        public async Task<string> AgregarDj(DjCreacionDto dj)
        {
            // validar que no exista otro usuario con esa cedula
            if (await _djRepository.ObtenerPorCedula(dj.Cedula)) return "Ya esta registrado ese dj";


            Dj nuevoDj = new Dj();

            nuevoDj.Id = Guid.NewGuid();
            nuevoDj.Nombre = dj.Nombre;
            nuevoDj.Cedula = dj.Cedula;
            nuevoDj.NombreArtistico = dj.NombreArtistico;
            nuevoDj.PrecioPorHora = dj.PrecioPorHora;
            nuevoDj.Generos = dj.GenerosId.Select(idGenero => new GeneroMusical
            {
                Id = idGenero
            }).ToList();

            try
            {
                await _djRepository.AgregarDj(nuevoDj);
                return $"DJ {nuevoDj.NombreArtistico} registrado con éxito.";
            }
            catch (Exception ex) {
                return "Error crítico: No se pudo guardar el DJ. Detalles: " + ex.Message;
            }

        }

        public Task<Dj?> ObtenerPorCedula(string cedula)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dj>> ObtenerTodos()
        {
            return _djRepository.ObtenerTodo();
        }
    }
}
