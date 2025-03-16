using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Reservations.Extensions;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservations;

/// <summary>
/// Maneja la consulta para obtener reservas.
/// </summary>
public sealed class GetReservationsQueryHandler
    : IQueryHandler<GetReservationsQuery, GetReservationsQueryResponse>
{
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de reservas es null.</exception>
    public GetReservationsQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetReservationsQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Una colección de respuestas de la consulta para obtener reservas.</returns>
    public async Task<Result<GetReservationsQueryResponse>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetAllAsNoTrackingAsync(cancellationToken);

        var reservationResponses = reservations.Select(reservation => reservation.ToReservationResponse());

        return new GetReservationsQueryResponse(reservationResponses);
    }
}
