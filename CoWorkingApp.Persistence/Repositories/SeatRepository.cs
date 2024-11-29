﻿using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad <see cref="Seat"/>.
/// </summary>
public class SeatRepository : RepositoryGeneric<Seat>, ISeatRepository
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SeatRepository"/> utilizando el contexto de datos especificado.
    /// </summary>
    /// <param name="context">El contexto de datos de Entity Framework.</param>
    public SeatRepository(DbContext context) : base(context) { }

    /// <summary>
    /// Obtiene las entidades <see cref="Seat"/> disponibles (no bloqueadas) de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de entidades <see cref="Seat"/> disponibles.</returns>
    public async Task<IEnumerable<Seat>> GetAvailablesAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
        // Filtra los asientos que no están bloqueados
        await Set
            .AsNoTracking()
            .Where(s => !s.IsBlocked)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Busca la primera entidad <see cref="Seat"/> que coincida con el nombre especificado de manera asincrónica.
    /// </summary>
    /// <param name="name">Nombre de la entidad <see cref="Seat"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="Seat"/> correspondiente al nombre especificado o null si no se encuentra.</returns>
    public async Task<Seat?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        // Busca el primer asiento que coincida con el nombre
        await Set
            .FirstOrDefaultAsync(s => s != null && s.Name != null && s.Name.Equals(name), cancellationToken);

    // Puedes implementar otros métodos específicos para SeatRepository si es necesario
}