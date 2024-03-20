using CoWorkingApp.Core.Domain.Entities;

namespace CoWorkingApp.Core.Application.Contracts.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de la entidad User que extiende las operaciones básicas del repositorio genérico.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Obtiene un usuario por su dirección de correo electrónico de manera asincrónica.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico del usuario.</param>
        /// <returns>El usuario encontrado con la dirección de correo electrónico especificada, o null si no se encuentra.</returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Autentica a un usuario por su dirección de correo electrónico y contraseña de manera asincrónica.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>El usuario autenticado si las credenciales son válidas, o null si las credenciales son incorrectas.</returns>
        Task<User> AuthenticateAsync(string email, string password);

        // Otros métodos específicos para UserRepository
    }
}
