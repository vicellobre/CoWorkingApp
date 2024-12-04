using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Commands.UpdateSeat;

/// <summary>
/// Comando para actualizar un asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct UpdateSeatCommand(
    Guid SeatId,
    string Name,
    string Description) : ICommand<UpdateSeatCommandResponse>;
