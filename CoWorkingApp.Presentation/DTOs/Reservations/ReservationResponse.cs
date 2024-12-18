﻿using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs.Reservations;

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
}
