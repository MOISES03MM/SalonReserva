using BookingEventos.Infrastructure.Persistencia;
using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace BookingEventos.Infrastructure.Repositories
{
    // Cambiado a public para que sea accesible
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AgregarUsuario(Usuario usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> existeUsuario(string email)
        {
            return await _dbContext.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetEmailAsync(string email)
        {
            return await _dbContext.Usuarios
        .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}