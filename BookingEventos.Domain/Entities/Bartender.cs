using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Entities
{
    public class Bartender
    {
        public Guid Id {  get; set; }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string NombreArtistico { get; set; }
        public virtual ICollection<TipoCoptel> Copteles { get; set; }
        public decimal PrecioPorHora { get; set; }
    }
}
