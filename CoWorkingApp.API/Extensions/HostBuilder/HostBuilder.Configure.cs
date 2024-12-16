using System.Diagnostics.CodeAnalysis;

namespace CoWorkingApp.API.Extensions.HostBuilder;

/// <summary>
/// Clase de extensión para configurar el host de la aplicación.
/// </summary>
[ExcludeFromCodeCoverage]
public static partial class HostBuilderExtensions
{
    /// <summary>
    /// Método de extensión para crear y configurar un <see cref="IHostBuilder"/> para la aplicación.
    /// </summary>
    /// <param name="hostBuilder">El <see cref="IHostBuilder"/>.</param>
    /// <returns>Un <see cref="IHostBuilder"/> configurado para usar <see cref="Startup"/>.</returns>
    public static IHostBuilder ConfigureHost(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
    }
}
