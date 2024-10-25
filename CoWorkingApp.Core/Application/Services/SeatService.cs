using CoWorkingApp.Core.Application.Abstracts;
using CoWorkingApp.Core.Application.Contracts.Adapters;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Application.Contracts.Services;
using CoWorkingApp.Core.Domain.DTOs;
using CoWorkingApp.Core.Domain.Entities;

namespace CoWorkingApp.Core.Application.Services
{
    /// <summary>
    /// Implementación concreta del servicio para la entidad Seat.
    /// </summary>
    public class SeatService : ServiceGeneric<ISeatRepository, Seat, SeatRequest, SeatResponse>, ISeatService
    {
        /// <summary>
        /// Constructor de la clase SeatService.
        /// </summary>
        /// <param name="seatRepository">El repositorio de asientos asociado al servicio.</param>
        public SeatService(ISeatRepository? seatRepository, IMapperAdapter? mapper) : base(seatRepository, mapper) { }

        // Implementación de métodos específicos de ISeatService

        /// <summary>
        /// Obtiene todos los asientos disponibles.
        /// </summary>
        /// <returns>Una colección de respuestas de asientos disponibles.</returns>
        public async Task<IEnumerable<SeatResponse>> GetAvailablesAsync()
        {
            try
            {
                // Obtener los asientos disponibles desde el repositorio
                var seatsAvailable = await _repository.GetAvailablesAsync();

                // Mapear los asientos disponibles a respuestas de asientos
                var responses = _mapper.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(seatsAvailable).ToList();
                responses.ForEach(responses => responses.Success = true);
                return responses;
            }
            catch (Exception ex)
            {
                // Manejar excepciones y lanzar un error genérico
                return new List<SeatResponse> { HandleException(ex) };
            }
        }

        /// <summary>
        /// Obtiene un asiento por su nombre.
        /// </summary>
        /// <param name="name">El nombre del asiento.</param>
        /// <returns>Una respuesta de asiento correspondiente al nombre proporcionado.</returns>
        public async Task<SeatResponse> GetByNameAsync(string name)
        {
            try
            {
                // Verificar si el nombre es nulo o vacío
                if (string.IsNullOrEmpty(name))
                {
                    // Si es nulo o vacío, lanzar una excepción
                    throw new ArgumentNullException("The name cannot be null or empty");
                }

                // Obtener el asiento por su nombre desde el repositorio. Si no, lanzar una excepción
                var seat = await _repository.GetByNameAsync(name) ?? throw new ArgumentException($"Seat {name} not found");

                // Mapear el asiento a una respuesta de asiento y establecer el éxito en verdadero
                var response = _mapper.Map<Seat, SeatResponse>(seat);
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
            catch (Exception)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                var exception = new Exception();
                return HandleException(exception);
            }
        }

        // Implementación de métodos abstractos de ServiceGeneric

        /// <summary>
        /// ACTualiza las propiedades de un asiento existente con los valores de una solicitud de asiento.
        /// </summary>
        /// <param name="existingEntity">La entidad de asiento existente que se actualizará.</param>
        /// <param name="request">La solicitud de asiento que contiene los nuevos valores.</param>
        /// <returns>El asiento actualizado.</returns>
        protected override Seat UpdateProperties(Seat existingEntity, SeatRequest request)
        {
            // ACTualizar las propiedades del asiento existente con los valores de la solicitud
            return new Seat
            {
                Name = string.IsNullOrEmpty(request.Name) ? existingEntity.Name : request.Name,
                IsBlocked = request.IsBlocked,
                Description = string.IsNullOrEmpty(request.Description) ? existingEntity.Description : request.Description,
            };
        }

        /// <summary>
        /// Verifica si un asiento es válido.
        /// </summary>
        /// <param name="entity">El asiento que se va a validar.</param>
        /// <returns>True si el asiento es válido, False en caso contrario.</returns>
        protected override bool IsValid(Seat entity)
        {
            // Verificar si la entidad es válida (en este caso, el nombre no puede ser nulo o vacío)
            return !string.IsNullOrEmpty(entity.Name);
        }

        // Otros métodos específicos para SeatService
    }
}
