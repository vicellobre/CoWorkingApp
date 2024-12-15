using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;

/// <summary>
/// Maneja la consulta para obtener las reservas por la fecha especificada.
/// </summary>
public sealed class GetReservationsByDateQueryHandler : IQueryHandler<GetReservationsByDateQuery, IEnumerable<GetReservationsByDateQueryResponse>>
{
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByDateQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de reservas es null.</exception>
    public GetReservationsByDateQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetReservationsByDateQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Una colección de respuestas de la consulta para obtener las reservas por la fecha especificada.</returns>
    public async Task<Result<IEnumerable<GetReservationsByDateQueryResponse>>> Handle(GetReservationsByDateQuery request, CancellationToken cancellationToken)
    {
        Date date = Date.Create(request.DateTime).Value;

        var reservations = await _reservationRepository.GetByDateAsNoTrackingAsync(date, cancellationToken);

        return reservations.Select(reservation => (GetReservationsByDateQueryResponse)reservation).ToList();
    }
}
