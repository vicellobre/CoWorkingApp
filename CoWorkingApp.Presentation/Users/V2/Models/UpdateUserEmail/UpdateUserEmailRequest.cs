namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUserEmail;

/// <summary>
/// Representa una solicitud para actualizar el correo electrónico de un usuario.
/// </summary>
/// <param name="Email">El nuevo correo electrónico del usuario.</param>
public record class UpdateUserEmailRequest(string Email);
