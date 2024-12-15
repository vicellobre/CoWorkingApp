using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserId;

/// <summary>
/// Consulta para obtener las reservas por el identificador del usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
public record struct GetReservationsByUserIdQuery(Guid UserId) : IQuery<IEnumerable<GetReservationsByUserIdQueryResponse>>;
