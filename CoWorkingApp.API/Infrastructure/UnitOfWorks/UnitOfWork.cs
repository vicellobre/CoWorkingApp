using CoWorkingApp.API.Infrastructure.Context;

namespace CoWorkingApp.API.Infrastructure.UnitOfWorks
{
    /// <summary>
    /// Implementación de la interfaz IUnitOfWork para gestionar transacciones y operaciones de base de datos.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Obtiene el contexto de la base de datos utilizado para acceder y manipular los datos.
        /// </summary>
        public CoWorkingContext Context { get; }

        /// <summary>
        /// Constructor que inicializa una nueva instancia de UnitOfWork con el contexto de la base de datos proporcionado.
        /// </summary>
        /// <param name="dbContext">Contexto de la base de datos (CoWorkingContext).</param>
        public UnitOfWork(CoWorkingContext dbContext)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Confirma los cambios realizados en el contexto de la base de datos.
        /// </summary>
        /// <returns>Una tarea asincrónica que representa la operación de confirmación.</returns>
        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Libera los recursos no administrados utilizados por la instancia de UnitOfWork.
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
