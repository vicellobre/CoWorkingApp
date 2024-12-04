using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatByName;

/// <summary>
/// Maneja la consulta para obtener un asiento por su nombre.
/// </summary>
public sealed class GetSeatByNameQueryHandler : IQueryHandler<GetSeatByNameQuery, GetSeatByNameQueryResponse>
{
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetSeatByNameQueryHandler"/>.
    /// </summary>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de asientos es null.</exception>
    public GetSeatByNameQueryHandler(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetSeatByNameQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta de la consulta para obtener un asiento por su nombre.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result<GetSeatByNameQueryResponse>> Handle(GetSeatByNameQuery request, CancellationToken cancellationToken)
    {
        SeatName name = SeatName.ConvertFromString(request.Name).Value;

        var seat = await _seatRepository.GetByNameAsync(name, cancellationToken);
        if (seat == null)
        {
            return Result.Failure<GetSeatByNameQueryResponse>(Errors.Seat.NameNotExist(request.Name));
        }

        return (GetSeatByNameQueryResponse)seat;
    }
}
