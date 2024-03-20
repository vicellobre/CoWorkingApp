using CoWorkingApp.Core.Domain.DTOs;

namespace CoWorkingApp.Core.Application.Contracts.Services
{
    /// <summary>
    /// Interfaz que define los métodos específicos para el servicio de usuarios.
    /// </summary>
    public interface IUserService : IService<UserRequest, UserResponse>
    {
        /// <summary>
        /// Obtiene un usuario por su dirección de correo electrónico de manera asincrónica.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico del usuario.</param>
        /// <returns>DTO del usuario correspondiente a la dirección de correo electrónico especificada (DTO).</returns>
        Task<UserResponse> GetByEmailAsync(string email);

        /// <summary>
        /// Autentica a un usuario por su dirección de correo electrónico y contraseña de manera asincrónica.
        /// </summary>
        /// <param name="request">Solicitud de autenticación del usuario que contiene la dirección de correo electrónico y la contraseña.</param>
        /// <returns>DTO del usuario autenticado correspondiente a las credenciales especificadas (DTO).</returns>
        Task<UserResponse> AuthenticateAsync(UserRequest request);

        // Otros métodos específicos para IUserService
    }
}
