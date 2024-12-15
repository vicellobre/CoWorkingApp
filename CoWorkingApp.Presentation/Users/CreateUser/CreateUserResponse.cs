using CoWorkingApp.Application.Users.Commands.CreateUser;

namespace CoWorkingApp.Presentation.Users.CreateUser;

/// <summary>
/// Representa la respuesta para la creación de un usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public readonly record struct CreateUserResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email)
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="CreateUserCommandResponse"/> a <see cref="CreateUserResponse"/>.
    /// </summary>
    /// <param name="response">La respuesta del comando de creación de usuario.</param>
    /// <returns>Un nuevo <see cref="CreateUserResponse"/> con los datos de la respuesta.</returns>
    public static explicit operator CreateUserResponse(CreateUserCommandResponse response) =>
        new(
            response.UserId,
            response.FirstName,
            response.LastName,
            response.Email);
}
