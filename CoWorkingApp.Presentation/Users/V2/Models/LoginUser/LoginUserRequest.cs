using CoWorkingApp.Application.Users.Commands.AuthenticateUser;

namespace CoWorkingApp.Presentation.Users.V2.Models.LoginUser;

/// <summary>
/// Solicitud de inicio de sesión del usuario.
/// </summary>
/// <param name="Email">Correo electrónico del usuario.</param>
/// <param name="Password">Contraseña del usuario.</param>
public readonly record struct LoginUserRequest(
    string Email,
    string Password)
{
    /// <summary>
    /// Convierte una instancia de <see cref="LoginUserRequest"/> a <see cref="AuthenticateUserCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud de inicio de sesión.</param>
    public static explicit operator AuthenticateUserCommand(LoginUserRequest request) =>
        new(
            request.Email,
            request.Password);
}
