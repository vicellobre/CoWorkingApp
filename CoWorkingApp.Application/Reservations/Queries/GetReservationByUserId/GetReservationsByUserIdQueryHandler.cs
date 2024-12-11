using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationByUserId;

/// <summary>
/// Manejador para la consulta de obtener reservas por identificador de usuario.
/// </summary>
public sealed class GetReservationsByUserIdQueryHandler : IQueryHandler<GetReservationsByUserIdQuery, IEnumerable<GetReservationsByUserIdQueryResponse>>
{
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByUserIdQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <exception cref="ArgumentNullException">Se lanza cuando <paramref name="reservationRepository"/> es nulo.</exception>
    public GetReservationsByUserIdQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la consulta para obtener reservas por identificador de usuario.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token para notificar la cancelación de la operación.</param>
    /// <returns>Un <see cref="Result{T}"/> que contiene la lista de reservas.</returns>
    /// <exception cref="NotImplementedException">Se lanza cuando el método no está implementado.</exception>
    public async Task<Result<IEnumerable<GetReservationsByUserIdQueryResponse>>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetByUserIdAsNoTrackingAsync(request.UserId, cancellationToken);

        return reservations.Select(reservation => (GetReservationsByUserIdQueryResponse)reservation).ToList();
    }
}
