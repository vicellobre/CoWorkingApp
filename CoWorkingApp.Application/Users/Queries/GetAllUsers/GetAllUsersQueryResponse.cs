using CoWorkingApp.Application.Users.DTO;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Queries.GetAllUsers;

/// <summary>
/// Respuesta a la consulta para obtener todos los usuarios.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="FirstName">El nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
public readonly record struct GetAllUsersQueryResponse(IEnumerable<UserDto> Users) : IResponse;