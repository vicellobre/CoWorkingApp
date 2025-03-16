using CoWorkingApp.Application.Users.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Queries.GetUsers;

/// <summary>
/// Respuesta a la consulta para obtener usuarios.
/// </summary>
/// <param name="Users">La lista de usuarios.</param>
public record class GetUsersQueryResponse(IEnumerable<UserResponse> Users) : IResponse;