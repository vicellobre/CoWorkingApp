using CoWorkingApp.Application.Reservations.Commands.UpdateReservation;
using CoWorkingApp.Presentation.Reservations.V2.Models.UpdateReservation;

namespace CoWorkingApp.Presentation.Reservations.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la actualización de reservas.
/// </summary>
public static class UpdateReservationExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="UpdateReservationRequest"/> a un objeto <see cref="UpdateReservationCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="UpdateReservationRequest"/> que se va a convertir.</param>
    /// <param name="reservationId">El identificador de la reserva.</param>
    /// <returns>Una instancia de <see cref="UpdateReservationCommand"/> con los datos de la actualización de la reserva.</returns>
    public static UpdateReservationCommand ToUpdateReservationCommand(this UpdateReservationRequest request, Guid reservationId) =>
        new(
            reservationId,
            request.Date,
            request.UserId,
            request.SeatId);
}
