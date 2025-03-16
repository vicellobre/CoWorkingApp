namespace CoWorkingApp.Presentation.Users.V2.Models.CreateUser;

/// <summary>
/// Representa una solicitud para crear un nuevo usuario.
/// </summary>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public record class CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
