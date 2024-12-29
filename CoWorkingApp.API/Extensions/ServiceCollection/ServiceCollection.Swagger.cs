using CoWorkingApp.API.Configurations.Swagger;
using System.Reflection;

namespace CoWorkingApp.API.Extensions.ServiceCollection;

/// <summary>
/// Contiene métodos de extensión para la configuración de servicios de la colección.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Método de extensión para configurar Swagger.
    /// </summary>
    /// <param name="services">La colección de servicios.</param>
    /// <returns>La colección de servicios con la configuración de Swagger agregada.</returns>
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
