using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Queries.GetUserById;

/// <summary>
/// Respuesta a la consulta para obtener un usuario por su identificador.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public readonly record struct GetUserByIdQueryResponse(Guid UserId,
    string FirstName,
    string LastName,
    string Email
    ) : IResponse
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="User"/> a <see cref="GetUserByIdQueryResponse"/>.
    /// </summary>
    /// <param name="user">El usuario a convertir.</param>
    public static explicit operator GetUserByIdQueryResponse(User user) =>
        new(user.Id, user.Name.FirstName, user.Name.LastName, user.Credentials.Email);
}
