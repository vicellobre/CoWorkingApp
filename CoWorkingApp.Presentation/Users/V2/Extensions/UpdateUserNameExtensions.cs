using CoWorkingApp.Application.Users.Commands.UpdateUserName;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserName;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la actualización del nombre de usuarios.
/// </summary>
public static class UpdateUserNameExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="UpdateUserNameRequest"/> a un objeto <see cref="UpdateUserNameCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="UpdateUserNameRequest"/> que se va a convertir.</param>
    /// <param name="userId">El identificador del usuario.</param>
    /// <returns>Una instancia de <see cref="UpdateUserNameCommand"/> con los datos de la actualización del nombre.</returns>
    public static UpdateUserNameCommand ToUpdateUserNameCommand(this UpdateUserNameRequest request, Guid userId) =>
        new(
            userId,
            request.FirstName,
            request.LastName);
}
