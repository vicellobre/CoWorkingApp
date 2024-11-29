using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Application.Contracts.Services;

/// <summary>
/// Interfaz para servicios que manejan operaciones específicas para la entidad <see cref="Reservation"/>.
/// </summary>
public interface IReservationService : IService<ReservationRequest, ReservationResponse>
{
    /// <summary>
    /// Obtiene las entidades <see cref="ReservationResponse"/> para una fecha específica de manera asincrónica.
    /// </summary>
    /// <param name="date">Fecha para la cual se buscan las reservaciones.</param>
    /// <returns>Una colección de entidades <see cref="ReservationResponse"/> correspondientes a la fecha especificada.</returns>
    Task<IEnumerable<ReservationResponse>> GetByDateAsync(DateTime date);

    /// <summary>
    /// Obtiene las entidades <see cref="ReservationResponse"/> asociadas a un usuario por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="userId">Identificador único del usuario.</param>
    /// <returns>Una colección de entidades <see cref="ReservationResponse"/> asociadas al ID de usuario especificado.</returns>
    Task<IEnumerable<ReservationResponse>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Obtiene las entidades <see cref="ReservationResponse"/> asociadas a un asiento por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="seatId">Identificador único del asiento.</param>
    /// <returns>Una colección de entidades <see cref="ReservationResponse"/> asociadas al ID de asiento especificado.</returns>
    Task<IEnumerable<ReservationResponse>> GetBySeatIdAsync(Guid seatId);

    // Otros métodos específicos para IReservationService
}
