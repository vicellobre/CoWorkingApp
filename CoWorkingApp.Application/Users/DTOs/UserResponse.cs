namespace CoWorkingApp.Application.Users.DTOs;

/// <summary>
/// Representa la respuesta para un usuario.
/// </summary>
/// <param name="UserId">El ID del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public record class UserResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email);