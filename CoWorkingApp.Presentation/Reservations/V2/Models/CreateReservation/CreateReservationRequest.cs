namespace CoWorkingApp.Presentation.Reservations.V2.Models.CreateReservation;

/// <summary>
/// Representa una solicitud para crear una nueva reserva.
/// </summary>
/// <param name="Date">La fecha de la reserva.</param>
/// <param name="UserId">El identificador del usuario que realiza la reserva.</param>
/// <param name="SeatId">El identificador del asiento a reservar.</param>
public record class CreateReservationRequest(
    DateTime Date,
    Guid UserId,
    Guid SeatId);