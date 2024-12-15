using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserId;

/// <summary>
/// Representa la respuesta para la consulta de obtener reservas por identificador de usuario.
/// </summary>
/// <param name="ReservationId">El identificador de la reserva.</param>
/// <param name="Date">La fecha de la reserva.</param>
/// <param name="UserFirstName">El nombre de pila del usuario.</param>
/// <param name="UserLastName">El apellido del usuario.</param>
/// <param name="UserEmail">El correo electrónico del usuario.</param>
/// <param name="SeatName">El nombre del asiento.</param>
/// <param name="SeatDescription">La descripción del asiento.</param>
public readonly record struct GetReservationsByUserIdQueryResponse(
    Guid ReservationId,
    DateTime Date,
    string UserFirstName,
    string UserLastName,
    string UserEmail,
    string SeatName,
    string SeatDescription) : IResponse
{
    /// <summary>
    /// Convierte una instancia de <see cref="Reservation"/> en <see cref="GetReservationsByUserIdQueryResponse"/>.
    /// </summary>
    /// <param name="reservation">La reserva a convertir.</param>
    public static explicit operator GetReservationsByUserIdQueryResponse(Reservation reservation) =>
        new(
            reservation.Id,
            reservation.Date,
            reservation.User.Name.FirstName,
            reservation.User.Name.LastName,
            reservation.User.Credentials.Email,
            reservation.Seat.Name,
            reservation.Seat.Description);
}
