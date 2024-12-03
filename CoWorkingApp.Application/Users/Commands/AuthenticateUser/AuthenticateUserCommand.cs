using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Commands.AuthenticateUser;

/// <summary>
/// Comando para autenticar un usuario.
/// </summary>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public readonly record struct AuthenticateUserCommand(
    string Email,
    string Password) : ICommand<AuthenticateUserCommandResponse>;
