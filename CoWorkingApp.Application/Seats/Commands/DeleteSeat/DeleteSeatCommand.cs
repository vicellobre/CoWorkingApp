using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Commands.DeleteSeat;

/// <summary>
/// Comando para eliminar un asiento.
/// </summary>
public record class DeleteSeatCommand : ICommand<DeleteSeatCommandResponse>
{
    /// <summary>
    /// El identificador del asiento.
    /// </summary>
    public Guid SeatId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DeleteSeatCommand"/>.
    /// </summary>
    /// <param name="seatId">El identificador del asiento.</param>
    public DeleteSeatCommand(Guid seatId)
    {
        SeatId = seatId;
    }
}
