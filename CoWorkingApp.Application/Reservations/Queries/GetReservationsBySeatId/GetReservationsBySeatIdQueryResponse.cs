using CoWorkingApp.Application.Reservations.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatId;

/// <summary>
/// Representa la respuesta para la consulta de obtener reservas por identificador de asiento.
/// </summary>
/// <param name="Reservations">La colección de reservas.</param>
public record class GetReservationsBySeatIdQueryResponse(
    IEnumerable<ReservationResponse> Reservations) : IResponse;