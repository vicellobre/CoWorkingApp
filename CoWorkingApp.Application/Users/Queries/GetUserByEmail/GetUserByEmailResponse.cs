using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// Respuesta a la consulta para obtener un usuario por su correo electrónico.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public readonly record struct GetUserByEmailResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email) : IResponse
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="User"/> a <see cref="GetUserByEmailResponse"/>.
    /// </summary>
    /// <param name="user">El usuario a convertir.</param>
    public static explicit operator GetUserByEmailResponse(User user) =>
        new(user.Id, user.Name.FirstName, user.Name.LastName, user.Credentials.Email);
}
