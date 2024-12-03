using CoWorkingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.Persistence.Context;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}
