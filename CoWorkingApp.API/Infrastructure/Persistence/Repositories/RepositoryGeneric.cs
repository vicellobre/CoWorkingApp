using CoWorkingApp.API.Infrastructure.UnitOfWorks;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoWorkingApp.API.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Clase genérica que implementa la interfaz <see cref="IRepository{T}"/> 
    /// y proporciona operaciones básicas de acceso a datos para entidades del tipo <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad gestionada por el repositorio.</typeparam>
    public abstract class RepositoryGeneric<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Unidad de trabajo utilizada para gestionar la transacción de operaciones de datos.
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Conjunto de datos que representa la colección de entidades <typeparamref name="T"/> en el contexto de datos.
        /// Proporciona métodos para consultar y realizar operaciones CRUD en la base de datos.
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Constructor que recibe una instancia de IUnitOfWork para gestionar la conexión con la base de datos.
        /// </summary>
        /// <param name="unitOfWork">Instancia de IUnitOfWork.</param>
        public RepositoryGeneric(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dbSet = _unitOfWork.Context.Set<T>() ?? throw new InvalidOperationException("DbSet could not be initialized.");
        }

        /// <summary>
        /// Obtiene todos los registros de la entidad de la base de datos de manera asincrónica.
        /// </summary>
        /// <returns>Una colección de todos los registros de la entidad.</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un registro por su identificador único de manera asincrónica.
        /// </summary>
        /// <param name="id">Identificador único del registro.</param>
        /// <returns>El registro con el identificador único especificado, o null si no se encuentra.</returns>
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Agrega un nuevo registro a la base de datos de manera asincrónica.
        /// </summary>
        /// <param name="entity">Entidad a agregar.</param>
        /// <returns>True si se agregó correctamente; de lo contrario, false.</returns>
        public virtual async Task<bool> AddAsync(T entity)
        {
            try
            {
                // Agrega la entidad al conjunto de entidades del contexto
                _dbSet.Add(entity);

                // Guarda los cambios en la base de datos
                await _unitOfWork.CommitAsync();

                // Retorna true indicando que la operación fue exitosa
                return true;
            }
            catch (Exception)
            {
                // Retorna false si ocurre alguna excepción al agregar la entidad
                return false;
            }
        }

        /// <summary>
        /// ACTualiza un registro existente en la base de datos de manera asincrónica.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        /// <returns>True si se actualizó correctamente; de lo contrario, false.</returns>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                // Adjunta la entidad al contexto y marca su estado como modificado
                _dbSet.Attach(entity);
                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;

                // Guarda los cambios en la base de datos
                await _unitOfWork.CommitAsync();

                // Retorna true indicando que la operación fue exitosa
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Retorna false si ocurre alguna excepción al actualizar la entidad
                return false;
            }
        }

        /// <summary>
        /// Elimina un registro de la base de datos de manera asincrónica.
        /// </summary>
        /// <param name="entity">Entidad a eliminar.</param>
        /// <returns>True si se eliminó correctamente; de lo contrario, false.</returns>
        public virtual async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                // Remueve la entidad del conjunto de entidades del contexto
                _dbSet.Remove(entity);

                // Guarda los cambios en la base de datos
                await _unitOfWork.CommitAsync();

                // Retorna true indicando que la operación fue exitosa
                return true;
            }
            catch (Exception)
            {
                // Retorna false si ocurre alguna excepción al eliminar la entidad
                return false;
            }
        }

        /// <summary>
        /// Verifica si un elemento existe en la entidad de manera asincrónica.
        /// </summary>
        /// <param name="id">Identificador del elemento.</param>
        /// <returns>True si el elemento existe; de lo contrario, false.</returns>
        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.FindAsync(id) is not null;
        }
    }
}
