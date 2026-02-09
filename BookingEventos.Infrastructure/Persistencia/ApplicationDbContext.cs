using BookingEventos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingEventos.Infrastructure.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Definición de las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Dj> Djs { get; set; }
        public DbSet<Bartender> Bartenders { get; set; }
        public DbSet<Salon> Salones { get; set; }
        public DbSet<GeneroMusical> GeneroMusicales { get; set; }
        // Nueva tabla para los tipos de cócteles
        public DbSet<TipoCoptel> TipoCopteles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Muchos a Muchos (DJ <-> Generos)
            modelBuilder.Entity<Dj>()
                .HasMany(d => d.Generos)
                .WithMany()
                .UsingEntity(j => j.ToTable("DjGeneros"));

            // NUEVA: Relación Muchos a Muchos (Bartender <-> TipoCoptel)
            modelBuilder.Entity<Bartender>()
                .HasMany(b => b.Copteles)
                .WithMany()
                .UsingEntity(j => j.ToTable("BartenderCopteles"));

            // Precisión para dinero (Importante en MySQL para tipos decimal)
            modelBuilder.Entity<Reserva>()
                .Property(r => r.MontoTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Bartender>()
                .Property(b => b.PrecioPorHora)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Dj>()
                .Property(d => d.PrecioPorHora)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}