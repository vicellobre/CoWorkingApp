using CoWorkingApp.Core.Domain.DTOs;

namespace CoWorkingApp.Core.Application.Contracts.Services
{
    /// <summary>
    /// Interfaz para servicios que manejan operaciones específicas para la entidad Reservation.
    /// </summary>
    public interface IReservationService : IService<ReservationRequest, ReservationResponse>
    {
        /// <summary>
        /// Obtiene las reservaciones para una fecha específica de manera asincrónica.
        /// </summary>
        /// <param name="date">Fecha para la cual se buscan las reservaciones.</param>
        /// <returns>Colección de reservaciones correspondientes a la fecha especificada (DTO).</returns>
        Task<IEnumerable<ReservationResponse>> GetByDateAsync(DateTime date);

        /// <summary>
        /// Obtiene las reservaciones asociadas a un usuario por su identificador de manera asincrónica.
        /// </summary>
        /// <param name="userId">Identificador único del usuario.</param>
        /// <returns>Colección de reservaciones asociadas al ID de usuario especificado (DTO).</returns>
        Task<IEnumerable<ReservationResponse>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Obtiene las reservaciones asociadas a un asiento por su identificador de manera asincrónica.
        /// </summary>
        /// <param name="seatId">Identificador único del asiento.</param>
        /// <returns>Colección de reservaciones asociadas al ID de asiento especificado (DTO).</returns>
        Task<IEnumerable<ReservationResponse>> GetBySeatIdAsync(Guid seatId);

        // Otros métodos específicos para IReservationService
    }
}
