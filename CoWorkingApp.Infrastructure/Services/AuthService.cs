using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoWorkingApp.Infrastructure.Services;

/// <summary>
/// Servicio de autenticación para generar tokens JWT.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="AuthService"/>.
    /// </summary>
    /// <param name="configuration">Instancia de <see cref="IConfiguration"/> para acceder a la configuración de la aplicación.</param>
    /// <exception cref="ArgumentNullException">Se lanza si <paramref name="configuration"/> es <see langword="null"/>.</exception>
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Genera un token JWT para un usuario autenticado.
    /// </summary>
    /// <param name="firstName">El primer nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <returns>Un objeto <see cref="JsonResult"/> que contiene el token generado.</returns>
    /// <exception cref="ArgumentNullException">
    /// Se lanza si el emisor, la audiencia o la clave secreta son <see langword="null"/> o están vacíos.
    /// </exception>
    public JsonResult BuildToken(FirstName firstName, LastName lastName, Email email)
    {
        // Obtener el origen del emisor y la audiencia desde la configuración
        string issuer = _configuration["Auth:Jwt:Issuer"] ?? throw new ArgumentNullException(nameof(issuer), "Issuer cannot be null.");
        string audience = _configuration["Auth:Jwt:Audience"] ?? throw new ArgumentNullException(nameof(audience), "Audience cannot be null.");
        string secretKey = _configuration["Auth:Jwt:SecretKey"] ?? throw new ArgumentNullException(nameof(secretKey), "SecretKey cannot be null.");

        // Datos a incluir en el token
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, firstName.Value),
            new Claim(ClaimTypes.Name, lastName.Value),
            new Claim(ClaimTypes.Email, email.Value),
        };

        // Generar la clave secreta para firmar el token
        var key = Encoding.UTF8.GetBytes(secretKey);
        var symmetricSecurityKey = new SymmetricSecurityKey(key);
        var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // Calcular el tiempo de validez del token
        DateTime now = DateTime.Now;
        double minutes = Convert.ToDouble(_configuration["Auth:Jwt:TokenExpirationInMinutes"]);
        DateTime expiredDateTime = now.AddMinutes(minutes);

        // Generar el token JWT
        var token = new JwtSecurityToken(issuer,
                                         audience,
                                         claims,
                                         expires: expiredDateTime,
                                         signingCredentials: creds);

        // Escribir el token como una cadena
        var tokenSecurity = new JwtSecurityTokenHandler();
        var tokenString = tokenSecurity.WriteToken(token);

        // Retornar el token en un JsonResult
        return new JsonResult(new { Token = tokenString });
    }
}
