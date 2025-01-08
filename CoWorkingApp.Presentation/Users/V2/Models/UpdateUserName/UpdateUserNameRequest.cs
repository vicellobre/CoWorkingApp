using CoWorkingApp.Application.Users.Commands.UpdateUserName;

namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUserName;

/// <summary>
/// Representa una solicitud para actualizar el nombre de un usuario.
/// </summary>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
public readonly record struct UpdateUserNameRequest(
    string FirstName,
    string LastName)
{
    /// <summary>
    /// Convierte la solicitud de actualización de nombre de usuario a un comando de actualización de nombre de usuario.
    /// </summary>
    /// <param name="userId">El ID del usuario.</param>
    /// <returns>Un nuevo <see cref="UpdateUserNameCommand"/> con los datos de la solicitud y el ID del usuario.</returns>
    public UpdateUserNameCommand ToUpdateUserNameCommand(Guid userId) =>
        new()
        {
            UserId = userId,
            FirstName = FirstName,
            LastName = LastName
        };
}
