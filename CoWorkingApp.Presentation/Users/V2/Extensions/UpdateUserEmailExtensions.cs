using CoWorkingApp.Application.Users.Commands.UpdateUserEmail;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserEmail;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la actualización del correo electrónico de usuarios.
/// </summary>
public static class UpdateUserEmailExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="UpdateUserEmailRequest"/> a un objeto <see cref="UpdateUserEmailCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="UpdateUserEmailRequest"/> que se va a convertir.</param>
    /// <param name="userId">El identificador del usuario.</param>
    /// <returns>Una instancia de <see cref="UpdateUserEmailCommand"/> con los datos de la actualización del correo electrónico.</returns>
    public static UpdateUserEmailCommand ToUpdateUserEmailCommand(this UpdateUserEmailRequest request, Guid userId) =>
        new(
            userId,
            request.Email);
}
