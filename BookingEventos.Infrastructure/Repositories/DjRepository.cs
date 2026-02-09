using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using BookingEventos.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Infrastructure.Repositories
{
    public class DjRepository : IDjRepository
    {

        public readonly ApplicationDbContext _dbContext;

        public DjRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AgregarDj(Dj dj)
        {
            await _dbContext.AddAsync(dj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ObtenerPorCedula(string cedula)
        {
            return await _dbContext.Djs.AnyAsync(d => d.Cedula == cedula);
        }

        public async Task<Dj> ObtenerPorId(Guid id)
        {
            return await _dbContext.Djs.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Dj>> ObtenerTodo()
        {
            return await _dbContext.Djs.ToListAsync();
        }
    }
}
