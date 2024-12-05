using CoWorkingApp.Application.Reservations.Services.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs;

/// <summary>
/// Representa la respuesta de una reserva en el sistema.
/// </summary>
public record ReservationResponse : ResponseMessage
{
    /// <summary>
    /// Obtiene o establece el identificador único de la reservación.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha de la reserva.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del usuario.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Obtiene o establece el apellido del usuario.
    /// </summary>
    public string? UserLastName { get; set; }

    /// <summary>
    /// Obtiene o establece el correo electrónico del usuario.
    /// </summary>
    public string? UserEmail { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del asiento.
    /// </summary>
    public string? SeatName { get; set; }

    /// <summary>
    /// Obtiene o establece la descripción del asiento.
    /// </summary>
    public string? SeatDescription { get; set; }

    /// <summary>
    /// Convierte implícitamente una instancia de <see cref="ReservationServiceResponse"/> a <see cref="ReservationResponse"/>.
    /// </summary>
    /// <param name="request">La respuesta de la reserva.</param>
    public static implicit operator ReservationResponse(ReservationServiceResponse request) =>
        new()
        {
            Id = request.Id,
            Date = request.Date,
            UserName = request.UserName,
            UserLastName = request.UserLastName,
            UserEmail = request.UserEmail,
            SeatName = request.SeatName,
            SeatDescription = request.SeatDescription,
        };
}
