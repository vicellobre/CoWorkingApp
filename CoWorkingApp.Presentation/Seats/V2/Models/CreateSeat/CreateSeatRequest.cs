namespace CoWorkingApp.Presentation.Seats.V2.Models.CreateSeat;

/// <summary>
/// Representa una solicitud para crear un nuevo asiento.
/// </summary>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record class CreateSeatRequest(
    string Name,
    string Description);
