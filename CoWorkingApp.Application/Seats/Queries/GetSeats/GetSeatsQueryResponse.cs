using CoWorkingApp.Application.Seats.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Queries.GetSeats;

/// <summary>
/// Respuesta a la consulta para obtener asientos.
/// </summary>
/// <param name="Seats">La colección de asientos.</param>
public record class GetSeatsQueryResponse(IEnumerable<SeatResponse> Seats) : IResponse;
