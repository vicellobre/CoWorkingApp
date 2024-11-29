﻿using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad <see cref="User"/>.
/// </summary>
public class UserRepository : RepositoryGeneric<User>, IUserRepository
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserRepository"/> utilizando el contexto de datos especificado.
    /// </summary>
    /// <param name="context">El contexto de datos de Entity Framework.</param>
    public UserRepository(DbContext context) : base(context) { }

    /// <summary>
    /// Obtiene una entidad <see cref="User"/> por su dirección de correo electrónico de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> correspondiente a la dirección de correo electrónico especificada o null si no se encuentra.</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        // Busca el primer usuario que coincida con el correo electrónico especificado, manejando el caso de null
        await Set
            .FirstOrDefaultAsync(u => u.Email != null && u.Email.Equals(email, StringComparison.OrdinalIgnoreCase),
                cancellationToken);

    /// <summary>
    /// Autentica una entidad <see cref="User"/> por su dirección de correo electrónico y contraseña de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="password">Contraseña del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> autenticada correspondiente a las credenciales especificadas o null si la autenticación falla.</returns>
    public async Task<User?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default) =>
        // Busca un usuario que coincida con el correo electrónico y la contraseña proporcionados,
        // manejando el caso de posibles valores null en Email y Password
        await Set.FirstOrDefaultAsync(u =>
            u.Email != null && u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            u.Password != null && u.Password.Equals(password, StringComparison.OrdinalIgnoreCase),
            cancellationToken);

    // Puedes implementar otros métodos específicos para UserRepository si es necesario
}