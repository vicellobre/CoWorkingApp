using CoWorkingApp.Application.Reservations.Services.DTOs;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Application.Reservations.Services.Contracts;

/// <summary>
/// Interfaz para servicios que manejan operaciones específicas para la entidad <see cref="Reservation"/>.
/// </summary>
public interface IReservationService : IEntityService<ReservationServiceRequest, ReservationServiceResponse>
{
    /// <summary>
    /// Obtiene las entidades <see cref="ReservationServiceResponse"/> para una fecha específica de manera asincrónica.
    /// </summary>
    /// <param name="date">Fecha para la cual se buscan las reservaciones.</param>
    /// <returns>Una colección de entidades <see cref="ReservationServiceResponse"/> correspondientes a la fecha especificada.</returns>
    Task<IEnumerable<ReservationServiceResponse>> GetByDateAsync(DateTime date);

    /// <summary>
    /// Obtiene las entidades <see cref="ReservationServiceResponse"/> asociadas a un usuario por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="userId">Identificador único del usuario.</param>
    /// <returns>Una colección de entidades <see cref="ReservationServiceResponse"/> asociadas al ID de usuario especificado.</returns>
    Task<IEnumerable<ReservationServiceResponse>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Obtiene las entidades <see cref="ReservationServiceResponse"/> asociadas a un asiento por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="seatId">Identificador único del asiento.</param>
    /// <returns>Una colección de entidades <see cref="ReservationServiceResponse"/> asociadas al ID de asiento especificado.</returns>
    Task<IEnumerable<ReservationServiceResponse>> GetBySeatIdAsync(Guid seatId);

    // Otros métodos específicos para IReservationService
}
