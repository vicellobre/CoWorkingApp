using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;

/// <summary>
/// Consulta para obtener las reservas por la fecha especificada.
/// </summary>
/// <param name="DateTime">La fecha de las reservas.</param>
public readonly record struct GetReservationsByDateQuery(DateTime DateTime) : IQuery<IEnumerable<GetReservationsByDateQueryResponse>>;
