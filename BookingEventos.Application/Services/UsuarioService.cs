using BookingEventos.Application.DTOs;
using BookingEventos.Application.Interfaces;
using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Application.Services
{
    public class UsuarioService : IUsuarioService
    {

        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IJwtService _jwtService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IJwtService jwtService  ) // constructor para la inyeccion de dependencia
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Login(string email, string password)
        {
            // validar si el usuario existe con ese gmail
            Usuario usuario =await _usuarioRepository.GetEmailAsync(email);
            if (usuario == null) return "No existe una cuenta registrada con ese correo";


            //validar si la contrasena de ese gmail es correcta
            bool passwordValido = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash); // contrasena ingresada - contrasena hasheada
            if (!passwordValido) return "Contrasena incorrecta";
            // devolver un string de todo correcto si es que esta bien( mas adelante pondre jwt)
            return _jwtService.GenerarToken(usuario.Email, usuario.Rol);

        }

        public async Task<string> RegistroUsuario(UsuarioRegistroDto datos)
        {
            // validar si correo ya existe
            if( await _usuarioRepository.existeUsuario(datos.Email)) return "Error: El correo electrónico ya está registrado.";

            // creacion de nuevo usuario
            Usuario nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = datos.Nombre,
                Apellido = datos.Apellido,
                Email = datos.Email,
                Rol = datos.Rol ?? Rol.CLIENTE
            };

            // validaciones dentro del usuario

            if (!nuevoUsuario.TieneEmailValido()) return  "Error: El formato del correo no es válido."; // validar el email
            if (!nuevoUsuario.ValidarFortalezaPassword(datos.Password)) return "Error: La contraseña debe tener al menos 8 caracteres."; // validar la contrasena


            nuevoUsuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(datos.Password, 10); //encriptar la contrasena

            await _usuarioRepository.AgregarUsuario(nuevoUsuario); // mandamos a la bd

            return "Usuario creado con exito";
        }
    }
}
