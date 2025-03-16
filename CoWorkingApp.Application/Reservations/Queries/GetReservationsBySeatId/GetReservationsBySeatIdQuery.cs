using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatId;

/// <summary>
/// Consulta para obtener las reservas por el identificador del asiento.
/// </summary>
public record class GetReservationsBySeatIdQuery : IQuery<GetReservationsBySeatIdQueryResponse>
{
    /// <summary>
    /// El identificador del asiento.
    /// </summary>
    public Guid SeatId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsBySeatIdQuery"/>.
    /// </summary>
    /// <param name="seatId">El identificador del asiento.</param>
    public GetReservationsBySeatIdQuery(Guid seatId)
    {
        SeatId = seatId;
    }
}
