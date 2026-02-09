using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.DTOs
{
    public class DjCreacionDto
    {
        public string Nombre { get; set; }

        public string Cedula { get; set; }
        public string NombreArtistico { get; set; }

        public decimal PrecioPorHora { get; set; }
        public List<Guid> GenerosId {  get; set; }
    }
}
