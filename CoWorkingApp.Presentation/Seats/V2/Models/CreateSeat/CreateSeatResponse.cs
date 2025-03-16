namespace CoWorkingApp.Presentation.Seats.V2.Models.CreateSeat;

/// <summary>
/// Representa la respuesta para la creación de un nuevo asiento.
/// </summary>
/// <param name="Id">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record class CreateSeatResponse(
    Guid Id,
    string Name,
    string Description);