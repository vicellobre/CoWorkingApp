using CoWorkingApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método de extensión para configurar el contexto de la base de datos.
    /// </summary>
    /// <param name="services">La colección de servicios.</param>
    /// <param name="configuration">La configuración de la aplicación.</param>
    /// <returns>La colección de servicios con el contexto de la base de datos configurado.</returns>
    public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionCoWorking");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("The connection string is not configured correctly.");
        }

        services.AddDbContext<CoWorkingContext>(options =>
            options.UseSqlServer(connectionString,
            sqlOptions => sqlOptions.MigrationsAssembly("CoWorkingApp.Persistence")));

        return services;
    }
}
