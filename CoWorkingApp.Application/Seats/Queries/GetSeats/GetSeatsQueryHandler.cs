using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Seats.Extensions;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Errors;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Core.Shared;
using Microsoft.IdentityModel.Tokens;

namespace CoWorkingApp.Application.Seats.Queries.GetSeats;

/// <summary>
/// Maneja la consulta para obtener asientos.
/// </summary>
public sealed class GetSeatsQueryHandler : IQueryHandler<GetSeatsQuery, GetSeatsQueryResponse>
{
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetSeatsQueryHandler"/>.
    /// </summary>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de asientos es null.</exception>
    public GetSeatsQueryHandler(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetSeatsQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Una colección de respuestas de la consulta para obtener asientos.</returns>
    public async Task<Result<GetSeatsQueryResponse>> Handle(GetSeatsQuery request, CancellationToken cancellationToken)
    {
        var seats = await _seatRepository.GetAllAsNoTrackingAsync(cancellationToken);

        if (seats.IsNullOrEmpty())
        {
            return Result.Failure<GetSeatsQueryResponse>(ERRORS.Seat.NoSeatsFound);
        }

        var seatResponses = seats.Select(seat => seat.ToSeatResponse());

        return new GetSeatsQueryResponse(seatResponses);
    }
}
