using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserId;

/// <summary>
/// Consulta para obtener las reservas por el identificador del usuario.
/// </summary>
public record class GetReservationsByUserIdQuery : IQuery<GetReservationsByUserIdQueryResponse>
{
    /// <summary>
    /// El identificador del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByUserIdQuery"/>.
    /// </summary>
    /// <param name="userId">El identificador del usuario.</param>
    public GetReservationsByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}
