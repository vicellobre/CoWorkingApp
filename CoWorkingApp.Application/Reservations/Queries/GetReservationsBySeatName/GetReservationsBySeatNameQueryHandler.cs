using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Reservations.Extensions;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Errors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatName;

/// <summary>
/// Maneja la consulta para obtener las reservas por el nombre del asiento.
/// </summary>
public sealed class GetReservationsBySeatNameQueryHandler : IQueryHandler<GetReservationsBySeatNameQuery, GetReservationsBySeatNameQueryResponse>
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
    public async Task<Result<GetReservationsBySeatNameQueryResponse>> Handle(GetReservationsBySeatNameQuery request, CancellationToken cancellationToken)
    {
        SeatName seatName = SeatName.CreateFromString(request.SeatName).Value;

        bool notFound = await _seatRepository.GetByNameAsync(seatName, cancellationToken) is null;
        if (notFound)
        {
            return Result.Failure<GetReservationsBySeatNameQueryResponse>(ERRORS.Seat.NameNotExist(request.SeatName));
        }

        var reservations = await _reservationRepository.GetBySeatNameAsNoTrackingAsync(seatName, cancellationToken);

        var reservationResponses = reservations.Select(reservation => reservation.ToReservationResponse());

        return new GetReservationsBySeatNameQueryResponse(reservationResponses);

    }
}
