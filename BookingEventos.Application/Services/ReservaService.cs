using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Exception;
using BookingEventos.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Services
{
    public class ReservaService : IReservaService
    {

        public readonly IReservaRepository _reservaRepository;
        public readonly IBartenderRepository _bartenderRepository;
        public readonly IDjRepository _jjRepository;
        public readonly ISalonRepository _salonRepository;

        public ReservaService(IReservaRepository reservaRepository, IBartenderRepository bartenderRepository, IDjRepository djRepository, ISalonRepository salonRepository )
        {
            this._reservaRepository = reservaRepository;
            this._bartenderRepository = bartenderRepository;
            this._jjRepository = djRepository;
            this._salonRepository = salonRepository;

        }



        public async Task<string> CrearReserva(ReservaCreacionDto datos)
        {
            var reservasSalon = await _reservaRepository.ObtenerPorSalonYFecha(datos.SalonId, datos.FechaInicio, datos.FechaFin);
            if (reservasSalon.Any()) throw new NegocioException("El salón ya se encuentra reservado para la fecha y hora seleccionadas.");




            if (datos.DjId.HasValue)
            {
                var reservasDj = await _reservaRepository.ObtenerPorTrabajadorYFecha(datos.DjId.Value, datos.FechaInicio, datos.FechaFin);
                if (reservasDj.Any()) throw new NegocioException("El DJ seleccionado ya tiene un compromiso en ese horario.");
            }

            
            if (datos.BartenderId.HasValue)
            {
                var reservasBartender = await _reservaRepository.ObtenerPorTrabajadorYFecha(datos.BartenderId.Value, datos.FechaInicio, datos.FechaFin);
                if (reservasBartender.Any()) throw new NegocioException("El Bartender seleccionado ya tiene un compromiso en ese horario.");
            }

            
            var salon = await _salonRepository.ObtenerPorId(datos.SalonId); // obtener el id del salon que se quiere reservar
            if (salon == null) throw new NegocioException("El salón seleccionado no existe en el sistema.");


            var dj = datos.DjId.HasValue ? await _jjRepository.ObtenerPorId(datos.DjId.Value) : null; // obtenemos el dj si esque existe
            var bartender = datos.BartenderId.HasValue ? await _bartenderRepository.obtenerPorId(datos.BartenderId.Value) : null; // obtenemos el bartender si esque existe

            var nuevaReserva = new Reserva
            {
                Id = Guid.NewGuid(),
                ClienteId = datos.UsuarioId,
                SalonId = datos.SalonId,
                DjId = datos.DjId,
                BartenderId = datos.BartenderId,
                FechaInicio = datos.FechaInicio,
                FechaFin = datos.FechaFin,
                DjInicio = datos.DjInicio,
                DjFin = datos.DjFin,
                BartenderInicio = datos.BartenderInicio,
                BartenderFin = datos.BartenderFin,
                CantidadInvitados = datos.CantidadInvitados
            };

           

            if (!nuevaReserva.validarHorariosStaff()) throw new NegocioException("Los horarios asignados al staff deben estar dentro del rango de inicio y fin del evento.");// Validar los horarios de entrada y salida


            if (!nuevaReserva.validarCantidadInvitados(salon.Capacidad)) throw new NegocioException($"La cantidad de invitados ({datos.CantidadInvitados}) supera la capacidad máxima del salón ({salon.Capacidad}).");

            nuevaReserva.CalcularMontoTotal(
                salon.PrecioPorHora,
                dj?.PrecioPorHora ?? 0,
                bartender?.PrecioPorHora ?? 0
            );

            
                await _reservaRepository.AgregarReserva(nuevaReserva);
                return $"Reserva creada con éxito. El monto total calculado es: ${nuevaReserva.MontoTotal}";
            
        }
    }
}
