﻿using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Core.Contracts.Repositories;

/// <summary>
/// Interfaz para el repositorio de la entidad <see cref="Reservation"/> que extiende las operaciones básicas del repositorio genérico.
/// </summary>
public interface IReservationRepository : IRepository<Reservation>
{
    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por ID de usuario de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="userId">ID del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al usuario con el ID especificado.</returns>
    Task<IEnumerable<Reservation>> GetByUserIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por ID de asiento de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="seatId">ID del asiento.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al asiento con el ID especificado.</returns>
    Task<IEnumerable<Reservation>> GetBySeatIdAsNoTrackingAsync(Guid seatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por fecha de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="date">Fecha de la reservación.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> realizadas en la fecha especificada.</returns>
    Task<IEnumerable<Reservation>> GetByDateAsNoTrackingAsync(DateTime date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por correo electrónico del usuario de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="email">Correo electrónico del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al usuario con el correo electrónico especificado.</returns>
    Task<IEnumerable<Reservation>> GetByUserEmailAsNoTrackingAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene reservaciones de la entidad <see cref="Reservation"/> por nombre del asiento de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="seatName">Nombre del asiento.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de reservaciones de la entidad <see cref="Reservation"/> asociadas al asiento con el nombre especificado.</returns>
    Task<IEnumerable<Reservation>> GetBySeatNameAsNoTrackingAsync(string seatName, CancellationToken cancellationToken = default);

    // Otros métodos específicos para ReservationRepository
}
