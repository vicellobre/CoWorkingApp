using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Commands.CreateUser;

/// <summary>
/// Comando para crear un nuevo usuario.
/// </summary>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public readonly record struct CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<CreateUserCommandResponse>;
