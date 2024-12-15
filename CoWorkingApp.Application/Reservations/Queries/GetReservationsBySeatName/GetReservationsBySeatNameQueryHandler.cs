using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatName;

/// <summary>
/// Maneja la consulta para obtener las reservas por el nombre del asiento.
/// </summary>
public sealed class GetReservationsBySeatNameQueryHandler : IQueryHandler<GetReservationsBySeatNameQuery, IEnumerable<GetReservationsBySeatNameQueryResponse>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsBySeatNameQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de reservas o el repositorio de asientos es null.</exception>
    public GetReservationsBySeatNameQueryHandler(IReservationRepository reservationRepository, ISeatRepository seatRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetReservationsBySeatNameQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Una colección de respuestas de la consulta para obtener las reservas por el nombre del asiento.</returns>
    public async Task<Result<IEnumerable<GetReservationsBySeatNameQueryResponse>>> Handle(GetReservationsBySeatNameQuery request, CancellationToken cancellationToken)
    {
        SeatName seatName = SeatName.ConvertFromString(request.SeatName).Value;

        bool notFound = await _seatRepository.GetByNameAsync(seatName, cancellationToken) is null;
        if (notFound)
        {
            return Result.Failure<IEnumerable<GetReservationsBySeatNameQueryResponse>>(Errors.Seat.NameNotExist(request.SeatName));
        }

        var reservations = await _reservationRepository.GetBySeatNameAsNoTrackingAsync(seatName, cancellationToken);
        return reservations.Select(reservation => (GetReservationsBySeatNameQueryResponse)reservation).ToList();
    }
}
