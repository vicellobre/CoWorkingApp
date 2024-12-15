using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatById;

/// <summary>
/// Respuesta a la consulta para obtener un asiento por su identificador.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct GetSeatByIdQueryResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="Seat"/> a <see cref="GetSeatByIdQueryResponse"/>.
    /// </summary>
    /// <param name="seat">El asiento a convertir.</param>
    public static explicit operator GetSeatByIdQueryResponse(Seat seat) =>
        new(seat.Id, seat.Name, seat.Description);
}
