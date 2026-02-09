using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.DTOs
{
    public class BartenderCreacionDto
    {
        public string Nombre { get; set; }

        public string Cedula { get; set; }
        public decimal PrecioPorHora { get; set; }
        public string NombreArtistico { get; set; }

        public List<Guid> TipoCoptel { get; set; }
    }
}
