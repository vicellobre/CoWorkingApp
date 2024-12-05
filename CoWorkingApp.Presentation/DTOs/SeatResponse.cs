using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs;

/// <summary>
/// Representa la respuesta de un asiento en el sistema.
/// </summary>
public record SeatResponse : ResponseMessage
{
    /// <summary>
    /// Obtiene o establece el identificador único del asiento.
    /// </summary>
    public Guid Id { get; set; }

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
