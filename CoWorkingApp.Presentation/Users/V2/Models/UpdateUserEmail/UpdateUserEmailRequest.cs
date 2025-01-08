using CoWorkingApp.Application.Users.Commands.UpdateUserEmail;

namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUserEmail;

/// <summary>
/// Representa una solicitud para actualizar el correo electrónico de un usuario.
/// </summary>
/// <param name="Email">El nuevo correo electrónico del usuario.</param>
public readonly record struct UpdateUserEmailRequest(string Email)
{
    /// <summary>
    /// Convierte la solicitud de actualización de correo electrónico a un comando de actualización de correo electrónico.
    /// </summary>
    /// <param name="userId">El ID del usuario.</param>
    /// <returns>Un nuevo <see cref="UpdateUserEmailCommand"/> con los datos de la solicitud y el ID del usuario.</returns>
    public UpdateUserEmailCommand ToUpdateUserEmailCommand(Guid userId) =>
        new()
        {
            UserId = userId,
            Email = Email
        };
}
