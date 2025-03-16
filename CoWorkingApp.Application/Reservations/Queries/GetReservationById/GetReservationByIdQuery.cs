using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationById;

/// <summary>
/// Consulta para obtener una reserva por su identificador.
/// </summary>
public record class GetReservationByIdQuery : IQuery<GetReservationByIdQueryResponse>
{
    /// <summary>
    /// El identificador de la reserva.
    /// </summary>
    public Guid ReservationId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationByIdQuery"/>.
    /// </summary>
    /// <param name="reservationId">El identificador de la reserva.</param>
    public GetReservationByIdQuery(Guid reservationId)
    {
        ReservationId = reservationId;
    }
}
