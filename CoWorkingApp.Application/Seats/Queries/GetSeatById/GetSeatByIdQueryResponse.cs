using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatById;

/// <summary>
/// Respuesta a la consulta para obtener un asiento por su identificador.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct GetSeatByIdQueryResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse;