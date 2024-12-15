using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatByName;

/// <summary>
/// Respuesta a la consulta para obtener un asiento por su nombre.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct GetSeatByNameQueryResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="Seat"/> a <see cref="GetSeatByNameQueryResponse"/>.
    /// </summary>
    /// <param name="seat">El asiento a convertir.</param>
    public static explicit operator GetSeatByNameQueryResponse(Seat seat) =>
        new(seat.Id, seat.Name, seat.Description);
}
