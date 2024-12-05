using CoWorkingApp.Application.Reservations.Services.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs;

/// <summary>
/// Representa la solicitud de una reserva en el sistema.
/// </summary>
public record ReservationRequest : IRequest
{
    /// <summary>
    /// Obtiene o establece la fecha de la reserva.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador único del usuario asociado a la reserva.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador único del asiento asociado a la reserva.
    /// </summary>
    public Guid SeatId { get; set; }

    /// <summary>
    /// Convierte implícitamente una instancia de <see cref="ReservationRequest"/> a <see cref="ReservationServiceRequest"/>.
    /// </summary>
    /// <param name="request">La solicitud de reserva.</param>
    public static implicit operator ReservationServiceRequest(ReservationRequest request) =>
        new()
        {
            Date = request.Date,
            UserId = request.UserId,
            SeatId = request.SeatId
        };
}
