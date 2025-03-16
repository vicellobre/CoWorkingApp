using CoWorkingApp.Application.Reservations.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserId;

/// <summary>
/// Representa la respuesta para la consulta de obtener reservas por identificador de usuario.
/// </summary>
/// <param name="Reservations">La colección de reservas.</param>
public record class GetReservationsByUserIdQueryResponse(
    IEnumerable<ReservationResponse> Reservations) : IResponse;
