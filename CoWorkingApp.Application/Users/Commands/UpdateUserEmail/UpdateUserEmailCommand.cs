using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserEmail;

/// <summary>
/// Representa un comando para actualizar el correo electrónico de un usuario.
/// </summary>
/// <param name="UserId">El ID del usuario.</param>
/// <param name="Email">El nuevo correo electrónico del usuario.</param>
public record struct UpdateUserEmailCommand(
    Guid UserId,
    string Email) : ICommand, IInputFilter
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
