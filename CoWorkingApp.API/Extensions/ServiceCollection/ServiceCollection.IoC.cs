using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Infrastructure.Services;
using CoWorkingApp.Persistence.Repositories;
using CoWorkingApp.Persistence.UnitOfWorks;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método de extensión para agregar las dependencias necesarias a la colección de servicios.
    /// </summary>
    /// <param name="services">Colección de servicios de la aplicación.</param>
    /// <returns>Colección de servicios con las nuevas dependencias agregadas.</returns>
    public static IServiceCollection AddDependencyService(this IServiceCollection services)
    {
        // Inyectar UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Inyectar los servicios específicos de User, Seat y Reservation
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        // Inyectar los servicios de caché
        services.Decorate<IUserRepository, UserCachedRepository>();

        // Inyectar el servicio de autenticación
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
