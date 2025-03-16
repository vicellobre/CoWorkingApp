using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Commands.CreateSeat;

/// <summary>
/// Respuesta al comando para crear un nuevo asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record class CreateSeatCommandResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse;
