using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.DTOs;

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
}
