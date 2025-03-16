namespace CoWorkingApp.Presentation.Users.V2.Models.CreateUser;

/// <summary>
/// Representa la respuesta para la creación de un usuario.
/// </summary>
/// <param name="Id">El identificador del usuario.</param>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public record class CreateUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);
