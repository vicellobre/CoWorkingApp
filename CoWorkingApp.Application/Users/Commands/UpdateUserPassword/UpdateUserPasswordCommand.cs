using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserPassword;

/// <summary>
/// Representa un comando para actualizar la contraseña de un usuario.
/// </summary>
/// <param name="UserId">El ID del usuario.</param>
/// <param name="Password">La nueva contraseña del usuario.</param>
public record struct UpdateUserPasswordCommand(
    Guid UserId,
    string Password) : ICommand, IInputFilter
{
    /// <summary>
    /// Filtra y normaliza la contraseña del usuario.
    /// </summary>
    public void Filter()
    {
        Password = Password.GetValueOrDefault(string.Empty);
    }
}
