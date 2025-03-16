using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Commands.UpdateReservation;

/// <summary>
/// Comando para actualizar una reserva.
/// </summary>
public record class UpdateReservationCommand : ICommand<UpdateReservationCommandResponse>
{
    /// <summary>
    /// El identificador de la reserva.
    /// </summary>
    public Guid ReservationId { get; private set; }

    /// <summary>
    /// La nueva fecha de la reserva.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// El identificador del usuario asociado a la reserva.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// El identificador del asiento asociado a la reserva.
    /// </summary>
    public Guid SeatId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateReservationCommand"/>.
    /// </summary>
    /// <param name="reservationId">El identificador de la reserva.</param>
    /// <param name="date">La nueva fecha de la reserva.</param>
    /// <param name="userId">El identificador del usuario asociado a la reserva.</param>
    /// <param name="seatId">El identificador del asiento asociado a la reserva.</param>
    public UpdateReservationCommand(Guid reservationId, DateTime date, Guid userId, Guid seatId)
    {
        ReservationId = reservationId;
        Date = date;
        UserId = userId;
        SeatId = seatId;
    }
}
