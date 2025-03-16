using CoWorkingApp.Application.Users.Queries.GetUserByEmail;
using CoWorkingApp.Application.Users.Queries.GetUserById;
using CoWorkingApp.Presentation.Users.V2.Models.GetUser;

namespace CoWorkingApp.Presentation.Users.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con las consultas de usuarios.
/// </summary>
public static class GetUserExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="GetUserByIdQueryResponse"/> a un objeto <see cref="GetUserResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetUserByIdQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetUserResponse"/> que contiene los datos del usuario.</returns>
    public static GetUserResponse ToGetUserResponse(this GetUserByIdQueryResponse response) =>
        new(
            response.UserId,
            response.FirstName,
            response.LastName,
            response.Email);

    /// <summary>
    /// Convierte un objeto <see cref="GetUserByEmailQueryResponse"/> a un objeto <see cref="GetUserResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetUserByEmailQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetUserResponse"/> que contiene los datos del usuario.</returns>
    public static GetUserResponse ToGetUserResponse(this GetUserByEmailQueryResponse response) =>
        new(
            response.UserId,
            response.FirstName,
            response.LastName,
            response.Email);
}
