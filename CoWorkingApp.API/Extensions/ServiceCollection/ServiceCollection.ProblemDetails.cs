using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método para agregar la configuración de ProblemDetails a la colección de servicios.
    /// </summary>
    /// <param name="services">Colección de servicios de la aplicación.</param>
    /// <returns>Colección de servicios con la configuración de ProblemDetails agregada.</returns>
    public static IServiceCollection AddProblemDetailsService(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });

        return services;
    }
}
