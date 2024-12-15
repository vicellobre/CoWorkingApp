namespace CoWorkingApp.Core.Contracts.UnitOfWork;

/// <summary>
/// Interfaz que define la unidad de trabajo para manejar transacciones y persistencia de datos.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Guarda todos los cambios realizados en el contexto de datos de manera asincrónica.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional para la operación asincrónica.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    Task CommitAsync(CancellationToken cancellationToken = default);
}
