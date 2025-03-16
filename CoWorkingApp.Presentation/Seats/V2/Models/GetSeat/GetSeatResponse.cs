namespace CoWorkingApp.Presentation.Seats.V2.Models.GetSeat;

/// <summary>
/// Representa la respuesta para la consulta de obtención de un asiento por ID.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
public record class GetSeatResponse(
    Guid Id,
    string Name,
    string Description);
