using CoWorkingApp.Application.Users.Commands.AuthenticateUser;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Presentation.Users.V2.Models.LoginUser;

/// <summary>
/// Respuesta de la solicitud de inicio de sesión del usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="Token">El token JWT generado para el usuario.</param>
public readonly record struct LoginUserResponse(
    Guid UserId,
    JsonResult Token)
{
    /// <summary>
    /// Convierte una instancia de <see cref="AuthenticateUserCommandResponse"/> a <see cref="LoginUserResponse"/>.
    /// </summary>
    /// <param name="commandResponse">La respuesta del comando de autenticación.</param>
    public static explicit operator LoginUserResponse(AuthenticateUserCommandResponse commandResponse) =>
        new(
            commandResponse.UserId,
            commandResponse.Token);
}
