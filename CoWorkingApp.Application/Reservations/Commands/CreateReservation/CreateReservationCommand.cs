using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Comando para crear una nueva reserva.
/// </summary>
public record class CreateReservationCommand : ICommand<CreateReservationCommandResponse>
{
    /// <summary>
    /// La fecha de la reserva.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// El identificador del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// El identificador del asiento.
    /// </summary>
    public Guid SeatId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateReservationCommand"/>.
    /// </summary>
    /// <param name="date">La fecha de la reserva.</param>
    /// <param name="userId">El identificador del usuario.</param>
    /// <param name="seatId">El identificador del asiento.</param>
    public CreateReservationCommand(DateTime date, Guid userId, Guid seatId)
    {
        Date = date;
        UserId = userId;
        SeatId = seatId;
    }
}
