using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CoWorkingApp.Infrastructure.Options.Jwt;

/// <summary>
/// Clase que representa las opciones de configuración para JWT.
/// </summary>
[ExcludeFromCodeCoverage]
public class JwtOptions
{
    /// <summary>
    /// Audiencia del token JWT.
    /// </summary>
    public required string Audience { get; init; }

    /// <summary>
    /// Emisor del token JWT.
    /// </summary>
    public required string Issuer { get; init; }

    /// <summary>
    /// Clave secreta utilizada para firmar el token JWT.
    /// </summary>
    [Required]
    public required string SecretKey { get; init; }

    /// <summary>
    /// Tiempo de expiración del token JWT en minutos.
    /// </summary>
    [Required]
    [Range(10, 60)]
    public required string TokenExpirationInMinutes { get; init; }
}
