using CoWorkingApp.Application.Seats.Queries.GetSeats;
using CoWorkingApp.Presentation.Seats.V2.Models.GetSeats;

namespace CoWorkingApp.Presentation.Seats.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la obtención de asientos.
/// </summary>
public static class GetSeatsExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="GetSeatsQueryResponse"/> a un objeto <see cref="GetSeatsResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetSeatsQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetSeatsResponse"/> con los datos de los asientos.</returns>
    public static GetSeatsResponse ToGetSeatsResponse(this GetSeatsQueryResponse response) =>
        new(response.Seats);
}
