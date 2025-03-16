namespace CoWorkingApp.Presentation.Seats.V2.Models.UpdateSeat;

/// <summary>
/// Representa una solicitud para actualizar un asiento.
/// </summary>
/// <param name="Id">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record class UpdateSeatRequest(
    Guid Id,
    string Name,
    string Description);
