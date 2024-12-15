using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetAllReservations;

/// <summary>
/// Maneja la consulta para obtener todas las reservas.
/// </summary>
public sealed class GetAllReservationsQueryHandler : IQueryHandler<GetAllReservationsQuery, IEnumerable<GetAllReservationsQueryResponse>>
{
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetAllReservationsQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de reservas es null.</exception>
    public GetAllReservationsQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetAllReservationsQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Una colección de respuestas de la consulta para obtener todas las reservas.</returns>
    public async Task<Result<IEnumerable<GetAllReservationsQueryResponse>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetAllAsNoTrackingAsync(cancellationToken);

        return reservations.Select(reservation => (GetAllReservationsQueryResponse)reservation).ToList();
    }
}
