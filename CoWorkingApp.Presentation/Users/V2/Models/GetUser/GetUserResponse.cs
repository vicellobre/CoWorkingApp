namespace CoWorkingApp.Presentation.Users.V2.Models.GetUser;

/// <summary>
/// Representa la respuesta para las consultas de obtención de usuario.
/// </summary>
/// <param name="Id">El ID del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public record class GetUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);
