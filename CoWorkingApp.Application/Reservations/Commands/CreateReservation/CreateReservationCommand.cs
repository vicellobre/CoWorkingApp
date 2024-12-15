using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Comando para crear una nueva reserva.
/// </summary>
/// <param name="Date">La fecha de la reserva.</param>
/// <param name="UserId">El identificador del usuario.</param>
/// <param name="SeatId">El identificador del asiento.</param>
public readonly record struct CreateReservationCommand(
    DateTime Date,
    Guid UserId,
    Guid SeatId) : ICommand<CreateReservationCommandResponse>;
