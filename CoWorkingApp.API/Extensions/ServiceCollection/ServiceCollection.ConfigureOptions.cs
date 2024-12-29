using CoWorkingApp.API.Configurations.Swagger;
using CoWorkingApp.Infrastructure.Constants;
using CoWorkingApp.Infrastructure.Options.Jwt;
using CoWorkingApp.Infrastructure.Options.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la colección de servicios.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Agrega servicios de configuración de opciones validadas a la colección de servicios.
    /// </summary>
    /// <param name="services">La colección de servicios.</param>
    /// <returns>La colección de servicios con las opciones validadas agregadas.</returns>
    public static IServiceCollection AddConfigureOptionsService(this IServiceCollection services)
    {
        return services
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
            .AddValidatedOptionsOnStart<SwaggerOptions>(SectionNames.Swagger)
            .AddValidatedConfigureOptionsOnStart<ConfigureJwtOptions, JwtOptions>();
    }

    /// <summary>
    /// Método para agregar opciones con validación de anotaciones de datos,
    /// asegurando que las opciones se validan al inicio de la aplicación.
    /// </summary>
    /// <typeparam name="TOptions">El tipo de las opciones.</typeparam>
    /// <param name="services">La colección de servicios.</param>
    /// <param name="sectionName">El nombre de la sección de configuración.</param>
    /// <returns>El constructor de opciones con validaciones agregadas.</returns>
    public static IServiceCollection AddValidatedOptionsOnStart<TOptions>(this IServiceCollection services, string sectionName) where TOptions : class
    {
        services.AddOptions<TOptions>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    /// <summary>
    /// Método para configurar opciones con validación de anotaciones de datos,
    /// asegurando que las opciones se validan al inicio de la aplicación.
    /// </summary>
    /// <typeparam name="TConfigureOptions">El tipo de la clase de configuración.</typeparam>
    /// <typeparam name="TOptions">El tipo de las opciones.</typeparam>
    /// <param name="services">La colección de servicios.</param>
    /// <returns>El constructor de opciones con validaciones agregadas.</returns>
    public static IServiceCollection AddValidatedConfigureOptionsOnStart<TConfigureOptions, TOptions>(this IServiceCollection services)
        where TConfigureOptions : class, IConfigureOptions<TOptions>
        where TOptions : class
    {
        services.ConfigureOptions<TConfigureOptions>();
        services.AddOptions<TOptions>()
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
