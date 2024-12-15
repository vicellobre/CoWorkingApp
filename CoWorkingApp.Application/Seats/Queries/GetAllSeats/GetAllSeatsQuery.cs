using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Queries.GetAllSeats;

/// <summary>
/// Consulta para obtener todos los asientos.
/// </summary>
public readonly record struct GetAllSeatsQuery() : IQuery<IEnumerable<GetAllSeatsQueryResponse>>;
