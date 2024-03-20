using CoWorkingApp.Core.Domain.Entities;

namespace CoWorkingApp.Core.Application.Contracts.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de la entidad Reservation que extiende las operaciones básicas del repositorio genérico.
    /// </summary>
    public interface IReservationRepository : IRepository<Reservation>
    {
        /// <summary>
        /// Obtiene reservaciones por ID de usuario de manera asincrónica.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>Una colección de reservaciones asociadas al usuario con el ID especificado.</returns>
        Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Obtiene reservaciones por ID de asiento de manera asincrónica.
        /// </summary>
        /// <param name="seatId">ID del asiento.</param>
        /// <returns>Una colección de reservaciones asociadas al asiento con el ID especificado.</returns>
        Task<IEnumerable<Reservation>> GetBySeatIdAsync(Guid seatId);

        /// <summary>
        /// Obtiene reservaciones por fecha de manera asincrónica.
        /// </summary>
        /// <param name="date">Fecha de la reservación.</param>
        /// <returns>Una colección de reservaciones realizadas en la fecha especificada.</returns>
        Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date);

        /// <summary>
        /// Obtiene reservaciones por correo electrónico del usuario de manera asincrónica.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Una colección de reservaciones asociadas al usuario con el correo electrónico especificado.</returns>
        Task<IEnumerable<Reservation>> GetByUserEmailAsync(string email);

        /// <summary>
        /// Obtiene reservaciones por nombre del asiento de manera asincrónica.
        /// </summary>
        /// <param name="seatName">Nombre del asiento.</param>
        /// <returns>Una colección de reservaciones asociadas al asiento con el nombre especificado.</returns>
        Task<IEnumerable<Reservation>> GetBySeatNameAsync(string seatName);

        // Otros métodos específicos para ReservationRepository
    }
}
