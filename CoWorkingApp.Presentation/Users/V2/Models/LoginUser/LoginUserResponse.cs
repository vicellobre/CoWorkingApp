using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Presentation.Users.V2.Models.LoginUser;

/// <summary>
/// Respuesta de la solicitud de inicio de sesión del usuario.
/// </summary>
/// <param name="Id">El identificador del usuario.</param>
/// <param name="Token">El token JWT generado para el usuario.</param>
public record class LoginUserResponse(
    Guid Id,
    JsonResult Token);