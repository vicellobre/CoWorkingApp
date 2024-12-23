﻿using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs.Users;

/// <summary>
/// Representa una solicitud de un usuario en el sistema.
/// </summary>
public record UserRequest : IRequest
{
    /// <summary>
    /// Obtiene o establece el nombre del usuario.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Obtiene o establece el apellido del usuario.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Obtiene o establece el correo electrónico del usuario.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Obtiene o establece la contraseña del usuario.
    /// </summary>
    public string? Password { get; set; }
};