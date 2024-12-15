using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatId;

/// <summary>
/// Consulta para obtener las reservas por el identificador del asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
public record struct GetReservationsBySeatIdQuery(Guid SeatId) : IQuery<IEnumerable<GetReservationsBySeatIdQueryResponse>>;
