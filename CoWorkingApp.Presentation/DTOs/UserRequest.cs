using CoWorkingApp.Application.Users.Services.DTOs;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs;

/// <summary>
/// Representa una solicitud para crear o actualizar un usuario en el sistema.
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

    /// <summary>
    /// Convierte implícitamente una instancia de <see cref="UserRequest"/> a <see cref="UserServiceRequest"/>.
    /// </summary>
    /// <param name="request">La solicitud del usuario.</param>
    public static implicit operator UserServiceRequest(UserRequest request) =>
        new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
        };
}
