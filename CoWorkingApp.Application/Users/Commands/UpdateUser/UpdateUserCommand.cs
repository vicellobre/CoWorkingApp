using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUser;

/// <summary>
/// Comando para actualizar un usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public record struct UpdateUserCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Password
) : ICommand<UpdateUserCommandResponse>, IInputFilter
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
