using CoWorkingApp.Application.Users.Queries.GetUserByEmail;
using CoWorkingApp.Application.Users.Queries.GetUserById;

namespace CoWorkingApp.Presentation.Users.V2.Models.GetUser;

/// <summary>
/// Representa la respuesta para las consultas de obtención de usuario.
/// </summary>
/// <param name="Id">El ID del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public readonly record struct GetUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email)
{
    /// <summary>
    /// Crea una instancia de <see cref="GetUserResponse"/> a partir de <see cref="GetUserByIdQueryResponse"/>.
    /// </summary>
    /// <param name="queryResponse">La respuesta de la consulta de obtención de usuario por ID.</param>
    public static explicit operator GetUserResponse(GetUserByIdQueryResponse queryResponse) =>
        new(
            queryResponse.UserId,
            queryResponse.FirstName,
            queryResponse.LastName,
            queryResponse.Email);

    /// <summary>
    /// Crea una instancia de <see cref="GetUserResponse"/> a partir de <see cref="GetUserByEmailQueryResponse"/>.
    /// </summary>
    /// <param name="queryResponse">La respuesta de la consulta de obtención de usuario por correo electrónico.</param>
    public static explicit operator GetUserResponse(GetUserByEmailQueryResponse queryResponse) =>
        new(
            queryResponse.UserId,
            queryResponse.FirstName,
            queryResponse.LastName,
            queryResponse.Email);
}
