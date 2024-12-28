using CoWorkingApp.Infrastructure.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoWorkingApp.Infrastructure.Options.Jwt;

/// <summary>
/// Clase para configurar las opciones de JWT mediante la interfaz IConfigureOptions.
/// </summary>
public class ConfigureJwtOptions : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ConfigureJwtOptions"/>.
    /// </summary>
    /// <param name="configuration">La configuración de la aplicación.</param>
    /// <exception cref="AggregateException">Se lanza si la configuración es nula.</exception>
    public ConfigureJwtOptions(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new AggregateException(nameof(configuration));
    }

    /// <summary>
    /// Configura las opciones de JWT.
    /// </summary>
    /// <param name="options">Las opciones de JWT a configurar.</param>
    /// <exception cref="NotImplementedException">Se lanza si la sección de configuración no está implementada.</exception>
    public void Configure(JwtOptions options)
    {
        _configuration
            .GetSection(SectionNames.JsonWebToken)
            .Bind(options);
    }
}
