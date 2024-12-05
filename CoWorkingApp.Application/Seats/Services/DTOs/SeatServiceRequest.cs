using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Services.DTOs;

/// <summary>
/// Representa la solicitud a un servicio de asientos en el sistema.
/// </summary>
public record SeatServiceRequest : IRequest
{
    /// <summary>
    /// Obtiene o establece el nombre del asiento.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que indica si el asiento está bloqueado.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Obtiene o establece la descripción del asiento.
    /// </summary>
    public string? Description { get; set; }
}
