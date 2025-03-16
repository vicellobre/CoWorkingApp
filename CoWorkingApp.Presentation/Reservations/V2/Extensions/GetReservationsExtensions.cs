using CoWorkingApp.Application.Reservations.Queries.GetReservations;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatId;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserId;
using CoWorkingApp.Presentation.Reservations.V2.Models.GetReservations;

namespace CoWorkingApp.Presentation.Reservations.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la obtención de reservas.
/// </summary>
public static class GetReservationsExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="GetReservationsQueryResponse"/> a un objeto <see cref="GetReservationsResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetReservationsQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetReservationsResponse"/> con los datos de las reservas.</returns>
    public static GetReservationsResponse ToGetReservationsResponse(this GetReservationsQueryResponse response) =>
        new(response.Reservations);

    /// <summary>
    /// Convierte un objeto <see cref="GetReservationsByDateQueryResponse"/> a un objeto <see cref="GetReservationsResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetReservationsByDateQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetReservationsResponse"/> con los datos de las reservas.</returns>
    public static GetReservationsResponse ToGetReservationsResponse(this GetReservationsByDateQueryResponse response) =>
        new(response.Reservations);

    /// <summary>
    /// Convierte un objeto <see cref="GetReservationsByUserIdQueryResponse"/> a un objeto <see cref="GetReservationsResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetReservationsByUserIdQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetReservationsResponse"/> con los datos de las reservas.</returns>
    public static GetReservationsResponse ToGetReservationsResponse(this GetReservationsByUserIdQueryResponse response) =>
        new(response.Reservations);

    /// <summary>
    /// Convierte un objeto <see cref="GetReservationsBySeatIdQueryResponse"/> a un objeto <see cref="GetReservationsResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="GetReservationsBySeatIdQueryResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetReservationsResponse"/> con los datos de las reservas.</returns>
    public static GetReservationsResponse ToGetReservationsResponse(this GetReservationsBySeatIdQueryResponse response) =>
        new(response.Reservations);
}
