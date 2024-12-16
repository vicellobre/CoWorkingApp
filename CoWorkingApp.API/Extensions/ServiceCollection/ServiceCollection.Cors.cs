namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método de extensión para configurar CORS.
    /// </summary>
    /// <param name="services">La colección de servicios.</param>
    /// <returns>La colección de servicios con la configuración de CORS agregada.</returns>
    public static IServiceCollection AddCorsService(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowAnyMethod();
            });
        });

        return services;
    }
}
