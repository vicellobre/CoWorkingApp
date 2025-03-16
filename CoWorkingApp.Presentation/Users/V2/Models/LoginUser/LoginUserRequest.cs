namespace CoWorkingApp.Presentation.Users.V2.Models.LoginUser;

/// <summary>
/// Solicitud de inicio de sesión del usuario.
/// </summary>
/// <param name="Email">Correo electrónico del usuario.</param>
/// <param name="Password">Contraseña del usuario.</param>
public record class LoginUserRequest(
    string Email,
    string Password);