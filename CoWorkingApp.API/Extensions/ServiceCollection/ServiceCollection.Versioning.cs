using Asp.Versioning;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la colección de servicios.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Agrega la configuración de versionado de API a la colección de servicios.
    /// </summary>
    /// <param name="services">La colección de servicios de la aplicación.</param>
    /// <param name="configuration">La configuración de la aplicación.</param>
    /// <returns>La colección de servicios de la aplicación con la configuración de versionado de API.</returns>
    public static IServiceCollection AddApiVersioningService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Api-Version"));
        })
        .AddMvc() // This is needed for controllers
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
