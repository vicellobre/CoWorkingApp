using CoWorkingApp.Application.Abstracts.Services;
using CoWorkingApp.Application.Contracts.Adapters;
using CoWorkingApp.Application.DTOs;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Infrastructure.Abstracts;

namespace CoWorkingApp.Infrastructure.Services;

/// <summary>
/// Implementación concreta del servicio para la entidad <see cref="Seat"/>.
/// </summary>
public class SeatService : ServiceGeneric<ISeatRepository, Seat, SeatRequest, SeatResponse>, ISeatService
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SeatService"/> utilizando las dependencias necesarias.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo para manejar transacciones.</param>
    /// <param name="seatRepository">El repositorio de asientos asociado al servicio.</param>
    /// <param name="mapper">El adaptador de mapeo para convertir entre entidades y DTOs.</param>
    public SeatService(IUnitOfWork? unitOfWork, ISeatRepository? seatRepository, IMapperAdapter? mapper) : base(unitOfWork, seatRepository, mapper) { }

    // Implementación de métodos específicos de ISeatService

    /// <summary>
    /// Obtiene todas las entidades <see cref="Seat"/> disponibles de manera asincrónica.
    /// </summary>
    /// <returns>Una colección de respuestas de tipo <see cref="SeatResponse"/> que representa los asientos disponibles.</returns>
    public async Task<IEnumerable<SeatResponse>> GetAvailablesAsync()
    {
        try
        {
            // Obtener los asientos disponibles desde el repositorio
            var seatsAvailable = await _repository.GetAvailablesAsNoTrackingAsync();

            // Mapear los asientos disponibles a respuestas de asientos
            var responses = _mapper.Map<IEnumerable<Seat>, IEnumerable<SeatResponse>>(seatsAvailable).ToList();
            responses.ForEach(response => response.Success = true);
            return responses;
        }
        catch (Exception ex)
        {
            // Manejar excepciones y generar una respuesta de error
            return [HandleException(ex)];
        }
    }

    /// <summary>
    /// Obtiene una entidad <see cref="Seat"/> por su nombre de manera asincrónica.
    /// </summary>
    /// <param name="name">El nombre del asiento.</param>
    /// <returns>Una respuesta de tipo <see cref="SeatResponse"/> correspondiente al nombre proporcionado.</returns>
    public async Task<SeatResponse> GetByNameAsync(string? name)
    {
        try
        {
            // Verificar si el nombre es nulo o vacío
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "The name cannot be null or empty");
            }

            var seatNameResult = SeatName.ConvertFromString(name);
            // Obtener el asiento por su nombre desde el repositorio. Si no, lanzar una excepción
            var seat = await _repository.GetByNameAsync(seatNameResult.Value) ?? throw new ArgumentException($"Seat {name} not found");

            // Mapear el asiento a una respuesta de asiento y establecer el éxito en verdadero
            var response = _mapper.Map<Seat, SeatResponse>(seat);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones y generar una respuesta de error
            return HandleException(ex);
        }
    }

    // Implementación de métodos abstractos de ServiceGeneric

    /// <summary>
    /// Actualiza las propiedades de una entidad <see cref="Seat"/> existente con los valores de una solicitud de tipo <see cref="SeatRequest"/>.
    /// </summary>
    /// <param name="existingEntity">La entidad de asiento existente que se actualizará.</param>
    /// <param name="request">La solicitud de asiento que contiene los nuevos valores.</param>
    /// <returns>La entidad <see cref="Seat"/> actualizada.</returns>
    protected override Seat UpdateProperties(Seat existingEntity, SeatRequest request)
    {
        // Actualizar las propiedades del asiento existente con los valores de la solicitud
        if (!string.IsNullOrEmpty(request.Name))
        {
            existingEntity.Name = SeatName.Create(request.Name, "").Value;
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            existingEntity.Description = Description.Create(request.Description).Value;
        }

        //existingEntity.IsBlocked = request.IsBlocked;

        // Retornar la entidad existente que ahora está actualizada
        return existingEntity;
    }

    /// <summary>
    /// Verifica si una entidad <see cref="Seat"/> es válida.
    /// </summary>
    /// <param name="entity">La entidad <see cref="Seat"/> que se va a validar.</param>
    /// <returns>True si la entidad <see cref="Seat"/> es válida, de lo contrario False.</returns>
    protected override bool IsValid(Seat entity) =>
        // Verificar si la entidad es válida (en este caso, el nombre no puede ser nulo o vacío)
        !string.IsNullOrEmpty(entity.Name.Value);

    // Otros métodos específicos para SeatService
}
