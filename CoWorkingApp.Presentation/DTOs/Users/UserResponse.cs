using CoWorkingApp.Application.Users.Commands.AuthenticateUser;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Presentation.DTOs.Users;

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
    /// 
    /// </summary>
    /// <param name="response"></param>

    public static explicit operator UserResponse(AuthenticateUserCommandResponse response) =>
        new()
        {
            Id = response.UserId,
            FirstName = response.FirstName,
            LastName = response.LastName,
            Email = response.Email
        };
}
