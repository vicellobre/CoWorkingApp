using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Services.DTOs;

/// <summary>
/// Representa la solicitud a un servicio de reservas en el sistema.
/// </summary>
public record ReservationServiceRequest : IRequest
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
