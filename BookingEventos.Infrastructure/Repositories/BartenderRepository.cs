using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using BookingEventos.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingEventos.Infrastructure.Repositories
{
    public class BartenderRepository : IBartenderRepository
    {

        public readonly ApplicationDbContext _dbContext;

        public BartenderRepository(ApplicationDbContext dbContext)
        {
        this._dbContext = dbContext;
        }
        public async Task AgregarBartender(Bartender bartender)
        {
            await _dbContext.AddAsync(bartender);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ObtenerPorCedula(string cedula)
        {
            return await _dbContext.Bartenders.AnyAsync(b => b.Cedula == cedula);
        }

        public async Task<Bartender> obtenerPorId(Guid id)
        {
            return await _dbContext.Bartenders.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Bartender>> obtenerTodos()
        {
            return await _dbContext.Bartenders.ToListAsync();
        }
    }
}
