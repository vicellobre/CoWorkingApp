using CoWorkingApp.Application.Abstracts.Services;
using CoWorkingApp.Application.Contracts.Adapters;
using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Infrastructure.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Infrastructure.Services;

/// <summary>
/// Implementación concreta del servicio para la entidad <see cref="Reservation"/>.
/// </summary>
public class ReservationService : ServiceGeneric<IReservationRepository, Reservation, ReservationRequest, ReservationResponse>, IReservationService
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ReservationService"/> utilizando las dependencias necesarias.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo para manejar transacciones.</param>
    /// <param name="reservationRepository">El repositorio de reservaciones asociado al servicio.</param>
    /// <param name="mapper">El adaptador de mapeo para convertir entre entidades y DTOs.</param>
    public ReservationService(IUnitOfWork? unitOfWork, IReservationRepository? reservationRepository, IMapperAdapter? mapper) : base(unitOfWork, reservationRepository, mapper) { }

    // Implementación de métodos específicos de IReservationService

    /// <summary>
    /// Crea una nueva entidad <see cref="Reservation"/> de manera asincrónica.
    /// </summary>
    /// <param name="request">El objeto de solicitud de tipo <see cref="ReservationRequest"/>.</param>
    /// <returns>Un objeto de respuesta de tipo <see cref="ReservationResponse"/> que representa la reservación creada.</returns>
    public override async Task<ReservationResponse> CreateAsync(ReservationRequest? request)
    {
        try
        {
            // Verificar si la solicitud es nula
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request), "The request object cannot be null");
            }

            // Mapear la solicitud a una reservación
            var reservation = _mapper.Map<ReservationRequest, Reservation>(request);

            // Validar la reservación
            if (!IsValid(reservation))
            {
                throw new ValidationException("Argument is invalid.");
            }

            // Agregar la reservación al repositorio
            _repository.Add(reservation);
            await _unitOfWork.CommitAsync();

            // Obtener la información completa sobre la reservación
            var responseComplete = await _repository.GetByIdAsNoTrackingAsync(reservation.Id)
                ?? throw new ArgumentException("The request contains inconsistent details");

            // Mapear la reservación a un objeto de respuesta y retornarlo
            var response = _mapper.Map<Reservation, ReservationResponse>(responseComplete);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    /// <summary>
    /// Obtiene las entidades <see cref="Reservation"/> realizadas en una fecha específica de manera asincrónica.
    /// </summary>
    /// <param name="date">La fecha para la cual se desean obtener las reservaciones.</param>
    /// <returns>Una colección de respuestas de tipo <see cref="ReservationResponse"/> que representan las reservaciones realizadas en la fecha especificada.</returns>
    public async Task<IEnumerable<ReservationResponse>> GetByDateAsync(DateTime date)
    {
        try
        {
            var dateResult = Date.Create(date);
            // Obtener las reservaciones por fecha del repositorio
            var reservations = await _repository.GetByDateAsNoTrackingAsync(dateResult.Value);

            // Mapear las reservaciones a ReservationResponse y convertirlas en una lista
            var response = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations).ToList();

            // Marcar todas las respuestas como exitosas
            response.ForEach(r => r.Success = true);

            return response;
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción y devolver una lista con una sola respuesta de error
            return [HandleException(ex)];
        }
    }

    /// <summary>
    /// Obtiene las entidades <see cref="Reservation"/> realizadas por un usuario específico de manera asincrónica.
    /// </summary>
    /// <param name="userId">El ID del usuario para el cual se desean obtener las reservaciones.</param>
    /// <returns>Una colección de respuestas de tipo <see cref="ReservationResponse"/> que representan las reservaciones realizadas por el usuario con el ID especificado.</returns>
    public async Task<IEnumerable<ReservationResponse>> GetByUserIdAsync(Guid userId)
    {
        try
        {
            // Obtener las reservaciones por ID de usuario del repositorio
            var reservations = await _repository.GetByUserIdAsNoTrackingAsync(userId);

            // Mapear las reservaciones a ReservationResponse y convertirlas en una lista
            var response = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations).ToList();

            // Marcar todas las respuestas como exitosas
            response.ForEach(r => r.Success = true);

            return response;
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción y devolver una lista con una sola respuesta de error
            return [HandleException(ex)];
        }
    }

    /// <summary>
    /// Obtiene las entidades <see cref="Reservation"/> asociadas a un asiento específico de manera asincrónica.
    /// </summary>
    /// <param name="seatId">El ID del asiento para el cual se desean obtener las reservaciones.</param>
    /// <returns>Una colección de respuestas de tipo <see cref="ReservationResponse"/> que representan las reservaciones asociadas al asiento con el ID especificado.</returns>
    public async Task<IEnumerable<ReservationResponse>> GetBySeatIdAsync(Guid seatId)
    {
        try
        {
            // Obtener las reservaciones por ID de asiento del repositorio
            var reservations = await _repository.GetBySeatIdAsNoTrackingAsync(seatId);

            // Mapear las reservaciones a ReservationResponse y convertirlas en una lista
            var response = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations).ToList();

            // Marcar todas las respuestas como exitosas
            response.ForEach(r => r.Success = true);

            return response;
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción y devolver una lista con una sola respuesta de error
            return [HandleException(ex)];
        }
    }

    /// <summary>
    /// Actualiza una entidad <see cref="Reservation"/> existente de manera asincrónica.
    /// </summary>
    /// <param name="id">El ID de la reservación.</param>
    /// <param name="request">El objeto de solicitud de tipo <see cref="ReservationRequest"/>.</param>
    /// <returns>Un objeto de respuesta de tipo <see cref="ReservationResponse"/> que representa la reservación actualizada.</returns>
    public override async Task<ReservationResponse> UpdateAsync(Guid id, ReservationRequest? request)
    {
        try
        {
            // Verificar si la solicitud es nula
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request), "The request object cannot be null");
            }

            // Obtener la reservación existente por su ID. Si no, lanzar una excepción
            var existingReservation = await _repository.GetByIdAsync(id) ?? throw new ArgumentException($"Reservation with id {id} not found");

            // Verificar que el usuario sea el propietario
            if (existingReservation.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException("The User ID does not match the existing reservation's User ID.");
            }

            // Verificar que se mantenga el mismo asiento
            if (existingReservation.SeatId != request.SeatId)
            {
                throw new InvalidOperationException("The Seat ID does not match the existing reservation's Seat ID.");
            }

            // Actualizar las propiedades de la reservación existente con los valores de la solicitud
            var updatedReservation = UpdateProperties(existingReservation, request);

            // Validar la reservación actualizada
            if (!IsValid(updatedReservation))
            {
                throw new ValidationException("Argument is invalid.");
            }

            // Actualizar la reservación en el repositorio
            _repository.Update(updatedReservation);
            await _unitOfWork.CommitAsync();

            // Mapear la reservación actualizada a un objeto de respuesta y retornarlo
            var response = _mapper.Map<Reservation, ReservationResponse>(updatedReservation);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    // Implementación de métodos abstractos de ServiceGeneric

    /// <summary>
    /// Actualiza las propiedades de una entidad <see cref="Reservation"/> existente con los valores de una solicitud de tipo <see cref="ReservationRequest"/>.
    /// </summary>
    /// <param name="existingEntity">La reserva existente que se actualizará.</param>
    /// <param name="request">La nueva solicitud de reserva con los valores actualizados.</param>
    /// <returns>La entidad <see cref="Reservation"/> actualizada.</returns>
    protected override Reservation UpdateProperties(Reservation existingEntity, ReservationRequest request)
    {
        // Actualizar las propiedades de la reserva existente con los valores de la nueva reserva
        existingEntity.Date = Date.Create(request.Date).Value;
        return existingEntity;
    }

    /// <summary>
    /// Verifica si una entidad <see cref="Reservation"/> es válida.
    /// </summary>
    /// <param name="entity">La reserva que se va a validar.</param>
    /// <returns>True si la reserva es válida, False en caso contrario.</returns>
    protected override bool IsValid(Reservation entity) =>
        // Verificar si la reserva es válida (la fecha debe ser igual o posterior a la fecha actual)
        entity.Date.Value >= DateTime.Today;
}
