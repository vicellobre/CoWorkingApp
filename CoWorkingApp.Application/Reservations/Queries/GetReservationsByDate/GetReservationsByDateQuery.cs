using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;

/// <summary>
/// Consulta para obtener las reservas por la fecha especificada.
/// </summary>
public record class GetReservationsByDateQuery : IQuery<GetReservationsByDateQueryResponse>
{
    /// <summary>
    /// La fecha de las reservas.
    /// </summary>
    public DateTime DateTime { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByDateQuery"/>.
    /// </summary>
    /// <param name="dateTime">La fecha de las reservas.</param>
    public GetReservationsByDateQuery(DateTime dateTime)
    {
        DateTime = dateTime;
    }
}
