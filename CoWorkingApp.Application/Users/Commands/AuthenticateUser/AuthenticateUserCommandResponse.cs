using CoWorkingApp.Core.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Application.Users.Commands.AuthenticateUser;

/// <summary>
/// Respuesta al comando de autenticación de usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Token">El token de autenticación del usuario.</param>
public record class AuthenticateUserCommandResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    JsonResult Token) : IResponse;