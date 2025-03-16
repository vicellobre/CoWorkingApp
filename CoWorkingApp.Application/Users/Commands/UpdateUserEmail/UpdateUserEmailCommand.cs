using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserEmail;

/// <summary>
/// Representa un comando para actualizar el correo electrónico de un usuario.
/// </summary>
public record class UpdateUserEmailCommand : ICommand, IInputFilter
{
    /// <summary>
    /// El ID del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// El nuevo correo electrónico del usuario.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserEmailCommand"/>.
    /// </summary>
    /// <param name="userId">El ID del usuario.</param>
    /// <param name="email">El nuevo correo electrónico del usuario.</param>
    public UpdateUserEmailCommand(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }

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
