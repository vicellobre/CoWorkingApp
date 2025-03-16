using CoWorkingApp.Application.Seats.Queries.GetSeatById;
using CoWorkingApp.Application.Seats.Queries.GetSeatByName;
using CoWorkingApp.Presentation.Seats.V2.Models.GetSeat;

namespace CoWorkingApp.Presentation.Seats.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la obtención de asientos.
/// </summary>
public static class GetSeatExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="GetSeatByIdQueryResponse"/> a un objeto <see cref="GetSeatResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetSeatByIdQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetSeatResponse"/> con los datos del asiento.</returns>
    public static GetSeatResponse ToGetSeatResponse(this GetSeatByIdQueryResponse response) =>
        new(
            response.SeatId,
            response.Name,
            response.Description);

    /// <summary>
    /// Convierte un objeto <see cref="GetSeatByNameQueryResponse"/> a un objeto <see cref="GetSeatResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetSeatByNameQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetSeatResponse"/> con los datos del asiento.</returns>
    public static GetSeatResponse ToGetSeatResponse(this GetSeatByNameQueryResponse response) =>
        new(
            response.SeatId,
            response.Name,
            response.Description);
}
