using BookingEventos.Infrastructure.Persistencia;
using BookingEventos.Domain.Entities;
using BookingEventos.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace BookingEventos.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AgregarReserva(Reserva reserva)
        {
            
            await _dbContext.Reservas.AddAsync(reserva);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Reserva>> ObtenerPorSalonYFecha(Guid salonId, DateTime fechaInicio, DateTime fechaFin)
        {
            return await _dbContext.Reservas.Where(r => r.SalonId == salonId && r.FechaInicio == fechaInicio && r.FechaFin == fechaFin).ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorTrabajadorYFecha(Guid trabajadorId, DateTime fechaInicio, DateTime fechaFin)
        {
            return await _dbContext.Reservas
        .Where(r => (r.DjId == trabajadorId || r.BartenderId == trabajadorId) // Filtramos por cualquiera de los dos roles
                    && r.FechaInicio < fechaFin
                    && fechaInicio < r.FechaFin) // Aplicamos la lógica de solapamiento
        .ToListAsync();
        }
    }
}