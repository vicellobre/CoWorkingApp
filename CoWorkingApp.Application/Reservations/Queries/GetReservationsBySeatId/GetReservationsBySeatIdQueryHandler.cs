using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatId;

/// <summary>
/// Manejador para la consulta de obtener reservas por identificador de asiento.
/// </summary>
public sealed class GetReservationsBySeatIdQueryHandler : IQueryHandler<GetReservationsBySeatIdQuery, IEnumerable<GetReservationsBySeatIdQueryResponse>>
{
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsBySeatIdQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <exception cref="ArgumentNullException">Se lanza cuando <paramref name="reservationRepository"/> es nulo.</exception>
    public GetReservationsBySeatIdQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la consulta para obtener reservas por identificador de asiento.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token para notificar la cancelación de la operación.</param>
    /// <returns>Un <see cref="Result{T}"/> que contiene la lista de reservas.</returns>
    public async Task<Result<IEnumerable<GetReservationsBySeatIdQueryResponse>>> Handle(GetReservationsBySeatIdQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetBySeatIdAsNoTrackingAsync(request.SeatId, cancellationToken);

        return reservations.Select(reservation => (GetReservationsBySeatIdQueryResponse)reservation).ToList();
    }
}
