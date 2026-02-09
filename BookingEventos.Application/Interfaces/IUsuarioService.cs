using BookingEventos.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> RegistroUsuario(UsuarioRegistroDto datos);// metodo para el registro, entra con el dto
        Task<string> Login(string email, string password); // metodo para el login, entra con el dto 
    }
}
