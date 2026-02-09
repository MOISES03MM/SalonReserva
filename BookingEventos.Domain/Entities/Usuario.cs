using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BookingEventos.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public Rol Rol { get; set; }

        public bool TieneEmailValido()
        {
            if (string.IsNullOrWhiteSpace(Email)) return false;

            // Usamos una expresión regular básica para validar el formato del correo
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(Email);
        }

        public bool ValidarFortalezaPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            // Validamos que tenga al menos 8 caracteres, una mayúscula y un número
            return password.Length >= 8 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[0-9]");
        }

    }
}
