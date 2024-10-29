using CoWorkingApp.Core.Domain.Entities;

namespace CoWorkingApp.Core.Application.Contracts.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de la entidad Seat que extiende las operaciones básicas del repositorio genérico.
    /// </summary>
    public interface ISeatRepository : IRepository<Seat>
    {
        /// <summary>
        /// Obtiene los asientos disponibles de manera asincrónica.
        /// </summary>
        /// <returns>Una colección de asientos disponibles.</returns>
        Task<IEnumerable<Seat>> GetAvailablesAsync();

        /// <summary>
        /// Obtiene el asiento por su nombre de manera asincrónica.
        /// </summary>
        /// <param name="name">Nombre del asiento.</param>
        /// <returns>El asiento con el nombre especificado, o null si no se encuentra.</returns>
        Task<Seat?> GetByNameAsync(string name);

        // Otros métodos específicos para SeatRepository
    }
}
