using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoWorkingApp.Persistence.Configurations;

/// <summary>
/// Configura la entidad <see cref="User"/> para el modelo de la base de datos a través de Fluent API.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configura las propiedades y las relaciones de la entidad <see cref="User"/>.
    /// </summary>
    /// <param name="builder">El constructor de la entidad <see cref="User"/>.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);
        builder.HasKey(u => u.Id);

        // Configuración del nombre completo del usuario desglosado
        builder.OwnsOne(u => u.Name, fullName =>
        {
            fullName.Property(f => f.FirstName.Value)
                    .HasColumnName(nameof(FirstName))
                    .IsRequired()
                    .HasMaxLength(FirstName.MaxLength);

            fullName.Property(f => f.LastName.Value)
                    .HasColumnName(nameof(LastName))
                    .IsRequired()
                    .HasMaxLength(LastName.MaxLength);
        });

        //Configuración de las credenciales del usuario desglosado
        builder.OwnsOne(u => u.Credentials, credentials =>
        {
            credentials.Property(c => c.Email)
                       .IsRequired()
                       .HasColumnName(nameof(Email))
                       .HasMaxLength(Email.MaxLength);

            credentials.Property(c => c.Password)
                       .IsRequired()
                       .HasColumnName(nameof(Password))
                       .HasMaxLength(Password.MaxLength);
        });

        builder.HasIndex(u => u.Credentials.Email)
               .IsUnique();

        // Ignorar la colección Reservations en la entidad User
        builder.Ignore(u => u.Reservations);
    }
}