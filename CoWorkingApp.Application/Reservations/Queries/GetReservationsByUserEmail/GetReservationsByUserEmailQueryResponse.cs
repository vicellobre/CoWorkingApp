using CoWorkingApp.Application.Reservations.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserEmail;

/// <summary>
/// Respuesta a la consulta para obtener las reservas por el correo electrónico del usuario.
/// </summary>
/// <param name="Reservations">La lista de reservas.</param>
public record class GetReservationsByUserEmailQueryResponse(
    IEnumerable<ReservationResponse> Reservations) : IResponse;
