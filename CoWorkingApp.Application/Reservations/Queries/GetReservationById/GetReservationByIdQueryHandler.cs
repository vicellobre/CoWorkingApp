using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationById;

/// <summary>
/// Maneja la consulta para obtener una reserva por su identificador.
/// </summary>
public sealed class GetReservationByIdQueryHandler : IQueryHandler<GetReservationByIdQuery, GetReservationByIdQueryResponse>
{
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationByIdQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de reservas es null.</exception>
    public GetReservationByIdQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetReservationByIdQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta de la consulta para obtener una reserva por su identificador.</returns>
    public async Task<Result<GetReservationByIdQueryResponse>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsNoTrackingAsync(request.ReservationId, cancellationToken);
        if (reservation is null)
        {
            return Result.Failure<GetReservationByIdQueryResponse>(Errors.Reservation.NotFound(request.ReservationId));
        }

        return (GetReservationByIdQueryResponse)reservation;
    }
}
