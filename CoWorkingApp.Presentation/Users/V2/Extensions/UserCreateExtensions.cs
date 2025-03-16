using CoWorkingApp.Application.Users.Commands.CreateUser;
using CoWorkingApp.Presentation.Users.V2.Models.CreateUser;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la creación de usuarios.
/// </summary>
public static class UserCreateExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="CreateUserRequest"/> a un objeto <see cref="CreateUserCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="CreateUserRequest"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateUserCommand"/> con los datos del usuario.</returns>
    public static CreateUserCommand ToCreateUserCommand(this CreateUserRequest request) =>
        new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

    /// <summary>
    /// Convierte un objeto <see cref="CreateUserCommandResponse"/> a un objeto <see cref="CreateUserResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="CreateUserCommandResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateUserResponse"/> con los datos del usuario.</returns>
    public static CreateUserResponse ToCreateUserResponse(this CreateUserCommandResponse response) =>
        new(
            response.UserId,
            response.FirstName,
            response.LastName,
            response.Email);
}
