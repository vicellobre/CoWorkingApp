namespace CoWorkingApp.Core.Primitives;

/// <summary>
/// Interfaz para repositorios que manejan operaciones básicas para la entidad genérica.
/// </summary>
/// <typeparam name="T">Tipo de entidad manejada por el repositorio.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Obtiene todos los elementos de la entidad de manera asincrónica.
    /// </summary>
    /// <returns>Una colección de elementos de la entidad.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Obtiene un elemento por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="id">Identificador del elemento.</param>
    /// <returns>El elemento con el identificador especificado.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Agrega un nuevo elemento de la entidad de manera asincrónica.
    /// </summary>
    /// <param name="entity">Elemento a agregar.</param>
    /// <returns>True si se agregó el elemento correctamente; de lo contrario, false.</returns>
    Task<bool> AddAsync(T entity);

    /// <summary>
    /// Actualiza un elemento de la entidad de manera asincrónica.
    /// </summary>
    /// <param name="entity">Elemento a actualizar.</param>
    /// <returns>True si se actualizó el elemento correctamente; de lo contrario, false.</returns>
    Task<bool> UpdateAsync(T entity);

    /// <summary>
    /// Elimina un elemento de la entidad de manera asincrónica.
    /// </summary>
    /// <param name="entity">Elemento a eliminar.</param>
    /// <returns>True si se eliminó el elemento correctamente; de lo contrario, false.</returns>
    Task<bool> RemoveAsync(T entity);

    /// <summary>
    /// Verifica si un elemento existe en la entidad de manera asincrónica.
    /// </summary>
    /// <param name="id">Identificador del elemento.</param>
    /// <returns>True si el elemento existe; de lo contrario, false.</returns>
    Task<bool> ExistsAsync(Guid id);
}
