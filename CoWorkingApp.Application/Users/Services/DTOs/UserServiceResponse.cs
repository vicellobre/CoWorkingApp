using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Services.DTOs;

/// <summary>
/// Representa la respuesta de un servicio de usuarios en el sistema.
/// </summary>
public record UserServiceResponse : ResponseMessage
{
    /// <summary>
    /// Obtiene o establece el identificador único del usuario.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del usuario.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Obtiene o establece el apellido del usuario.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Obtiene o establece el correo electrónico del usuario.
    /// </summary>
    public string? Email { get; set; }
}
