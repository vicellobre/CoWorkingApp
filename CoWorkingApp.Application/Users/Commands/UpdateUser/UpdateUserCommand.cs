using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Commands.UpdateUser;

/// <summary>
/// Comando para actualizar un usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public readonly record struct UpdateUserCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : ICommand<UpdateUserCommandResponse>;
