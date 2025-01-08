using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Application.Users.DTO;

/// <summary>
/// DTO que representa un usuario.
/// </summary>
/// <param name="UserId">El ID del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public readonly record struct UserDto(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email)
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="User"/> a <see cref="UserDto"/>.
    /// </summary>
    /// <param name="user">El usuario a convertir.</param>
    public static explicit operator UserDto(User user) =>
        new(user.Id, user.Name.FirstName, user.Name.LastName, user.Credentials.Email);
}