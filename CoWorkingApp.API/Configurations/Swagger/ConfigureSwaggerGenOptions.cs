using Asp.Versioning.ApiExplorer;
using CoWorkingApp.Infrastructure.Options.Swagger;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoWorkingApp.API.Configurations.Swagger;

/// <summary>
/// Clase para configurar las opciones de SwaggerGen.
/// </summary>
public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly SwaggerOptions _swaggerOptions;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ConfigureSwaggerGenOptions"/>.
    /// </summary>
    /// <param name="provider">Proveedor de descripciones de versiones de API.</param>
    /// <param name="swaggerOptions">Opciones de configuración de Swagger.</param>
    /// <exception cref="ArgumentNullException">Se lanza cuando el parámetro es nulo.</exception>
    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IOptions<SwaggerOptions> swaggerOptions)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider)); ;
        _swaggerOptions = swaggerOptions.Value ?? throw new ArgumentNullException(nameof(swaggerOptions));
    }

    /// <summary>
    /// Configura las opciones de SwaggerGen.
    /// </summary>
    /// <param name="options">Las opciones de SwaggerGen a configurar.</param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var versionOption = _swaggerOptions.Versions.FirstOrDefault(v => v.Version!.Contains(description.ApiVersion.ToString()));
            if (versionOption != null)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, versionOption));
            }
        }
    }

    /// <summary>
    /// Crea la información de OpenAPI para una versión específica de la API.
    /// </summary>
    /// <param name="description">La descripción de la versión de la API.</param>
    /// <param name="versionOption">La configuración de la versión específica.</param>
    /// <returns>La información de OpenAPI para la versión de la API.</returns>
    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, SwaggerOptions.SwaggerVersionOptions versionOption)
    {
        var info = new OpenApiInfo
        {
            Title = versionOption.Title,
            Version = description.ApiVersion.ToString(),
            Description = versionOption.Description,
            Contact = new OpenApiContact
            {
                Name = _swaggerOptions.Contact.Name,
                Email = _swaggerOptions.Contact.Email,
                Url = new Uri(_swaggerOptions.Contact.Url!)
            },
            License = new OpenApiLicense
            {
                Name = _swaggerOptions.License.Name,
                Url = new Uri(_swaggerOptions.License.Url!)
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += " Esta versión de la API ha sido deprecada.";
        }

        return info;
    }
}
