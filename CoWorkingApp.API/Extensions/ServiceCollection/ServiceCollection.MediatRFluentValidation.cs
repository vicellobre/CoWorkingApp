using CoWorkingApp.Application.Behaviors;
using FluentValidation;
using MediatR;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método para agregar la configuración de MediatR y FluentValidation a la colección de servicios.
    /// </summary>
    /// <param name="services">Colección de servicios de la aplicación.</param>
    /// <returns>Colección de servicios con la configuración de MediatR y FluentValidation agregada.</returns>
    public static IServiceCollection AddMediatWithValidationService(this IServiceCollection services)
    {
        // Configuración de MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly));

        // MediatR with FluentValidation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(InputFilterBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        // Configuración de FluentValidation
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}
