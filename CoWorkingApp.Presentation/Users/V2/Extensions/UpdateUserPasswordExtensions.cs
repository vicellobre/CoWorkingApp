using CoWorkingApp.Application.Users.Commands.UpdateUserPassword;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserPassword;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la actualización de la contraseña de usuarios.
/// </summary>
public static class UpdateUserPasswordExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="UpdateUserPasswordRequest"/> a un objeto <see cref="UpdateUserPasswordCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="UpdateUserPasswordRequest"/> que se va a convertir.</param>
    /// <param name="userId">El identificador del usuario.</param>
    /// <returns>Una instancia de <see cref="UpdateUserPasswordCommand"/> con los datos de la actualización de la contraseña.</returns>
    public static UpdateUserPasswordCommand ToUpdateUserPasswordCommand(this UpdateUserPasswordRequest request, Guid userId) =>
        new(
            userId,
            request.Password);
}
