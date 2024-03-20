using CoWorkingApp.Core.Application.Abstracts;
using CoWorkingApp.Core.Application.Contracts.Adapters;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Application.Contracts.Services;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Core.Domain.Entities;

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
        public ReservationService(IReservationRepository reservationRepository, IMapperAdapter mapper) : base(reservationRepository, mapper) { }

        // Implementación de métodos específicos de IReservationService

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
