using CoWorkingApp.Application.Seats.Commands.CreateSeat;
using CoWorkingApp.Application.Seats.Commands.DeleteSeat;
using CoWorkingApp.Application.Seats.Commands.UpdateSeat;
using CoWorkingApp.Application.Seats.DTOs;
using CoWorkingApp.Application.Seats.Queries.GetSeatById;
using CoWorkingApp.Application.Seats.Queries.GetSeatByName;
using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Application.Seats.Extensions;

/// <summary>
/// Define métodos de extensión para la entidad <see cref="Seat"/>.
/// </summary>
public static class SeatExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="Seat"/> a <see cref="SeatResponse"/>.
    /// </summary>
    /// <param name="seat">El objeto <see cref="Seat"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="SeatResponse"/> con los datos del asiento.</returns>
    public static SeatResponse ToSeatResponse(this Seat seat) =>
        new(
            seat.Id,
            seat.Name,
            seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Seat"/> a <see cref="CreateSeatCommandResponse"/>.
    /// </summary>
    /// <param name="seat">El objeto <see cref="Seat"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateSeatCommandResponse"/> con los datos del asiento.</returns>
    public static CreateSeatCommandResponse ToCreateSeatCommandResponse(this Seat seat) =>
        new(
            seat.Id,
            seat.Name,
            seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Seat"/> a <see cref="DeleteSeatCommandResponse"/>.
    /// </summary>
    /// <param name="seat">El objeto <see cref="Seat"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="DeleteSeatCommandResponse"/> con los datos del asiento.</returns>
    public static DeleteSeatCommandResponse ToDeleteSeatCommandResponse(this Seat seat) =>
        new(
            seat.Id,
            seat.Name,
            seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Seat"/> a <see cref="UpdateSeatCommandResponse"/>.
    /// </summary>
    /// <param name="seat">El objeto <see cref="Seat"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="UpdateSeatCommandResponse"/> con los datos del asiento.</returns>
    public static UpdateSeatCommandResponse ToUpdateSeatCommandResponse(this Seat seat) =>
        new(
            seat.Id,
            seat.Name,
            seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Seat"/> a <see cref="GetSeatByIdQueryResponse"/>.
    /// </summary>
    /// <param name="seat">El objeto <see cref="Seat"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetSeatByIdQueryResponse"/> con los datos del asiento.</returns>
    public static GetSeatByIdQueryResponse ToGetSeatByIdQueryResponse(this Seat seat) =>
        new(
            seat.Id,
            seat.Name,
            seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Seat"/> a <see cref="GetSeatByNameQueryResponse"/>.
    /// </summary>
    /// <param name="seat">El objeto <see cref="Seat"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetSeatByNameQueryResponse"/> con los datos del asiento.</returns>
    public static GetSeatByNameQueryResponse ToGetSeatByNameQueryResponse(this Seat seat) =>
        new(
            seat.Id,
            seat.Name,
            seat.Description);
}
