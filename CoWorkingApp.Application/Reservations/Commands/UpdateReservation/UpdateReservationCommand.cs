using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Commands.UpdateReservation;

/// <summary>
/// Comando para actualizar una reserva.
/// </summary>
/// <param name="ReservationId">El identificador de la reserva.</param>
/// <param name="Date">La nueva fecha de la reserva.</param>
/// <param name="UserId">El identificador del usuario asociado a la reserva.</param>
/// <param name="SeatId">El identificador del asiento asociado a la reserva.</param>
public readonly record struct UpdateReservationCommand(
    Guid ReservationId,
    DateTime Date,
    Guid UserId,
    Guid SeatId) : ICommand<UpdateReservationCommandResponse>;
