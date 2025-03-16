using CoWorkingApp.Application.Users.Commands.AuthenticateUser;
using CoWorkingApp.Presentation.Users.V2.Models.LoginUser;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la autenticación de usuarios.
/// </summary>
public static class LoginUserExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="LoginUserRequest"/> a un objeto <see cref="AuthenticateUserCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="LoginUserRequest"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="AuthenticateUserCommand"/> con los datos de autenticación del usuario.</returns>
    public static AuthenticateUserCommand ToAuthenticateUserCommand(this LoginUserRequest request) =>
        new(
            request.Email,
            request.Password);

    /// <summary>
    /// Convierte un objeto <see cref="AuthenticateUserCommandResponse"/> a un objeto <see cref="LoginUserResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="AuthenticateUserCommandResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="LoginUserResponse"/> con los datos de la respuesta de autenticación.</returns>
    public static LoginUserResponse ToLoginUserResponse(this AuthenticateUserCommandResponse response) =>
        new(
            response.UserId,
            response.Token);
}
