using CoWorkingApp.Application.Seats.Services.DTOs;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Application.Seats.Services.Contracts;

/// <summary>
/// Interfaz para servicios que manejan operaciones específicas para la entidad <see cref="Seat"/>.
/// </summary>
public interface ISeatService : IEntityService<SeatServiceRequest, SeatServiceResponse>
{
    /// <summary>
    /// Obtiene las entidades <see cref="SeatServiceResponse"/> disponibles de manera asincrónica.
    /// </summary>
    /// <returns>Una colección de entidades <see cref="SeatServiceResponse"/> disponibles.</returns>
    Task<IEnumerable<SeatServiceResponse>> GetAvailablesAsync();

    /// <summary>
    /// Obtiene una entidad <see cref="SeatServiceResponse"/> por su nombre de manera asincrónica.
    /// </summary>
    /// <param name="name">Nombre de la entidad <see cref="Seat"/> a buscar.</param>
    /// <returns>Una entidad <see cref="SeatServiceResponse"/> correspondiente al nombre especificado.</returns>
    Task<SeatServiceResponse> GetByNameAsync(string name);

    // Otros métodos específicos para ISeatService
}
