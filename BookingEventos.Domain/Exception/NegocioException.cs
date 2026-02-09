using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Exception
{
    public class NegocioException : System.Exception
    {
        public NegocioException(string mensaje) : base(mensaje) { }
    }
}
