using CoWorkingApp.Infrastructure.Middlewares;
using Serilog;

namespace CoWorkingApp.API.Extensions.ApplicationBuilder;

/// <summary>
/// Contiene métodos de extensión para configurar el enrutamiento de la aplicación.
/// </summary>
public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Agrega el middleware para el logging del contexto de la solicitud, incluyendo el CorrelationId.
    /// </summary>
    /// <param name="app">La instancia de <see cref="IApplicationBuilder"/>.</param>
    /// <returns>La instancia de <see cref="IApplicationBuilder"/> con el middleware agregado.</returns>
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }

    /// <summary>
    /// Agrega el middleware de Serilog para el logging de solicitudes.
    /// </summary>
    /// <param name="app">La instancia de <see cref="IApplicationBuilder"/>.</param>
    /// <returns>La instancia de <see cref="IApplicationBuilder"/> con el middleware de Serilog agregado.</returns>
    public static IApplicationBuilder UseRequestLog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }
}
