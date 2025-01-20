using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Single;
using CoWorkingApp.Persistence.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace CoWorkingApp.Persistence.Repositories;

/// <summary>
/// Implementación del repositorio en caché para la entidad <see cref="User"/>.
/// </summary>
public class UserCachedRepository : IUserRepository
{
    private readonly IUserRepository _decorated;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _cacheExpiration;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserCachedRepository"/> utilizando el repositorio decorado y la caché de memoria especificados.
    /// </summary>
    /// <param name="decorated">El repositorio decorado.</param>
    /// <param name="memoryCache">La caché de memoria.</param>
    public UserCachedRepository(IUserRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

        _cacheExpiration = TimeSpan.FromMinutes(CacheSettings.DefaultCacheExpirationMinutes);
    }

    /// <summary>
    /// Agrega una nueva entidad <see cref="User"/> al repositorio.
    /// </summary>
    /// <param name="entity">La entidad <see cref="User"/> a agregar.</param>
    public void Add(User entity) => _decorated.Add(entity);

    /// <summary>
    /// Autentica una entidad <see cref="User"/> por su <see cref="Email"/> y <see cref="Password"/> de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="password">Contraseña del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> autenticada correspondiente a las credenciales especificadas o null si la autenticación falla.</returns>
    public Task<User?> AuthenticateAsync(Email email, Password password, CancellationToken cancellationToken = default) =>
        _decorated.AuthenticateAsync(email, password, cancellationToken);

    /// <summary>
    /// Verifica si una entidad <see cref="User"/> con el ID especificado existe en el repositorio de manera asincrónica.
    /// </summary>
    /// <param name="id">El ID de la entidad <see cref="User"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>True si la entidad existe, de lo contrario false.</returns>
    public Task<bool> ContainsAsync(Guid id, CancellationToken cancellationToken = default) =>
        _decorated.ContainsAsync(id, cancellationToken);

    /// <summary>
    /// Obtiene todas las entidades <see cref="User"/> sin seguimiento de cambios de manera asincrónica.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una colección de entidades <see cref="User"/>.</returns>
    public async Task<IEnumerable<User>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        var key = nameof(GetAllAsNoTrackingAsync);
        IEnumerable<User>? users = await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;
            return await _decorated.GetAllAsNoTrackingAsync(cancellationToken);
        });
        return users ?? [];
    }

    /// <summary>
    /// Obtiene una entidad <see cref="User"/> por su <see cref="Email"/> de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> correspondiente a la dirección de correo electrónico especificada o null si no se encuentra.</returns>
    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var key = $"{nameof(GetByEmailAsync)}_{email.Value}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;
            return await _decorated.GetByEmailAsync(email, cancellationToken);
        });
    }

    /// <summary>
    /// Obtiene una entidad <see cref="User"/> por su ID sin seguimiento de cambios de manera asincrónica.
    /// </summary>
    /// <param name="id">El ID de la entidad <see cref="User"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> correspondiente al ID especificado o null si no se encuentra.</returns>
    public async Task<User?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var key = $"{nameof(GetByIdAsNoTrackingAsync)}_{id}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;
            return await _decorated.GetByIdAsNoTrackingAsync(id, cancellationToken);
        });
    }

    /// <summary>
    /// Obtiene una entidad <see cref="User"/> por su ID de manera asincrónica.
    /// </summary>
    /// <param name="id">El ID de la entidad <see cref="User"/>.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>La entidad <see cref="User"/> correspondiente al ID especificado o null si no se encuentra.</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var key = $"{nameof(GetByIdAsync)}_{id}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;
            return await _decorated.GetByIdAsync(id, cancellationToken);
        });
    }

    /// <summary>
    /// Verifica si una dirección de correo electrónico es única de manera asincrónica.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>True si el correo electrónico es único, de lo contrario false.</returns>
    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) =>
        _decorated.IsEmailUniqueAsync(email, cancellationToken);

    /// <summary>
    /// Elimina una entidad <see cref="User"/> del repositorio.
    /// </summary>
    /// <param name="entity">La entidad <see cref="User"/> a eliminar.</param>
    public void Remove(User entity) => _decorated.Remove(entity);

    /// <summary>
    /// Actualiza una entidad <see cref="User"/> en el repositorio.
    /// </summary>
    /// <param name="entity">La entidad <see cref="User"/> a actualizar.</param>
    public void Update(User entity) => _decorated.Update(entity);
}
