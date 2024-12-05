using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Services.DTOs;

/// <summary>
/// Representa una solicitud a un servicio de usuarios en el sistema.
/// </summary>
public record UserServiceRequest : IRequest
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
}
