using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Queries.GetAllSeats;

/// <summary>
/// Maneja la consulta para obtener todos los asientos.
/// </summary>
public sealed class GetAllSeatsQueryHandler : IQueryHandler<GetAllSeatsQuery, IEnumerable<GetAllSeatsQueryResponse>>
{
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetAllSeatsQueryHandler"/>.
    /// </summary>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de asientos es null.</exception>
    public GetAllSeatsQueryHandler(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetAllSeatsQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Una colección de respuestas de la consulta para obtener todos los asientos.</returns>
    public async Task<Result<IEnumerable<GetAllSeatsQueryResponse>>> Handle(GetAllSeatsQuery request, CancellationToken cancellationToken)
    {
        var seats = await _seatRepository.GetAllAsNoTrackingAsync(cancellationToken);
        return seats.Select(seat => (GetAllSeatsQueryResponse)seat).ToList();
    }
}
