using CoWorkingApp.Application.Users.Commands.UpdateUserPassword;

namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUserPassword;

/// <summary>
/// Representa una solicitud para actualizar la contraseña de un usuario.
/// </summary>
/// <param name="Password">La nueva contraseña del usuario.</param>
public readonly record struct UpdateUserPasswordRequest(string Password)
{
    /// <summary>
    /// Convierte la solicitud de actualización de contraseña a un comando de actualización de contraseña.
    /// </summary>
    /// <param name="userId">El ID del usuario.</param>
    /// <returns>Un nuevo <see cref="UpdateUserPasswordCommand"/> con los datos de la solicitud y el ID del usuario.</returns>
    public UpdateUserPasswordCommand ToUpdateUserPasswordCommand(Guid userId) =>
        new(
            userId,
            Password);
}
