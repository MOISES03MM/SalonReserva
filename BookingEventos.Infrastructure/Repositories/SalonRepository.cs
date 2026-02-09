using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using BookingEventos.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Infrastructure.Repositories
{
    public class SalonRepository : ISalonRepository
    {

        public readonly ApplicationDbContext _dbContext;

        public SalonRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AgregarSalon(Salon salon)
        {
            await _dbContext.Salones.AddAsync(salon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExisteSalon(string nombre)
        {
            return await _dbContext.Salones.AnyAsync(s => s.Nombre == nombre);
        }

        public async Task<Salon> ObtenerPorId(Guid id)
        {
            return await _dbContext.Salones.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Salon>> ObtenerTodos()
        {
            return await _dbContext.Salones.ToListAsync();
        }
    }
}
