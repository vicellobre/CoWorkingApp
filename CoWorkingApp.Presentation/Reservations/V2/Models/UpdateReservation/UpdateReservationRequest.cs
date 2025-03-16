namespace CoWorkingApp.Presentation.Reservations.V2.Models.UpdateReservation;

/// <summary>
/// Representa una solicitud para actualizar una reserva.
/// </summary>
/// <param name="ReservationId">El identificador de la reserva.</param>
/// <param name="Date">La fecha de la reserva.</param>
/// <param name="UserId">El identificador del usuario que realiza la reserva.</param>
/// <param name="SeatId">El identificador del asiento a reservar.</param>
public record class UpdateReservationRequest(
    Guid ReservationId,
    DateTime Date,
    Guid UserId,
    Guid SeatId);