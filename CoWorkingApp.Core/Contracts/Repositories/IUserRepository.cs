﻿using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Core.Contracts.Repositories;

/// <summary>
/// Interfaz para el repositorio de la entidad <see cref="User"/> que extiende las operaciones básicas del repositorio genérico.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Obtiene una entidad <see cref="User"/> por su dirección de correo electrónico de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> encontrada con la dirección de correo electrónico especificada, o null si no se encuentra.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Autentica una entidad <see cref="User"/> por su dirección de correo electrónico y contraseña de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="password">Contraseña del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> autenticada si las credenciales son válidas, o null si las credenciales son incorrectas.</returns>
    Task<User?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);

    // Otros métodos específicos para UserRepository
}
