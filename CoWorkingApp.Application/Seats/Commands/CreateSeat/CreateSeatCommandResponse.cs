using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Commands.CreateSeat;

/// <summary>
/// Respuesta al comando para crear un nuevo asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct CreateSeatCommandResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="Seat"/> a <see cref="CreateSeatCommandResponse"/>.
    /// </summary>
    /// <param name="seat">El asiento a convertir.</param>
    public static explicit operator CreateSeatCommandResponse(Seat seat) =>
        new(seat.Id, seat.Name, seat.Description);
}
