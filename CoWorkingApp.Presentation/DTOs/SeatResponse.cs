using CoWorkingApp.Application.Seats.Services.DTOs;
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

    /// <summary>
    /// Convierte implícitamente una instancia de <see cref="SeatServiceResponse"/> a <see cref="SeatResponse"/>.
    /// </summary>
    /// <param name="request">La respuesta del servicio de asiento.</param>
    public static implicit operator SeatResponse(SeatServiceResponse request) =>
        new()
        {
            Id = request.Id,
            Name = request.Name,
            IsBlocked = request.IsBlocked,
            Description = request.Description,
        };
}
