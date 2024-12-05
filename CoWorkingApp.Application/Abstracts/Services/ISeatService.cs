using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Application.Abstracts.Services;

/// <summary>
/// Interfaz para servicios que manejan operaciones específicas para la entidad <see cref="Seat"/>.
/// </summary>
public interface ISeatService : IService<SeatRequest, SeatResponse>
{
    /// <summary>
    /// Obtiene las entidades <see cref="SeatResponse"/> disponibles de manera asincrónica.
    /// </summary>
    /// <returns>Una colección de entidades <see cref="SeatResponse"/> disponibles.</returns>
    Task<IEnumerable<SeatResponse>> GetAvailablesAsync();

    /// <summary>
    /// Obtiene una entidad <see cref="SeatResponse"/> por su nombre de manera asincrónica.
    /// </summary>
    /// <param name="name">Nombre de la entidad <see cref="Seat"/> a buscar.</param>
    /// <returns>Una entidad <see cref="SeatResponse"/> correspondiente al nombre especificado.</returns>
    Task<SeatResponse> GetByNameAsync(string name);

    // Otros métodos específicos para ISeatService
}
