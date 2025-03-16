using CoWorkingApp.Application.Seats.Commands.UpdateSeat;
using CoWorkingApp.Presentation.Seats.V2.Models.UpdateSeat;

namespace CoWorkingApp.Presentation.Seats.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la actualización de asientos.
/// </summary>
public static class UpdateSeatExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="UpdateSeatRequest"/> a un objeto <see cref="UpdateSeatCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="UpdateSeatRequest"/> que se va a convertir.</param>
    /// <param name="seatId">El identificador del asiento.</param>
    /// <returns>Una instancia de <see cref="UpdateSeatCommand"/> con los datos de la actualización del asiento.</returns>
    public static UpdateSeatCommand ToUpdateSeatCommand(this UpdateSeatRequest request, Guid seatId) =>
        new(seatId, request.Name, request.Description);
}