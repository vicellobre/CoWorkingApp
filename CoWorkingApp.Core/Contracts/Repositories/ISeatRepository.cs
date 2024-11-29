﻿using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Core.Contracts.Repositories;

/// <summary>
/// Interfaz para el repositorio de la entidad <see cref="Seat"/> que extiende las operaciones básicas del repositorio genérico.
/// </summary>
public interface ISeatRepository : IRepository<Seat>
{
    /// <summary>
    /// Obtiene las entidades <see cref="Seat"/> disponibles de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de entidades <see cref="Seat"/> disponibles.</returns>
    Task<IEnumerable<Seat>> GetAvailablesAsNoTrackingAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una entidad <see cref="Seat"/> por su nombre de manera asincrónica.
    /// </summary>
    /// <param name="name">Nombre de la entidad <see cref="Seat"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="Seat"/> con el nombre especificado, o null si no se encuentra.</returns>
    Task<Seat?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    // Otros métodos específicos para SeatRepository
}