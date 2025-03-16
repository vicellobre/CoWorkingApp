using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserPassword;

/// <summary>
/// Representa un comando para actualizar la contraseña de un usuario.
/// </summary>
public record class UpdateUserPasswordCommand : ICommand, IInputFilter
{
    /// <summary>
    /// El ID del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// La nueva contraseña del usuario.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserPasswordCommand"/>.
    /// </summary>
    /// <param name="userId">El ID del usuario.</param>
    /// <param name="password">La nueva contraseña del usuario.</param>
    public UpdateUserPasswordCommand(Guid userId, string password)
    {
        UserId = userId;
        Password = password;
    }

    /// <summary>
    /// Filtra y normaliza la contraseña del usuario.
    /// </summary>
    public void Filter()
    {
        Password = Password.GetValueOrDefault(string.Empty);
    }
}
