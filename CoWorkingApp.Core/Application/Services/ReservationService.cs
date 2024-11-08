using CoWorkingApp.Core.Application.Abstracts;
using CoWorkingApp.Core.Application.Contracts.Adapters;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Application.Contracts.Requests;
using CoWorkingApp.Core.Application.Contracts.Services;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Core.Application.Services
{
    /// <summary>
    /// Implementación concreta del servicio para la entidad Reservation.
    /// </summary>
    public class ReservationService : ServiceGeneric<IReservationRepository, Reservation, ReservationRequest, ReservationResponse>, IReservationService
    {
        /// <summary>
        /// Constructor de la clase ReservationService.
        /// </summary>
        /// <param name="reservationRepository">El repositorio de reservas asociado al servicio.</param>
        public ReservationService(IReservationRepository? reservationRepository, IMapperAdapter? mapper) : base(reservationRepository, mapper) { }

        // Implementación de métodos específicos de IReservationService

        /// <summary>
        /// Crea una nueva reservación de manera asincrónica.
        /// </summary>
        /// <param name="request">El objeto de solicitud.</param>
        /// <returns>Un objeto de respuesta.</returns>
        public override async Task<ReservationResponse> CreateAsync(ReservationRequest? request)
        {
            try
            {
                // Verificar si la solicitud es nula
                if (request is null)
                {
                    // Si la solicitud es nula, lanzar una excepción
                    throw new ArgumentNullException("The request object cannot be null");
                }

                // Mapear la solicitud a una reservación
                var reservation = _mapper.Map<ReservationRequest, Reservation>(request);

                // Validar la reservación
                if (!IsValid(reservation))
                {
                    // Si la reservación no es válida, lanzar una excepción
                    throw new ValidationException("Argument is invalid.");
                }

                // Agregar la reservación al repositorio
                var added = await _repository.AddAsync(reservation);

                // Obetner la información completa sobre la reservación
                var responseComplete = await _repository.GetByIdAsync(reservation.Id);
                if (responseComplete is null)
                {
                    throw new ArgumentException("The request contains inconsistent details");
                }

                // Verificar si la reservación fue agregada correctamente
                if (!added)
                {
                    // Si la reservación no fue agregada, lanzar una excepción
                    throw new InvalidOperationException("The reservation could not be added.");
                }

                // Mapear la reservación a un objeto de respuesta y retornarlo
                var response = _mapper.Map<Reservation, ReservationResponse>(responseComplete);
                response.Success = true;
                return response;
            }
            catch (ArgumentNullException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ValidationException ex)
            {
                // Manejar la excepción de argumentos inválidos y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ArgumentException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción de operación no válida y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Obtiene las reservaciones realizadas en una fecha específica de manera asincrónica.
        /// </summary>
        /// <param name="date">La fecha para la cual se desean obtener las reservaciones.</param>
        /// <returns>Una colección de respuestas de reservación que incluyen información sobre las reservaciones realizadas en la fecha especificada.</returns>
        public async Task<IEnumerable<ReservationResponse>> GetByDateAsync(DateTime date)
        {
            try
            {
                // Obtiene las reservaciones por fecha del repositorio
                var reservations = await _repository.GetByDateAsync(date);

                // Mapea las reservaciones a ReservationResponse y las convierte en una lista
                var response = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations).ToList();

                // Marca todas las respuestas como exitosas
                response.ForEach(r => r.Success = true);

                return response;
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción y devuelve una lista con una sola respuesta de error
                return new List<ReservationResponse> { HandleException(ex) };
            }
        }

        /// <summary>
        /// Obtiene las reservaciones realizadas por un usuario específico de manera asincrónica.
        /// </summary>
        /// <param name="userId">El ID del usuario para el cual se desean obtener las reservaciones.</param>
        /// <returns>Una colección de respuestas de reservación que incluyen información sobre las reservaciones realizadas por el usuario con el ID especificado.</returns>
        public async Task<IEnumerable<ReservationResponse>> GetByUserIdAsync(Guid userId)
        {
            try
            {
                // Obtiene las reservaciones por ID de usuario del repositorio
                var reservations = await _repository.GetByUserIdAsync(userId);

                // Mapea las reservaciones a ReservationResponse y las convierte en una lista
                var response = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations).ToList();

                // Marca todas las respuestas como exitosas
                response.ForEach(r => r.Success = true);

                return response;
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción y devuelve una lista con una sola respuesta de error
                return new List<ReservationResponse> { HandleException(ex) };
            }
        }

        /// <summary>
        /// Obtiene las reservaciones asociadas a un asiento específico de manera asincrónica.
        /// </summary>
        /// <param name="seatId">El ID del asiento para el cual se desean obtener las reservaciones.</param>
        /// <returns>Una colección de respuestas de reservación que incluyen información sobre las reservaciones asociadas al asiento con el ID especificado.</returns>
        public async Task<IEnumerable<ReservationResponse>> GetBySeatIdAsync(Guid seatId)
        {
            try
            {
                // Obtiene las reservaciones por ID de asiento del repositorio
                var reservations = await _repository.GetBySeatIdAsync(seatId);

                // Mapea las reservaciones a ReservationResponse y las convierte en una lista
                var response = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationResponse>>(reservations).ToList();

                // Marca todas las respuestas como exitosas
                response.ForEach(r => r.Success = true);

                return response;
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción y devuelve una lista con una sola respuesta de error
                return new List<ReservationResponse> { HandleException(ex) };
            }
        }

        /// <summary>
        /// Actualiza una reservación existente de manera asincrónica.
        /// </summary>
        /// <param name="id">El ID de la reservación.</param>
        /// <param name="request">El objeto de solicitud.</param>
        /// <returns>Un objeto de respuesta.</returns>
        public override async Task<ReservationResponse> UpdateAsync(Guid id, ReservationRequest? request)
        {
            try
            {
                // Verificar si la solicitud es nula
                if (request is null)
                {
                    // Si la solicitud es nula, lanzar una excepción
                    throw new ArgumentNullException("The request object cannot be null");
                }

                // Obtener la reservación existente por su ID. Si no, lanzar una excepción
                var existingReservation = await _repository.GetByIdAsync(id) ?? throw new ArgumentException($"Reservation with id {id} not found");

                // Verifica que el usuario sea el propietario
                if (existingReservation.UserId != request.UserId)
                {
                    // Lanzar una excepción si el UserId de la solicitud no coincide con el de la reservación existente
                    throw new UnauthorizedAccessException("The User ID does not match the existing reservation's User ID.");
                }

                // Verifica que se mantenga el mismo asiento
                if (existingReservation.SeatId != request.SeatId)
                {
                    // Lanzar una excepción si el SeatId de la solicitud no coincide con el de la reservación existente
                    throw new InvalidOperationException("The Seat ID does not match the existing reservation's Seat ID.");
                }

                // Actualizar las propiedades de la reservación existente con los valores de la solicitud
                var updatedReservation = UpdateProperties(existingReservation, request);

                // Validar la reservación actualizada
                if (!IsValid(updatedReservation))
                {
                    // Si la reservación no es válida, lanzar una excepción
                    throw new ValidationException("Argument is invalid.");
                }

                // Actualizar la reservación en el repositorio
                bool updated = await _repository.UpdateAsync(updatedReservation);

                // Verificar si la reservación fue actualizada correctamente
                if (!updated)
                {
                    // Si la reservación no fue actualizada, lanzar una excepción
                    throw new InvalidOperationException($"The reservation could not be updated.");
                }

                // Mapear la reservación actualizada a un objeto de respuesta y retornarlo
                var response = _mapper.Map<Reservation, ReservationResponse>(updatedReservation);
                response.Success = true;
                return response;
            }
            catch (ArgumentNullException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ArgumentException ex)
            {
                // Manejar la excepción de argumento inválido y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ValidationException ex)
            {
                // Manejar la excepción de validación y generar una respuesta de error
                return HandleException(ex);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción de operación no válida y generar una respuesta de error
                return HandleException(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Manejar la excepción de acceso no autorizado y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return HandleException(ex);
            }
        }

        // Implementación de métodos abstractos de ServiceGeneric

        /// <summary>
        /// ACTualiza las propiedades de una reserva existente con los valores de una nueva reserva.
        /// </summary>
        /// <param name="existingEntity">La reserva existente que se actualizará.</param>
        /// <param name="newEntity">La nueva reserva con los valores actualizados.</param>
        /// <returns>La reserva actualizada.</returns>
        protected override Reservation UpdateProperties(Reservation existingEntity, ReservationRequest newEntity)
        {
            // ACTualizar las propiedades de la reserva existente con los valores de la nueva reserva
            existingEntity.Date = newEntity.Date;
            return existingEntity;
        }

        /// <summary>
        /// Verifica si una reserva es válida.
        /// </summary>
        /// <param name="entity">La reserva que se va a validar.</param>
        /// <returns>True si la reserva es válida, False en caso contrario.</returns>
        protected override bool IsValid(Reservation entity)
        {
            // Verificar si la reserva es válida (la fecha debe ser igual o posterior a la fecha actual)
            return entity.Date >= DateTime.Today;
        }

        // Implementa otros métodos específicos para ReservationService
    }
}
