namespace CoWorkingApp.Core.Primitives;

/// <summary>
/// Interfaz que define las operaciones básicas para un repositorio genérico.
/// </summary>
/// <typeparam name="T">Tipo de la entidad gestionada por el repositorio.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Recupera todas las entidades de tipo <typeparamref name="T"/> de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de entidades de tipo <typeparamref name="T"/>.</returns>
    Task<IEnumerable<T>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera una entidad de tipo <typeparamref name="T"/> por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="id">Identificador de la entidad de tipo <typeparamref name="T"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad de tipo <typeparamref name="T"/> con el identificador especificado, o null si no se encuentra.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera una entidad de tipo <typeparamref name="T"/> por su identificador de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="id">Identificador de la entidad de tipo <typeparamref name="T"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad de tipo <typeparamref name="T"/> con el identificador especificado, o null si no se encuentra.</returns>
    Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Comprueba si contiene una entidad de tipo <typeparamref name="T"/> con el identificador especificado de manera asincrónica.
    /// </summary>
    /// <param name="id">Identificador de la entidad de tipo <typeparamref name="T"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>True si la entidad de tipo <typeparamref name="T"/> existe; de lo contrario, false.</returns>
    Task<bool> ContainsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Añade una nueva entidad de tipo <typeparamref name="T"/> al repositorio.
    /// </summary>
    /// <param name="entity">La entidad de tipo <typeparamref name="T"/> a añadir.</param>
    void Add(T entity);

    /// <summary>
    /// Actualiza una entidad existente de tipo <typeparamref name="T"/> en el repositorio.
    /// </summary>
    /// <param name="entity">La entidad de tipo <typeparamref name="T"/> a actualizar.</param>
    void Update(T entity);

    /// <summary>
    /// Elimina una entidad existente de tipo <typeparamref name="T"/> del repositorio.
    /// </summary>
    /// <param name="entity">La entidad de tipo <typeparamref name="T"/> a eliminar.</param>
    void Remove(T entity);
}
