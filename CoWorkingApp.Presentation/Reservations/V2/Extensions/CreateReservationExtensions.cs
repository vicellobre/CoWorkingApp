using CoWorkingApp.Application.Reservations.Commands.CreateReservation;
using CoWorkingApp.Presentation.Reservations.V2.Models.CreateReservation;

namespace CoWorkingApp.Presentation.Reservations.V2.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir DTOs relacionados con la creación de reservas.
/// </summary>
public static class CreateReservationExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="CreateReservationRequest"/> a un objeto <see cref="CreateReservationCommand"/>.
    /// </summary>
    /// <param name="request">El objeto <see cref="CreateReservationRequest"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateReservationCommand"/> con los datos de la reserva.</returns>
    public static CreateReservationCommand ToCreateReservationCommand(this CreateReservationRequest request) =>
        new(request.Date, request.UserId, request.SeatId);

    /// <summary>
    /// Convierte un objeto <see cref="CreateReservationCommandResponse"/> a un objeto <see cref="CreateReservationResponse"/>.
    /// </summary>
    /// <param name="response">El objeto <see cref="CreateReservationCommandResponse"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateReservationResponse"/> con los datos de la reserva.</returns>
    public static CreateReservationResponse ToCreateReservationResponse(this CreateReservationCommandResponse response) =>
        new(
            response.ReservationId,
            response.Date,
            response.UserFirstName,
            response.UserLastName,
            response.UserEmail,
            response.SeatName,
            response.SeatDescription);
}
