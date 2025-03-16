using CoWorkingApp.Application.Reservations.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatName;

/// <summary>
/// Respuesta a la consulta para obtener las reservas por el nombre del asiento.
/// </summary>
/// <param name="Reservations">La lista de reservas.</param>
public record class GetReservationsBySeatNameQueryResponse(
    IEnumerable<ReservationResponse> Reservations) : IResponse;
