using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs.Seats;

/// <summary>
/// Representa la solicitud de un asiento en el sistema.
/// </summary>
public record SeatRequest : IRequest
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