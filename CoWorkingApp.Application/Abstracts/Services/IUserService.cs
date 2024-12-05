using CoWorkingApp.Application.DTOs;

namespace CoWorkingApp.Application.Abstracts.Services
{
    /// <summary>
    /// Interfaz que define los métodos específicos para el servicio de usuarios.
    /// </summary>
    public interface IUserService : IService<UserRequest, UserResponse>
    {
        /// <summary>
        /// Obtiene una entidad <see cref="UserResponse"/> por su dirección de correo electrónico de manera asincrónica.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico del usuario.</param>
        /// <returns>Un objeto de tipo <see cref="UserResponse"/> correspondiente a la dirección de correo electrónico especificada.</returns>
        Task<UserResponse> GetByEmailAsync(string email);

        /// <summary>
        /// Autentica una entidad <see cref="UserResponse"/> por su dirección de correo electrónico y contraseña de manera asincrónica.
        /// </summary>
        /// <param name="request">Solicitud de autenticación del usuario que contiene la dirección de correo electrónico y la contraseña.</param>
        /// <returns>Un objeto de tipo <see cref="UserResponse"/> correspondiente a las credenciales especificadas.</returns>
        Task<UserResponse> AuthenticateAsync(UserRequest request);

        // Otros métodos específicos para IUserService
    }
}
