using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.CreateUser;

/// <summary>
/// Comando para crear un nuevo usuario.
/// </summary>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public record struct CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<CreateUserCommandResponse>, IInputFilter
{
    /// <summary>
    /// Filtra y normaliza los campos del usuario.
    /// </summary>
    public void Filter()
    {
        FirstName = FirstName
            .GetValueOrDefault(string.Empty)
            .Trim()
            .CapitalizeWords();

        LastName = LastName
            .GetValueOrDefault(string.Empty)
            .Trim()
            .CapitalizeWords();

        Email = Email
            .GetValueOrDefault(string.Empty)
            .Trim()
            .ToLowerInvariant();
    }
}
