using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.DTOs
{
    public class SalonCreacionDto
    {
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal PrecioPorHora { get; set; }

    }
}
