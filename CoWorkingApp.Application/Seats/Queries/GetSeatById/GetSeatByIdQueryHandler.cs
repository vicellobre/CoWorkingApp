using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatById;

/// <summary>
/// Maneja la consulta para obtener un asiento por su identificador.
/// </summary>
public sealed class GetSeatByIdQueryHandler : IQueryHandler<GetSeatByIdQuery, GetSeatByIdQueryResponse>
{
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetSeatByIdQueryHandler"/>.
    /// </summary>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de asientos es null.</exception>
    public GetSeatByIdQueryHandler(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetSeatByIdQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta de la consulta para obtener un asiento por su identificador.</returns>
    public async Task<Result<GetSeatByIdQueryResponse>> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
    {
        var seat = await _seatRepository.GetByIdAsNoTrackingAsync(request.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure<GetSeatByIdQueryResponse>(Errors.Seat.NotFound(request.SeatId));
        }

        return (GetSeatByIdQueryResponse)seat;
    }
}
