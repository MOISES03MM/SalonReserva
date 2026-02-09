using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Entities
{
    public class Dj
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

        public string Cedula { get; set; }
        public string NombreArtistico { get; set; }
        public virtual ICollection<GeneroMusical> Generos {  get; set; }
        public decimal PrecioPorHora { get; set; }
    }
}
