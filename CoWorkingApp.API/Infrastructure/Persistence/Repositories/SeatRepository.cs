using CoWorkingApp.API.Infrastructure.UnitOfWorks;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.API.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementación del repositorio para la entidad Seat.
    /// </summary>
    public class SeatRepository : RepositoryGeneric<Seat>, ISeatRepository
    {
        /// <summary>
        /// Constructor de la clase SeatRepository.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo que gestiona las transacciones de la base de datos.</param>
        public SeatRepository(IUnitOfWork? unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Obtiene los asientos disponibles (no bloqueados) de manera asincrónica.
        /// </summary>
        /// <returns>Colección de asientos disponibles.</returns>
        public async Task<IEnumerable<Seat>> GetAvailablesAsync()
        {
            // Filtra los asientos que no están bloqueados
            return await _dbSet
                .Where(s => !s.IsBlocked)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Busca el primer asiento que coincida con el nombre especificado de manera asincrónica.
        /// </summary>
        /// <param name="name">Nombre del asiento.</param>
        /// <returns>Asiento correspondiente al nombre especificado o null si no se encuentra.</returns>
        public async Task<Seat?> GetByNameAsync(string name)
        {
            // Busca el primer asiento que coincida con el nombre
            return await _dbSet.FirstOrDefaultAsync(s => s != null && s.Name != null && s.Name.Equals(name));
        }

        // Puedes implementar otros métodos específicos para SeatRepository si es necesario
    }
}
