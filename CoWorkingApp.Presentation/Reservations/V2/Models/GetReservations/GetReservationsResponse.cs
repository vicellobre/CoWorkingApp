using CoWorkingApp.Application.Reservations.DTOs;

namespace CoWorkingApp.Presentation.Reservations.V2.Models.GetReservations;

/// <summary>
/// Representa la respuesta para obtener todas las reservas.
/// </summary>
/// <param name="Reservations">La colección de reservas.</param>
public record class GetReservationsResponse(IEnumerable<ReservationResponse> Reservations);