using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatByName;

/// <summary>
/// Respuesta a la consulta para obtener un asiento por su nombre.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record class GetSeatByNameQueryResponse(
    Guid SeatId,
    string Name,
    string Description) : IResponse;
