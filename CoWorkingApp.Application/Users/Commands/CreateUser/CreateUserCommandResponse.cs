using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Commands.CreateUser;

/// <summary>
/// Respuesta al comando de creación de usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public record class CreateUserCommandResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email) : IResponse;
