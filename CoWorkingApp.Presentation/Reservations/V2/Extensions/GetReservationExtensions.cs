using CoWorkingApp.Application.Reservations.Queries.GetReservationById;
using CoWorkingApp.Presentation.Reservations.V2.Models.GetReservation;

namespace CoWorkingApp.Presentation.Reservations.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la obtención de reservas.
/// </summary>
public static class GetReservationExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="GetReservationByIdQueryResponse"/> a un objeto <see cref="GetReservationResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetReservationByIdQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetReservationResponse"/> con los datos de la reserva.</returns>
    public static GetReservationResponse ToGetReservationResponse(this GetReservationByIdQueryResponse response) =>
        new(
            response.ReservationId,
            response.Date,
            response.UserFirstName,
            response.UserLastName,
            response.UserEmail,
            response.SeatName,
            response.SeatDescription);
}
