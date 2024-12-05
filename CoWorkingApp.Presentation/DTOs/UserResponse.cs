using CoWorkingApp.Application.Users.Services.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs;

/// <summary>
/// Representa la respuesta de un usuario en el sistema.
/// </summary>
public record UserResponse : ResponseMessage
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

    /// <summary>
    /// Convierte implícitamente una instancia de <see cref="UserServiceResponse"/> a <see cref="UserResponse"/>.
    /// </summary>
    /// <param name="request">La respuesta del servicio de usuario.</param>
    public static implicit operator UserResponse(UserServiceResponse request) =>
        new()
        {
            Id = request.Id,
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
        };
}
