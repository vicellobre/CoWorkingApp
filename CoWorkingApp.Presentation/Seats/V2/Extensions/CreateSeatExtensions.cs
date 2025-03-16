using CoWorkingApp.Application.Seats.Commands.CreateSeat;
using CoWorkingApp.Presentation.Seats.V2.Models.CreateSeat;

namespace CoWorkingApp.Presentation.Seats.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la creación de asientos.
/// </summary>
public static class CreateSeatExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="CreateSeatRequest"/> a un objeto <see cref="CreateSeatCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="CreateSeatRequest"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateSeatCommand"/> con los datos del asiento.</returns>
    public static CreateSeatCommand ToCreateSeatCommand(this CreateSeatRequest request) =>
        new(request.Name, request.Description);

    /// <summary>
    /// Convierte un objeto <see cref="CreateSeatCommandResponse"/> a un objeto <see cref="CreateSeatResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="CreateSeatCommandResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateSeatResponse"/> con los datos del asiento.</returns>
    public static CreateSeatResponse ToCreateSeatResponse(this CreateSeatCommandResponse response) =>
        new(response.SeatId, response.Name, response.Description);
}
