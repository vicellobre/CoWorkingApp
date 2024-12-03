using CoWorkingApp.Core.Entities;
using CoWorkingApp.Persistence.Constants;
using CoWorkingApp.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoWorkingApp.Persistence.Configurations;

/// <summary>
/// Configura la entidad <see cref="Reservation"/> para el modelo de la base de datos a través de Fluent API.
/// </summary>
public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    /// <summary>
    /// Configura las propiedades y las relaciones de la entidad <see cref="Reservation"/>.
    /// </summary>
    /// <param name="builder">El constructor de la entidad <see cref="Reservation"/>.</param>
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable(TableNames.Reservations);
        builder.HasKey(r => r.Id);

        // Configuración de la fecha de la reserva con DateConverter
        builder.Property(r => r.Date)
               .IsRequired()
               .HasConversion(new DateConverter());

        // Relación con User
        builder.HasOne(r => r.User)
               .WithMany(u => u.Reservations)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relación con Seat
        builder.HasOne(r => r.Seat)
               .WithMany(s => s.Reservations)
               .HasForeignKey(r => r.SeatId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
