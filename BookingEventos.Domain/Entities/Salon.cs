using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Entities
{
    public class Salon
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal PrecioPorHora { get; set; }
    }
}
