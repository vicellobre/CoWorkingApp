namespace CoWorkingApp.Presentation.Reservations.V2.Models.GetReservation;

/// <summary>
/// Representa la respuesta para obtener una reserva.
/// </summary>
/// <param name="ReservationId">El identificador de la reserva.</param>
/// <param name="Date">La fecha de la reserva.</param>
/// <param name="UserFirstName">El nombre del usuario que realizó la reserva.</param>
/// <param name="UserLastName">El apellido del usuario que realizó la reserva.</param>
/// <param name="UserEmail">El correo electrónico del usuario que realizó la reserva.</param>
/// <param name="SeatName">El nombre del asiento reservado.</param>
/// <param name="SeatDescription">La descripción del asiento reservado.</param>
public record class GetReservationResponse(
    Guid ReservationId,
    DateTime Date,
    string UserFirstName,
    string UserLastName,
    string UserEmail,
    string SeatName,
    string SeatDescription);