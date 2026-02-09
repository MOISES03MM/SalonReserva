using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.DTOs
{
    public class ReservaCreacionDto
    {
        public Guid SalonId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin {  get; set; }
        public int CantidadInvitados { get; set; }

        public Guid? DjId { get; set; }
        public DateTime? DjInicio { get; set; }
        public DateTime? DjFin { get; set; }

        public Guid? BartenderId { get; set; }
        public DateTime? BartenderInicio { get; set; }
        public DateTime? BartenderFin { get; set; }
    }
}
