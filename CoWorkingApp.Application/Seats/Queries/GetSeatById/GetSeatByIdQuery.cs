using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatById;

/// <summary>
/// Consulta para obtener un asiento por su identificador.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
public readonly record struct GetSeatByIdQuery(Guid SeatId) : IQuery<GetSeatByIdQueryResponse>;
