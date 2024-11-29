using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Application.Contracts.Services;

/// <summary>
/// Interfaz para servicios que manejan operaciones específicas para la entidad Seat.
/// </summary>
public interface ISeatService : IService<SeatRequest, SeatResponse>
{
    /// <summary>
    /// Obtiene los asientos disponibles de manera asincrónica.
    /// </summary>
    /// <returns>Colección de asientos disponibles (DTO).</returns>
    Task<IEnumerable<SeatResponse>> GetAvailablesAsync();

    /// <summary>
    /// Obtiene el asiento por su nombre de manera asincrónica.
    /// </summary>
    /// <param name="name">Nombre del asiento a buscar.</param>
    /// <returns>Asiento correspondiente al nombre especificado (DTO).</returns>
    Task<SeatResponse> GetByNameAsync(string name);

    // Otros métodos específicos para ISeatService
}
