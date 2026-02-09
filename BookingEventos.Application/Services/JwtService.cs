using BookingEventos.Application.Interfaces;
using BookingEventos.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingEventos.Application.Services
{
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string GenerarToken(string email, Rol rol)
        {
            // creamos una variable
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, email), // guarda el email del usuario
                new Claim(ClaimTypes.Role, rol.ToString()), // guarda el rol del ususario
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // es el id unico del token para que nunca se vuelva a repetir
             };

            // TRAEMOS LA CLAVE SECRETA DESDE EL APPSETTING.JSON
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // crear el token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
