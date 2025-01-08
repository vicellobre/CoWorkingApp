using CoWorkingApp.Application.Users.DTO;
using CoWorkingApp.Application.Users.Queries.GetAllUsers;

namespace CoWorkingApp.Presentation.Users.V2.Models.GetallUsers;

/// <summary>
/// Representa la respuesta para la consulta de obtención de todos los usuarios.
/// </summary>
/// <param name="Users">La lista de usuarios.</param>
public readonly record struct GetAllUsersResponse(IEnumerable<UserDto> Users)
{
    /// <summary>
    /// Crea una instancia de <see cref="GetAllUsersResponse"/> a partir de una colección de <see cref="GetAllUsersQueryResponse"/>.
    /// </summary>
    /// <param name="queryResponses">La colección de respuestas de la consulta de obtención de todos los usuarios.</param>
    public static explicit operator GetAllUsersResponse(GetAllUsersQueryResponse queryResponse) =>
        new(queryResponse.Users);
}
