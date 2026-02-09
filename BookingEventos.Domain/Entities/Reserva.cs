using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Entities
{
    public class Reserva
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Guid SalonId { get; set; }
        public Guid? DjId { get; set; }
        public Guid? BartenderId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin {  get; set; }
        public DateTime? DjInicio { get; set; }
        public DateTime? DjFin { get; set; }
        public DateTime? BartenderInicio { get; set; }
        public DateTime? BartenderFin { get; set; }

        public int CantidadInvitados { get; set; }

        public decimal MontoTotal { get; set; }



        // metodo de solapamiento 
        public bool SeSolapaCon(DateTime inicio,  DateTime fin)
        {
            return inicio < FechaFin && fin > FechaInicio;
        }

        // metodo de horario
        public bool validarHorariosStaff()
        {
            //validarDj primero
            if(DjId.HasValue  && DjInicio.HasValue && DjFin.HasValue)
            {
                if (DjInicio < FechaInicio || DjFin > FechaFin) return false;
            }

            // validar bartender
            if (BartenderId.HasValue && BartenderInicio.HasValue && BartenderFin.HasValue)
            {
                if (BartenderInicio < FechaInicio || BartenderFin > FechaFin) return false;
            }

            return true;
        }

        // metodo de cantidad de invitados
        public bool validarCantidadInvitados(int cantidadMaxima)
        {
            if (CantidadInvitados > cantidadMaxima)
            {
                return false;
            }

            return true;
        }

        public void CalcularMontoTotal (decimal precioSalonPorHora, decimal precioDjPorHora, decimal precioBartenderPorHora)
        {
            // calcular horas del salon
            var horasSalon = (decimal)(FechaFin - FechaInicio).TotalHours;
            decimal total = horasSalon * precioSalonPorHora;

            // sumar costo de dj ( si es que aplica)
            if(DjId.HasValue && DjInicio.HasValue && DjFin.HasValue)
            {
                var horasDj = (decimal)(DjFin.Value - DjInicio.Value).TotalHours;
                total += horasDj * precioDjPorHora;
            }

            // sumar el costo del bartender ( si esque aplica)
            if(BartenderId.HasValue && BartenderInicio.HasValue && BartenderFin.HasValue)
            {
                var horasBartender = (decimal)(BartenderFin.Value - BartenderInicio.Value).TotalHours;
                total += horasBartender * precioBartenderPorHora;
            }

            this.MontoTotal = total;
        }

    }
}
