using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// Respuesta a la consulta para obtener un usuario por su correo electrónico.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public record class GetUserByEmailQueryResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email) : IResponse;