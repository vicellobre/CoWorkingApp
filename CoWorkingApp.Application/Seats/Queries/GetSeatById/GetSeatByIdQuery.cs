using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatById;

/// <summary>
/// Consulta para obtener un asiento por su identificador.
/// </summary>
public record class GetSeatByIdQuery : IQuery<GetSeatByIdQueryResponse>
{
    /// <summary>
    /// El identificador del asiento.
    /// </summary>
    public Guid SeatId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetSeatByIdQuery"/>.
    /// </summary>
    /// <param name="seatId">El identificador del asiento.</param>
    public GetSeatByIdQuery(Guid seatId)
    {
        SeatId = seatId;
    }
}
