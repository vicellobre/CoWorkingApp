using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Infrastructure.Services;
using CoWorkingApp.Persistence.Context;
using CoWorkingApp.Persistence.Repositories;
using CoWorkingApp.Persistence.UnitOfWorks;
using System.Diagnostics.CodeAnalysis;

namespace CoWorkingApp.API.Extensions;

/// <summary>
/// Clase estática para configurar la inyección de dependencias en la aplicación.
/// </summary>
[ExcludeFromCodeCoverage]
public static class IoC
{
    /// <summary>
    /// Método de extensión para agregar las dependencias necesarias a la colección de servicios.
    /// </summary>
    /// <param name="services">Colección de servicios de la aplicación.</param>
    /// <returns>Colección de servicios con las nuevas dependencias agregadas.</returns>
    public static IServiceCollection AddDependency(this IServiceCollection services)
    {
        // Inyectar UnitOfWork
        services.AddScoped<IUnitOfWork>(p => new UnitOfWork(p.GetRequiredService<CoWorkingContext>()));

        // Inyectar los servicios específicos de User, Seat y Reservation
        services.AddScoped<IUserRepository>(p => new UserRepository(p.GetRequiredService<CoWorkingContext>()));
        services.AddScoped<ISeatRepository>(p => new SeatRepository(p.GetRequiredService<CoWorkingContext>()));
        services.AddScoped<IReservationRepository>(p => new ReservationRepository(p.GetRequiredService<CoWorkingContext>()));

        // Inyectar el servicio de autenticación
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
