using CoWorkingApp.Application.Reservations.Commands.CreateReservation;
using CoWorkingApp.Application.Reservations.Commands.DeleteReservation;
using CoWorkingApp.Application.Reservations.Commands.UpdateReservation;
using CoWorkingApp.Application.Reservations.DTOs;
using CoWorkingApp.Application.Reservations.Queries.GetReservationById;
using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Application.Reservations.Extensions;

/// <summary>
/// Define métodos de extensión para la entidad <see cref="Reservation"/>.
/// </summary>
public static class ReservationExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="Reservation"/> a <see cref="ReservationResponse"/>.
    /// </summary>
    /// <param name="reservation">El objeto <see cref="Reservation"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="ReservationResponse"/> con los datos de la reserva.</returns>
    public static ReservationResponse ToReservationResponse(this Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.User.Name.FirstName,
            reservation.User.Name.LastName,
            reservation.User.Credentials.Email,
            reservation.Seat.Name,
            reservation.Seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Reservation"/> a <see cref="CreateReservationCommandResponse"/>.
    /// </summary>
    /// <param name="reservation">El objeto <see cref="Reservation"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateReservationCommandResponse"/> con los datos de la reserva.</returns>
    public static CreateReservationCommandResponse ToCreateReservationCommandResponse(this Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.User.Name.FirstName,
            reservation.User.Name.LastName,
            reservation.User.Credentials.Email,
            reservation.Seat.Name,
            reservation.Seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Reservation"/> a <see cref="DeleteReservationCommandResponse"/>.
    /// </summary>
    /// <param name="reservation">El objeto <see cref="Reservation"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="DeleteReservationCommandResponse"/> con los datos de la reserva.</returns>
    public static DeleteReservationCommandResponse ToDeleteReservationCommandResponse(this Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.User.Name.FirstName,
            reservation.User.Name.LastName,
            reservation.User.Credentials.Email,
            reservation.Seat.Name,
            reservation.Seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Reservation"/> a <see cref="UpdateReservationCommandResponse"/>.
    /// </summary>
    /// <param name="reservation">El objeto <see cref="Reservation"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="UpdateReservationCommandResponse"/> con los datos de la reserva.</returns>
    public static UpdateReservationCommandResponse ToUpdateReservationCommandResponse(this Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.User.Name.FirstName,
            reservation.User.Name.LastName,
            reservation.User.Credentials.Email,
            reservation.Seat.Name,
            reservation.Seat.Description);

    /// <summary>
    /// Convierte un objeto <see cref="Reservation"/> a <see cref="GetReservationByIdQueryResponse"/>.
    /// </summary>
    /// <param name="reservation">El objeto <see cref="Reservation"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetReservationByIdQueryResponse"/> con los datos de la reserva.</returns>
    public static GetReservationByIdQueryResponse ToGetReservationByIdQueryResponse(this Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.User.Name.FirstName,
            reservation.User.Name.LastName,
            reservation.User.Credentials.Email,
            reservation.Seat.Name,
            reservation.Seat.Description);
}
