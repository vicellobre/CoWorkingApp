using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Persistence.Abstracts;
using CoWorkingApp.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad User.
/// </summary>
public class UserRepository : RepositoryGeneric<User>, IUserRepository
{
    /// <summary>
    /// Constructor de la clase UserRepository.
    /// </summary>
    /// <param name="unitOfWork">Unidad de trabajo que gestiona las transacciones de la base de datos.</param>
    public UserRepository(IUnitOfWork? unitOfWork) : base(unitOfWork) { }

    /// <summary>
    /// Obtiene un usuario por su dirección de correo electrónico de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <returns>Usuario correspondiente a la dirección de correo electrónico especificada o null si no se encuentra.</returns>
    public async Task<User?> GetByEmailAsync(string email)
    {
        // Busca el primer usuario que coincida con el correo electrónico especificado, manejando el caso de null
        return await _dbSet.FirstOrDefaultAsync(u => u.Email != null && u.Email.Equals(email));
    }

    /// <summary>
    /// Autentica a un usuario por su dirección de correo electrónico y contraseña de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="password">Contraseña del usuario.</param>
    /// <returns>Usuario autenticado correspondiente a las credenciales especificadas o null si la autenticación falla.</returns>
    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        // Busca un usuario que coincida con el correo electrónico y la contraseña proporcionados,
        // manejando el caso de posibles valores null en Email y Password
        return await _dbSet.FirstOrDefaultAsync(u =>
            u.Email != null && u.Email.Equals(email) &&
            u.Password != null && u.Password.Equals(password));
    }

    // Puedes implementar otros métodos específicos para UserRepository si es necesario
}