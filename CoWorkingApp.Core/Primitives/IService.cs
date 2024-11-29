using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.Primitives;

/// <summary>
/// Interfaz que define los métodos básicos que deben implementar los servicios en la aplicación CoWorkingApp.
/// </summary>
/// <typeparam name="TRequest">Tipo de objeto de solicitud (DTO).</typeparam>
/// <typeparam name="TResponse">Tipo de objeto de respuesta (DTO).</typeparam>
public interface IService<TRequest, TResponse>
    where TRequest : IRequest
    where TResponse : ResponseMessage
{
    /// <summary>
    /// Obtiene todas las entidades disponibles de manera asincrónica.
    /// </summary>
    /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea es una colección de respuestas de tipo TResponse (DTO).</returns>
    Task<IEnumerable<TResponse>> GetAllAsync();

    /// <summary>
    /// Obtiene una entidad por su identificador único de manera asincrónica.
    /// </summary>
    /// <param name="id">El identificador único de la entidad.</param>
    /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea es la respuesta de tipo TResponse (DTO) correspondiente al identificador dado.</returns>
    Task<TResponse> GetByIdAsync(Guid id);

    /// <summary>
    /// Crea una nueva entidad de manera asincrónica.
    /// </summary>
    /// <param name="entityRequest">La entidad que se va a crear (DTO).</param>
    /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea es la respuesta de tipo TResponse (DTO) que representa la entidad creada.</returns>
    Task<TResponse> CreateAsync(TRequest entityRequest);

    /// <summary>
    /// ACTualiza una entidad existente de manera asincrónica.
    /// </summary>
    /// <param name="id">El identificador único de la entidad que se va a actualizar.</param>
    /// <param name="entityRequest">La entidad actualizada (DTO).</param>
    /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea es la respuesta de tipo TResponse (DTO) que representa la entidad actualizada.</returns>
    Task<TResponse> UpdateAsync(Guid id, TRequest entityRequest);

    /// <summary>
    /// Elimina una entidad por su identificador único de manera asincrónica.
    /// </summary>
    /// <param name="id">El identificador único de la entidad que se va a eliminar.</param>
    /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea es la respuesta de tipo TResponse (DTO) que indica el resultado de la eliminación.</returns>
    Task<TResponse> DeleteAsync(Guid id);
}
