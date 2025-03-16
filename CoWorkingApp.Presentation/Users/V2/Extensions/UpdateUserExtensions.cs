using CoWorkingApp.Application.Users.Commands.UpdateUser;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUser;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la actualización de usuarios.
/// </summary>
public static class UpdateUserExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="UpdateUserRequest"/> a un objeto <see cref="UpdateUserCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="UpdateUserRequest"/> que se va a convertir.</param>
    /// <param name="userId">El identificador del usuario.</param>
    /// <returns>Una instancia de <see cref="UpdateUserCommand"/> con los datos de la actualización del usuario.</returns>
    public static UpdateUserCommand ToUpdateUserCommand(this UpdateUserRequest request, Guid userId) =>
        new(
            userId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
}
