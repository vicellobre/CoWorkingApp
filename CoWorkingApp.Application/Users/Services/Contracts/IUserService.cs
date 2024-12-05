using CoWorkingApp.Application.Users.Services.DTOs;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Application.Users.Services.Contracts
{
    /// <summary>
    /// Interfaz que define los métodos específicos para el servicio de usuarios.
    /// </summary>
    public interface IUserService : IEntityService<UserServiceRequest, UserServiceResponse>
    {
        /// <summary>
        /// Obtiene una entidad <see cref="UserServiceResponse"/> por su dirección de correo electrónico de manera asincrónica.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico del usuario.</param>
        /// <returns>Un objeto de tipo <see cref="UserServiceResponse"/> correspondiente a la dirección de correo electrónico especificada.</returns>
        Task<UserServiceResponse> GetByEmailAsync(string email);

        /// <summary>
        /// Autentica una entidad <see cref="UserServiceResponse"/> por su dirección de correo electrónico y contraseña de manera asincrónica.
        /// </summary>
        /// <param name="request">Solicitud de autenticación del usuario que contiene la dirección de correo electrónico y la contraseña.</param>
        /// <returns>Un objeto de tipo <see cref="UserServiceResponse"/> correspondiente a las credenciales especificadas.</returns>
        Task<UserServiceResponse> AuthenticateAsync(UserServiceRequest request);

        // Otros métodos específicos para IUserService
    }
}
