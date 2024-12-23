﻿using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Respuesta al comando para crear una nueva reserva.
/// </summary>
/// <param name="ReservationId">El identificador de la reserva.</param>
/// <param name="Date">La fecha de la reserva.</param>
/// <param name="UserFirstName">El nombre del usuario.</param>
/// <param name="UserLastName">El apellido del usuario.</param>
/// <param name="UserEmail">El correo electrónico del usuario.</param>
/// <param name="SeatName">El nombre del asiento.</param>
/// <param name="SeatDescription">La descripción del asiento.</param>
public readonly record struct CreateReservationCommandResponse(
    Guid ReservationId,
    DateTime Date,
    string UserFirstName,
    string UserLastName,
    string UserEmail,
    string SeatName,
    string SeatDescription) : IResponse
{
    /// <summary>
    /// Convierte explícitamente un objeto <see cref="Reservation"/> a <see cref="CreateReservationCommandResponse"/>.
    /// </summary>
    /// <param name="reservation">La reserva a convertir.</param>
    public static explicit operator CreateReservationCommandResponse(Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.User.Name.FirstName,
            reservation.User.Name.LastName,
            reservation.User.Credentials.Email,
            reservation.Seat.Name,
            reservation.Seat.Description);
}
