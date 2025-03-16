using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Commands.DeleteReservation;

/// <summary>
/// Comando para eliminar una reserva.
/// </summary>
public record class DeleteReservationCommand : ICommand<DeleteReservationCommandResponse>
{
    /// <summary>
    /// El identificador de la reserva a eliminar.
    /// </summary>
    public Guid ReservationId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DeleteReservationCommand"/>.
    /// </summary>
    /// <param name="reservationId">El identificador de la reserva a eliminar.</param>
    public DeleteReservationCommand(Guid reservationId)
    {
        ReservationId = reservationId;
    }
}
