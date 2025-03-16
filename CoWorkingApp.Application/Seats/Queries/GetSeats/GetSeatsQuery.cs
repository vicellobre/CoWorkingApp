using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Queries.GetSeats;

/// <summary>
/// Consulta para obtener asientos.
/// </summary>
public record class GetSeatsQuery : IQuery<GetSeatsQueryResponse>;
