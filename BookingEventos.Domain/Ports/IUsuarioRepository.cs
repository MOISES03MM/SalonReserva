using BookingEventos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Domain.Ports
{
    public interface IUsuarioRepository
    {
        Task AgregarUsuario(Usuario usuario);// metodo para crear usuario
        Task<Usuario?> GetEmailAsync(string email);// metodo para obtener por emain
        Task<bool> existeUsuario(string email);//metodo para validar si existe un usuario con un gmail, para crear las cuentas
    }
}
