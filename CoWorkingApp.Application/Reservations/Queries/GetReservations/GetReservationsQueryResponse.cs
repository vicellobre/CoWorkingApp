using CoWorkingApp.Application.Reservations.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservations;

/// <summary>
/// Respuesta a la consulta para obtener reservas.
/// </summary>
/// <param name="Reservations">La colección de reservas.</param>
public record class GetReservationsQueryResponse(
    IEnumerable<ReservationResponse> Reservations) : IResponse;
