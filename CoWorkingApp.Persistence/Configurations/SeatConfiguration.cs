using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Persistence.Constants;
using CoWorkingApp.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoWorkingApp.Persistence.Configurations;

/// <summary>
/// Configura la entidad <see cref="Seat"/> para el modelo de la base de datos a través de Fluent API.
/// </summary>
public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    /// <summary>
    /// Configura las propiedades y las relaciones de la entidad <see cref="Seat"/>.
    /// </summary>
    /// <param name="builder">El constructor de la entidad <see cref="Seat"/>.</param>
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable(TableNames.Seats);
        builder.HasKey(s => s.Id);
        
        // Configuración del nombre del asiento con SeatNameConverter
        builder.Property(s => s.Name)
               .IsRequired()
               .HasConversion(new SeatNameConverter())
               .HasMaxLength(SeatName.MaxLength);

        // Configuración de la descripción del asiento con DescriptionConverter
        builder.Property(s => s.Description)
               .IsRequired()
               .HasConversion(new DescriptionConverter())
               .HasMaxLength(Description.MaxLength);
               
        builder.HasIndex(s => s.Name)
               .IsUnique();

        // Ignorar la colección Reservations en la entidad Seat
        builder.Ignore(s => s.Reservations);
    }
}
