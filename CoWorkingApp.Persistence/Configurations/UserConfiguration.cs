using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Persistence.Constants;
using CoWorkingApp.Persistence.ValueConverters;
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
        // Ignorar la colección Reservations en la entidad User
        //builder.Ignore(u => u.Reservations);

        builder.ToTable(TableNames.Users);
        builder.HasKey(u => u.Id);

        //Configuración de las credenciales del usuario desglosado
        builder.OwnsOne(u => u.Credentials, credentials =>
        {
            credentials.Property(c => c.Email)
                       .HasConversion(new EmailConverter())
                       .HasColumnName(nameof(Email))
                       .IsRequired()
                       .HasMaxLength(Email.MaxLength);

            credentials.Property(c => c.Password)
                       .HasConversion(new PasswordConverter())
                       .HasColumnName(nameof(Password))
                       .IsRequired()
                       .HasMaxLength(Password.MaxLength);
        });

        // Configuración del nombre completo del usuario desglosado
        builder.OwnsOne(u => u.Name, fullName =>
        {
            fullName.Property(f => f.FirstName)
                    .HasConversion(new FirstNameConverter())
                    .HasColumnName(nameof(FirstName))
                    .IsRequired()
                    .HasMaxLength(FirstName.MaxLength);

            fullName.Property(f => f.LastName)
                    .HasConversion(new LastNameConverter())
                    .HasColumnName(nameof(LastName))
                    .IsRequired()
                    .HasMaxLength(LastName.MaxLength);
        });

        //builder.HasIndex(u => u.Credentials.Email.Value)
        //       .IsUnique();

        // Crear índice en propiedad anidada
        builder.OwnsOne(u => u.Credentials)
               .HasIndex(c => c.Email)
               .IsUnique();

        builder.Navigation(u => u.Reservations)
               .AutoInclude(false); // Evitar la carga automática de la colección
    }
}