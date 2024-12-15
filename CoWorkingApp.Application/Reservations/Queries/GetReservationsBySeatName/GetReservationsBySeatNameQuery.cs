using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatName;

/// <summary>
/// Consulta para obtener las reservas por el nombre del asiento.
/// </summary>
/// <param name="SeatName">El nombre del asiento.</param>
public readonly record struct GetReservationsBySeatNameQuery(string SeatName) : IQuery<IEnumerable<GetReservationsBySeatNameQueryResponse>>;
