using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Commands.DeleteReservation;

/// <summary>
/// Comando para eliminar una reserva.
/// </summary>
/// <param name="ReservationId">El identificador de la reserva a eliminar.</param>
public readonly record struct DeleteReservationCommand(Guid ReservationId) : ICommand<DeleteReservationCommandResponse>;
