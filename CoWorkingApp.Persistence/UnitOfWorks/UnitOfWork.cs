using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Persistence.Contexts;

namespace CoWorkingApp.Persistence.UnitOfWorks;

/// <summary>
/// Implementación de la interfaz <see cref="IUnitOfWork"/> para gestionar transacciones y operaciones de base de datos.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;

    /// <summary>
    /// Obtiene el contexto de la base de datos utilizado para acceder y manipular los datos.
    /// </summary>
    public CoWorkingContext Context { get; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UnitOfWork"/> con el contexto de la base de datos proporcionado.
    /// </summary>
    /// <param name="dbContext">Contexto de la base de datos (<see cref="CoWorkingContext"/>).</param>
    /// <exception cref="ArgumentNullException">Se lanza si <paramref name="dbContext"/> es nulo.</exception>
    public UnitOfWork(CoWorkingContext? dbContext)
    {
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Guarda todos los cambios realizados en el contexto de datos de manera asincrónica.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    public async Task CommitAsync(CancellationToken cancellationToken = default) => await Context.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// Libera los recursos no administrados utilizados por la instancia de <see cref="UnitOfWork"/>.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Libera los recursos no administrados y opcionalmente los administrados utilizados por la instancia de <see cref="UnitOfWork"/>.
    /// </summary>
    /// <param name="disposing">Indica si se deben liberar los recursos administrados.</param>
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                try
                {
                    Context.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error disposing context: {ex.Message}");
                }
            }
            _disposed = true;
        }
    }
}
