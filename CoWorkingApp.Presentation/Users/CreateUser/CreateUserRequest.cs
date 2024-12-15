using CoWorkingApp.Application.Users.Commands.CreateUser;

namespace CoWorkingApp.Presentation.Users.CreateUser
{
    /// <summary>
    /// Representa una solicitud para crear un nuevo usuario.
    /// </summary>
    /// <param name="FirstName">El primer nombre del usuario.</param>
    /// <param name="LastName">El apellido del usuario.</param>
    /// <param name="Email">El correo electrónico del usuario.</param>
    /// <param name="Password">La contraseña del usuario.</param>
    public readonly record struct CreateUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password)
    {
        /// <summary>
        /// Convierte explícitamente una solicitud de creación de usuario a un comando de creación de usuario.
        /// <para>De <see cref="CreateUserRequest"/> a <see cref="CreateUserCommand"/></para>
        /// </summary>
        /// <param name="request">La solicitud de creación de usuario.</param>
        /// <returns>Un nuevo <see cref="CreateUserCommand"/> con los datos de la solicitud.</returns>
        public static explicit operator CreateUserCommand(CreateUserRequest request) =>
            new(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);
    }
}
