using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Persistence.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio para la entidad <see cref="Reservation"/>.
/// </summary>
public class ReservationRepository : RepositoryGeneric<Reservation>, IReservationRepository
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ReservationRepository"/> utilizando el contexto de datos especificado.
    /// </summary>
    /// <param name="context">El contexto de datos de Entity Framework.</param>
    public ReservationRepository(DbContext context) : base(context) { }

    /// <summary>
    /// Obtiene todas las reservaciones de la entidad <see cref="Reservation"/> de manera asincrónica, incluyendo información detallada del usuario y del asiento asociados a cada reserva.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de todas las reservaciones de la entidad <see cref="Reservation"/> con información detallada del usuario y del asiento asociados a cada reserva.</returns>
    public override async Task<IEnumerable<Reservation>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
            .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Obtiene una reserva de la entidad <see cref="Reservation"/> por su identificador de manera asincrónica, incluyendo información detallada del usuario y del asiento asociados a la reserva.
    /// </summary>
    /// <param name="id">El identificador único de la reserva.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La reserva de la entidad <see cref="Reservation"/> con información detallada del usuario y del asiento asociados, o null si no se encuentra ninguna reserva con el ID especificado.</returns>
    public override async Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Set
            .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
            .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por ID de usuario de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="userId">ID del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al usuario con el ID especificado.</returns>
    public async Task<IEnumerable<Reservation>> GetByUserIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
            .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por ID de asiento de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="seatId">ID del asiento.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al asiento con el ID especificado.</returns>
    public async Task<IEnumerable<Reservation>> GetBySeatIdAsNoTrackingAsync(Guid seatId, CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
            .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
            .Where(r => r.SeatId == seatId)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por <see cref="Date"/> de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="date">Fecha de la reservación.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> realizadas en la fecha especificada.</returns>
    public async Task<IEnumerable<Reservation>> GetByDateAsNoTrackingAsync(Date date, CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
            .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
            .Where(r => r.Date == date)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por <see cref="Email"/> del usuario de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="email">Correo electrónico del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al usuario con el correo electrónico especificado.</returns>
    public async Task<IEnumerable<Reservation>> GetByUserEmailAsNoTrackingAsync(Email email, CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
            .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
            .Where(r => r.User.Credentials.Email == email) // Verifica que User y Email no sean null
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por <see cref="SeatName"/> de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="seatName">Nombre del asiento.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al asiento con el nombre especificado.</returns>
    public async Task<IEnumerable<Reservation>> GetBySeatNameAsNoTrackingAsync(SeatName seatName, CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .Include(r => r.User) // Incluir información detallada del usuario asociado a la reserva
            .Include(r => r.Seat) // Incluir información detallada del asiento asociado a la reserva
            .Where(r => r.Seat.Name == seatName)
            .ToListAsync(cancellationToken);

    // Implementa otros métodos específicos para ReservationRepository
}
