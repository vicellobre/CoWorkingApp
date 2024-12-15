using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Commands.DeleteSeat;

/// <summary>
/// Comando para eliminar un asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
public readonly record struct DeleteSeatCommand(Guid SeatId) : ICommand<DeleteSeatCommandResponse>;
