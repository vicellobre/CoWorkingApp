using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Infrastructure.Options.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
    private readonly JwtOptions _jwtSettings;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="AuthService"/>.
    /// </summary>
    /// <param name="jwtSettings">Instancia de <see cref="IOptions{JwtOptions}"/> para acceder a la configuración de JWT.</param>
    /// <exception cref="ArgumentNullException">Se lanza si <paramref name="jwtSettings"/> es <see langword="null"/>.</exception>
    public AuthService(IOptions<JwtOptions> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
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
        // Datos a incluir en el token
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, firstName.Value),
            new Claim(ClaimTypes.Name, lastName.Value),
            new Claim(ClaimTypes.Email, email.Value),
        };

        // Generar la clave secreta para firmar el token
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var symmetricSecurityKey = new SymmetricSecurityKey(key);
        var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // Calcular el tiempo de validez del token
        DateTime now = DateTime.Now;
        double minutes = Convert.ToDouble(_jwtSettings.TokenExpirationInMinutes);
        DateTime expiredDateTime = now.AddMinutes(minutes);

        // Generar el token JWT
        var token = new JwtSecurityToken(_jwtSettings.Issuer,
                                         _jwtSettings.Audience,
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
