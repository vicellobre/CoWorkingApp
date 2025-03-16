using CoWorkingApp.Application.Seats.DTOs;

namespace CoWorkingApp.Presentation.Seats.V2.Models.GetSeats;

/// <summary>
/// Representa la respuesta para obtener asientos.
/// </summary>
/// <param name="Seats">La colección de asientos.</param>
public record class GetSeatsResponse(IEnumerable<SeatResponse> Seats);
