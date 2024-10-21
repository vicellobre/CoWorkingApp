using CoWorkingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.API.Infrastructure.Context
{
    /// <summary>
    /// Clase que representa el contexto de la base de datos para la aplicación CoWorking.
    /// </summary>
    public class CoWorkingContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración del contexto.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto.</param>
        public CoWorkingContext(DbContextOptions<CoWorkingContext> options) : base(options) { }

        /// <summary>
        /// DbSet que representa la tabla de usuarios en la base de datos.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet que representa la tabla de asientos en la base de datos.
        /// </summary>
        public DbSet<Seat> Seats { get; set; }

        /// <summary>
        /// DbSet que representa la tabla de reservas en la base de datos.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }


        /// <summary>
        /// Método que se llama al crear el modelo de la base de datos y define las configuraciones adicionales.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de la base de datos.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreateUser(modelBuilder);
            ModelCreateSeat(modelBuilder);
            ModelCreateReservation(modelBuilder);
        }

        /// <summary>
        /// Configuración del modelo de la entidad User a través de Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de la base de datos.</param>
        private void ModelCreateUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users");
                user.HasKey(u => u.Id);
                user.Property(u => u.Name).IsRequired().HasMaxLength(50);
                user.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                user.Property(u => u.Email).IsRequired().HasMaxLength(100);
                user.Property(u => u.Password).IsRequired().HasMaxLength(50);
                user.HasIndex(u => u.Email).IsUnique();

                // Ignorar la colección Reservations en la entidad User
                user.Ignore(u => u.Reservations);
            });
        }

        /// <summary>
        /// Configuración del modelo de la entidad Reservation a través de Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de la base de datos.</param>
        private void ModelCreateReservation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(reservation =>
            {
                reservation.ToTable("Reservations");
                reservation.HasKey(r => r.Id);
                reservation.Property(r => r.Date).IsRequired();

                // Relación con User
                reservation.HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación con Seat
                reservation.HasOne(r => r.Seat)
                    .WithMany(s => s.Reservations)
                    .HasForeignKey(r => r.SeatId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        /// <summary>
        /// Configuración del modelo de la entidad Seat a través de Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de la base de datos.</param>
        private void ModelCreateSeat(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>(seat =>
            {
                seat.ToTable("Seats");
                seat.HasKey(s => s.Id);
                seat.Property(s => s.Name).IsRequired().HasMaxLength(50);
                seat.Property(s => s.IsBlocked).IsRequired();
                seat.Property(s => s.Description).HasMaxLength(255);
                seat.HasIndex(s => s.Name).IsUnique();

                // Ignorar la colección Reservations en la entidad Seat
                seat.Ignore(s => s.Reservations);
            });
        }
    }
}
