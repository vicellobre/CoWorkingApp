namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUserPassword;

/// <summary>
/// Representa una solicitud para actualizar la contraseña de un usuario.
/// </summary>
/// <param name="Password">La nueva contraseña del usuario.</param>
public record class UpdateUserPasswordRequest(string Password);
