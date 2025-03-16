namespace CoWorkingApp.Application.Seats.DTOs;

/// <summary>
/// Representa la respuesta para un asiento.
/// </summary>
/// <param name="SeatId">El ID del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct SeatResponse(
    Guid SeatId,
    string Name,
    string Description);
