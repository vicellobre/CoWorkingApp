using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.AuthenticateUser;

/// <summary>
/// Comando para autenticar un usuario.
/// </summary>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public record struct AuthenticateUserCommand(
    string Email,
    string Password) : ICommand<AuthenticateUserCommandResponse>, IInputFilter
{
    /// <summary>
    /// Filtra y normaliza el correo electrónico del usuario.
    /// </summary>
    public void Filter()
    {
        Email = Email
            .GetValueOrDefault(string.Empty)
            .Trim()
            .ToLowerInvariant();
    }
}
