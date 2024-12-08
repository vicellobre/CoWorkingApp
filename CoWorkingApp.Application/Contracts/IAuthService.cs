using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Application.Contracts;

/// <summary>
/// Interfaz para el servicio de autenticación.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Genera un token JWT para un usuario autenticado.
    /// </summary>
    /// <param name="firstName">El primer nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <returns>Un objeto <see cref="JsonResult"/> que contiene el token generado.</returns>
    JsonResult BuildToken(FirstName firstName, LastName lastName, Email email);
}
