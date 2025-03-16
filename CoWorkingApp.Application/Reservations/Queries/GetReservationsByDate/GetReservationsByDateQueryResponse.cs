using CoWorkingApp.Application.Reservations.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;

/// <summary>
/// Respuesta a la consulta para obtener las reservas por la fecha especificada.
/// </summary>
/// <param name="Reservations">La colección de reservas.</param>
public record class GetReservationsByDateQueryResponse(
    IEnumerable<ReservationResponse> Reservations) : IResponse;
