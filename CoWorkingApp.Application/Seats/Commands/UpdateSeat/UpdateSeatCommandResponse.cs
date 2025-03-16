using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Commands.UpdateSeat;

/// <summary>
/// Respuesta al comando para actualizar un asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record class UpdateSeatCommandResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse;
