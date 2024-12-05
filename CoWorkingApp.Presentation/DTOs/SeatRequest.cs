using CoWorkingApp.Application.Seats.Services.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs;

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

    /// <summary>
    /// Convierte implícitamente una instancia de <see cref="SeatRequest"/> a <see cref="SeatServiceRequest"/>.
    /// </summary>
    /// <param name="request">La solicitud de asiento.</param>
    public static implicit operator SeatServiceRequest(SeatRequest request) =>
        new()
        {
            Name = request.Name,
            Description = request.Description,
            IsBlocked = request.IsBlocked,
        };
}
