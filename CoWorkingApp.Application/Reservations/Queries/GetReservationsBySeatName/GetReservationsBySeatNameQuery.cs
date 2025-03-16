using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatName;

/// <summary>
/// Consulta para obtener las reservas por el nombre del asiento.
/// </summary>
public record class GetReservationsBySeatNameQuery : IQuery<GetReservationsBySeatNameQueryResponse>
{
    /// <summary>
    /// El nombre del asiento.
    /// </summary>
    public string SeatName { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsBySeatNameQuery"/>.
    /// </summary>
    /// <param name="seatName">El nombre del asiento.</param>
    public GetReservationsBySeatNameQuery(string seatName)
    {
        SeatName = seatName;
    }
}
