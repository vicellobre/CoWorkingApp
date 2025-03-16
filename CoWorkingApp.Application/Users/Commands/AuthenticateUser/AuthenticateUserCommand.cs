using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.AuthenticateUser;

/// <summary>
/// Comando para autenticar un usuario.
/// </summary>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public record class AuthenticateUserCommand
    : ICommand<AuthenticateUserCommandResponse>, IInputFilter
{
    /// <summary>
    /// El correo electrónico del usuario.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// La contraseña del usuario.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="AuthenticateUserCommand"/>.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    public AuthenticateUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    /// <summary>
    /// Filtra y normaliza los campos del usuario.
    /// </summary>
    public void Filter()
    {
        Email = Email
            .GetValueOrDefault(string.Empty)
            .Trim()
            .ToLowerInvariant();
    }
}