namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUserName;

/// <summary>
/// Representa una solicitud para actualizar el nombre de un usuario.
/// </summary>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
public record class UpdateUserNameRequest(
    string FirstName,
    string LastName);
