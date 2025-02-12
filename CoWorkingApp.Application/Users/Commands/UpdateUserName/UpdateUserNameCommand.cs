using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserName;

/// <summary>
/// Representa un comando para actualizar el nombre de un usuario.
/// </summary>
/// <param name="UserId">El ID del usuario.</param>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
public record struct UpdateUserNameCommand(
    Guid UserId,
    string FirstName,
    string LastName) : ICommand, IInputFilter
{
    /// <summary>
    /// Filtra y normaliza los campos del nombre del usuario.
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
    }
}
