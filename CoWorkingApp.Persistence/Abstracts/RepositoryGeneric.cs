using CoWorkingApp.Core.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.Persistence.Abstracts;

/// <summary>
/// Clase genérica que implementa la interfaz <see cref="IRepository{T}"/> 
/// y proporciona operaciones básicas de acceso a datos para entidades del tipo <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Tipo de entidad gestionada por el repositorio.</typeparam>
public abstract class RepositoryGeneric<T> : IRepository<T> where T : EntityBase
{
    /// <summary>
    /// Conjunto de datos que representa la colección de entidades <typeparamref name="T"/> en el contexto de datos.
    /// Proporciona métodos para consultar y realizar operaciones CRUD en la base de datos.
    /// </summary>
    protected DbSet<T> Set { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="RepositoryGeneric{T}"/> utilizando el contexto de datos especificado.
    /// </summary>
    /// <param name="context">El contexto de datos de Entity Framework.</param>
    /// <exception cref="ArgumentNullException">Se lanza cuando el parámetro <paramref name="context"/> es nulo.</exception>
    /// <exception cref="InvalidOperationException">Se lanza cuando el conjunto de datos no puede ser inicializado.</exception>
    public RepositoryGeneric(DbContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        Set = context.Set<T>() ?? throw new InvalidOperationException("DbSet could not be initialized.");
    }

    /// <summary>
    /// Recupera todas las entidades de tipo <typeparamref name="T"/> de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de entidades de tipo <typeparamref name="T"/>.</returns>
    public virtual async Task<IEnumerable<T>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Recupera una entidad de tipo <typeparamref name="T"/> por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="id">Identificador de la entidad de tipo <typeparamref name="T"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad de tipo <typeparamref name="T"/> con el identificador especificado, o null si no se encuentra.</returns>
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await Set.FindAsync(new object[] { id }, cancellationToken);

    /// <summary>
    /// Recupera una entidad de tipo <typeparamref name="T"/> por su identificador de manera asincrónica sin realizar seguimiento de cambios.
    /// </summary>
    /// <param name="id">Identificador de la entidad de tipo <typeparamref name="T"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad de tipo <typeparamref name="T"/> con el identificador especificado, o null si no se encuentra.</returns>
    public virtual async Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Set
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    /// <summary>
    /// Comprueba si existe una entidad de tipo <typeparamref name="T"/> con el identificador especificado de manera asincrónica.
    /// </summary>
    /// <param name="id">Identificador de la entidad de tipo <typeparamref name="T"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>True si la entidad de tipo <typeparamref name="T"/> existe; de lo contrario, false.</returns>
    public virtual async Task<bool> ContainsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Set.FindAsync([id, cancellationToken], cancellationToken: cancellationToken) is not null;

    /// <summary>
    /// Añade una nueva entidad de tipo <typeparamref name="T"/> al repositorio.
    /// </summary>
    /// <param name="entity">La entidad de tipo <typeparamref name="T"/> a añadir.</param>
    public virtual void Add(T entity) => Set.Add(entity);

    /// <summary>
    /// Actualiza una entidad existente de tipo <typeparamref name="T"/> en el repositorio.
    /// </summary>
    /// <param name="entity">La entidad de tipo <typeparamref name="T"/> a actualizar.</param>
    public virtual void Update(T entity) => Set.Update(entity);

    /// <summary>
    /// Elimina una entidad existente de tipo <typeparamref name="T"/> del repositorio.
    /// </summary>
    /// <param name="entity">La entidad de tipo <typeparamref name="T"/> a eliminar.</param>
    public virtual void Remove(T entity) => Set.Remove(entity);
}
