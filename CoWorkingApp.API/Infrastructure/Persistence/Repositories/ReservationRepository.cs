using CoWorkingApp.API.Infrastructure.UnitOfWorks;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.API.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementación del repositorio para la entidad Reservation.
    /// </summary>
    public class ReservationRepository : RepositoryGeneric<Reservation>, IReservationRepository
    {
        /// <summary>
        /// Constructor de la clase ReservationRepository.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo que gestiona las transacciones de la base de datos.</param>
        public ReservationRepository(IUnitOfWork? unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Obtiene todas las reservaciones de manera asincrónica, incluyendo información detallada del usuario y del asiento asociados a cada reserva.
        /// </summary>
        /// <returns>Una colección de todas las reservaciones con información detallada del usuario y del asiento asociados a cada reserva.</returns>
        public override async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            // Incluye información detallada del usuario y del asiento asociados a cada reserva
            return await _dbSet
                .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
                .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una reserva por su identificador de manera asincrónica, incluyendo información detallada del usuario y del asiento asociados a la reserva.
        /// </summary>
        /// <param name="id">El identificador único de la reserva.</param>
        /// <returns>La reserva con información detallada del usuario y del asiento asociados, o null si no se encuentra ninguna reserva con el ID especificado.</returns>
        public override async Task<Reservation?> GetByIdAsync(Guid id)
        {
            // Incluye información detallada del usuario y del asiento asociados a cada reserva
            return await _dbSet
                .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
                .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <summary>
        /// Obtiene las reservas por ID de usuario de manera asincrónica.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>Colección de reservas correspondientes al ID de usuario especificado.</returns>
        public async Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId)
        {
            // Incluye información detallada del usuario y del asiento asociados a cada reserva
            return await _dbSet
                .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
                .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
                .Where(r => r.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene las reservas por ID de plaza de manera asincrónica.
        /// </summary>
        /// <param name="seatId">ID de la plaza.</param>
        /// <returns>Colección de reservas correspondientes al ID de plaza especificado.</returns>
        public async Task<IEnumerable<Reservation>> GetBySeatIdAsync(Guid seatId)
        {
            // Incluye información detallada del usuario y del asiento asociados a cada reserva
            return await _dbSet
                .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
                .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
                .Where(r => r.SeatId == seatId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene las reservas por fecha de manera asincrónica.
        /// </summary>
        /// <param name="date">Fecha de las reservas.</param>
        /// <returns>Colección de reservas correspondientes a la fecha especificada.</returns>
        public async Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date)
        {
            // Incluye información detallada del usuario y del asiento asociados a cada reserva
            return await _dbSet
                .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
                .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
                .Where(r => r.Date == date)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene reservaciones por correo electrónico del usuario de manera asincrónica.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Una colección de reservaciones asociadas al usuario con el correo electrónico especificado.</returns>
        public async Task<IEnumerable<Reservation>> GetByUserEmailAsync(string? email)
        {
            // Verifica que el email no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(email))
            {
                return []; // O puedes lanzar una excepción según tus requisitos
            }

            // Incluye información detallada del usuario y del asiento asociados a cada reserva
            return await _dbSet
                .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
                .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
                .Where(r => r.User != null && r.User.Email != null && r.User.Email.Equals(email)) // Verifica que User y Email no sean null
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una colección de reservaciones asociadas a un nombre de asiento específico de manera asincrónica, incluyendo información detallada del usuario y del asiento asociados a cada reserva.
        /// </summary>
        /// <param name="seatName">El nombre del asiento del que se desean obtener las reservaciones.</param>
        /// <returns>Una colección de reservaciones asociadas al nombre de asiento especificado, incluyendo información detallada del usuario y del asiento asociados a cada reserva.</returns>
        public async Task<IEnumerable<Reservation>> GetBySeatNameAsync(string? seatName)
        {
            // Incluye información detallada del usuario y del asiento asociados a cada reserva
            return await _dbSet
                .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
                .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
                .Where(r => r.Seat != null && r.Seat.Name != null && r.Seat.Name.Equals(seatName))
                .AsNoTracking()
                .ToListAsync();
        }

        // Implementa otros métodos específicos para ReservationRepository
    }
}
