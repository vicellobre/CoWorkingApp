using CoWorkingApp.Application.Users.Queries.GetUsers;
using CoWorkingApp.Presentation.Users.V2.Models.GetUsers;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con las consultas de usuarios.
/// </summary>
public static class GetUsersExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="GetUsersQueryResponse"/> a un objeto <see cref="GetUsersResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetUsersQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetUsersResponse"/> que contiene los usuarios.</returns>
    public static GetUsersResponse ToGetUsersResponse(this GetUsersQueryResponse response) =>
        new(response.Users);
}
