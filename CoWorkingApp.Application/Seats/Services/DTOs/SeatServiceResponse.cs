using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Services.DTOs;

/// <summary>
/// Representa la respuesta de un servicio de asientos en el sistema.
/// </summary>
public record SeatServiceResponse : ResponseMessage
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
