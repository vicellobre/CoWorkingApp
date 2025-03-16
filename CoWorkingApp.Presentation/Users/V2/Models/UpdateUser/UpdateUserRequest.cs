namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUser;

/// <summary>
/// Representa una solicitud para actualizar un usuario existente.
/// </summary>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public record class UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
