using CoWorkingApp.API.Configurations;
using Microsoft.OpenApi.Models;
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
    /// <param name="configuration">La configuración de la aplicación.</param>
    /// <returns>La colección de servicios con la configuración de Swagger agregada.</returns>
    public static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            var swaggerConfig = configuration.GetSection("Swagger").Get<SwaggerConfig>();
            if (swaggerConfig == null)
            {
                throw new ArgumentNullException(nameof(swaggerConfig));
            }

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = swaggerConfig.Version,
                Title = swaggerConfig.Title,
                Description = swaggerConfig.Description,
                Contact = new OpenApiContact
                {
                    Name = swaggerConfig.Contact.Name,
                    Email = swaggerConfig.Contact.Email,
                    Url = new Uri(swaggerConfig.Contact.Url!)
                },
                License = new OpenApiLicense
                {
                    Name = swaggerConfig.License.Name,
                    Url = new Uri(swaggerConfig.License.Url!)
                }
            });
        });

        return services;
    }
}
