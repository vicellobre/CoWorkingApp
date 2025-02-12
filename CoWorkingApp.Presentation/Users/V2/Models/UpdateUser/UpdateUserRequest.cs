using CoWorkingApp.Application.Users.Commands.UpdateUser;

namespace CoWorkingApp.Presentation.Users.V2.Models.UpdateUser;

/// <summary>
/// Representa una solicitud para actualizar un usuario existente.
/// </summary>
/// <param name="FirstName">El primer nombre del usuario.</param>
/// <param name="LastName">El apellido del usuario.</param>
/// <param name="Email">El correo electrónico del usuario.</param>
/// <param name="Password">La contraseña del usuario.</param>
public readonly record struct UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password)
{
    /// <summary>
    /// Convierte explícitamente una solicitud de actualización de usuario a un comando de actualización de usuario.
    /// <para>De <see cref="UpdateUserRequest"/> a <see cref="UpdateUserCommand"/></para>
    /// </summary>
    /// <param name="request">La solicitud de actualización de usuario.</param>
    /// <returns>Un nuevo <see cref="UpdateUserCommand"/> con los datos de la solicitud.</returns>
    public static explicit operator UpdateUserCommand(UpdateUserRequest request) =>
        new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

    /// <summary>
    /// Convierte la solicitud de actualización de usuario a un comando de actualización de usuario.
    /// </summary>
    /// <param name="userId">El ID del usuario.</param>
    /// <returns>Un nuevo <see cref="UpdateUserCommand"/> con los datos de la solicitud y el ID del usuario.</returns>
    public UpdateUserCommand ToUpdateUserCommand(Guid userId) =>
        new(
            userId,
            FirstName,
            LastName,
            Email,
            Password);
}
