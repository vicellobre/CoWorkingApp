using CoWorkingApp.Application.Users.DTOs;

namespace CoWorkingApp.Presentation.Users.V2.Models.GetUsers;

/// <summary>
/// Representa la respuesta para la consulta de obtención de usuarios.
/// </summary>
/// <param name="Users">La lista de usuarios.</param>
public record class GetUsersResponse(IEnumerable<UserResponse> Users);
