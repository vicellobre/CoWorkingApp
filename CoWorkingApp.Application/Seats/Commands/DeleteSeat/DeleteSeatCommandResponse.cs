using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Commands.DeleteSeat;

/// <summary>
/// Respuesta al comando para eliminar un asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct DeleteSeatCommandResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="Seat"/> a <see cref="DeleteSeatCommandResponse"/>.
    /// </summary>
    /// <param name="seat">El asiento a convertir.</param>
    public static explicit operator DeleteSeatCommandResponse(Seat seat) =>
        new(seat.Id, seat.Name, seat.Description);
}
