using CoWorkingApp.Core.Application.Contracts.Adapters;
using CoWorkingApp.Core.Application.Contracts.Repositories;
using CoWorkingApp.Core.Application.Contracts.Requests;
using CoWorkingApp.Core.Application.Contracts.Services;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Core.Application.Abstracts
{
    /// <summary>
    /// Clase abstracta que proporciona una implementación genérica para servicios que manejan operaciones CRUD básicas.
    /// </summary>
    /// <typeparam name="TRepository">Tipo de repositorio asociado al servicio.</typeparam>
    /// <typeparam name="TEntity">Tipo de entidad que se manejará.</typeparam>
    /// <typeparam name="TRequest">Tipo de objeto de solicitud.</typeparam>
    /// <typeparam name="TResponse">Tipo de objeto de respuesta.</typeparam>
    public abstract class ServiceGeneric<TRepository, TEntity, TRequest, TResponse> : IService<TRequest, TResponse>
        where TRepository : IRepository<TEntity>
        where TEntity : EntityBase
        where TRequest : IRequest
        where TResponse : ResponseMessage, new()
    {
        protected readonly TRepository _repository;
        protected readonly IMapperAdapter _mapper;

        /// <summary>
        /// Constructor de la clase ServiceGeneric.
        /// </summary>
        /// <param name="repository">El repositorio asociado al servicio.</param>
        /// <param name="mapper">El adaptador de mapeo utilizado para mapear entre tipos de entidades y DTO.</param>
        protected ServiceGeneric(TRepository? repository, IMapperAdapter? mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Maneja las excepciones y construye una respuesta de error coherente.
        /// </summary>
        /// <param name="ex">La excepción que se va a manejar.</param>
        /// <returns>Un objeto de respuesta que representa el error.</returns>
        protected TResponse HandleException(Exception ex)
        {
            return ResponseMessage.HandleException<TResponse>(ex);
        }

        // Implementación de métodos de la interfaz IService

        /// <summary>
        /// Obtiene todas las entidades de manera asincrónica.
        /// </summary>
        /// <returns>Una colección de objetos de respuesta.</returns>
        public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            try
            {
                // Obtener todas las entidades del repositorio
                var entities = await _repository.GetAllAsync();

                // Mapear las entidades a objetos de respuesta y retornarlos
                var responses = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TResponse>>(entities).ToList();
                responses.ForEach(res => res.Success = true);
                return responses;
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return new List<TResponse> { HandleException(ex) };
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID de manera asincrónica.
        /// </summary>
        /// <param name="id">El ID de la entidad.</param>
        /// <returns>Un objeto de respuesta.</returns>
        public virtual async Task<TResponse> GetByIdAsync(Guid id)
        {
            try
            {
                // Buscar la entidad por su ID en el repositorio. Si no, lanzar una excepción
                var entity = await _repository.GetByIdAsync(id) ?? throw new ArgumentException($"Entity with id {id} not found");

                // Mapear la entidad a un objeto de respuesta y retornarlo
                var response = _mapper.Map<TEntity, TResponse>(entity);
                response.Success = true;
                return response;
            }
            catch (ArgumentException ex)
            {
                // Manejar la excepción de entidad no encontrada y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Crea una nueva entidad de manera asincrónica.
        /// </summary>
        /// <param name="request">El objeto de solicitud.</param>
        /// <returns>Un objeto de respuesta.</returns>
        public virtual async Task<TResponse> CreateAsync(TRequest request)
        {
            try
            {
                // Verificar si la solicitud es nula
                if (request is null)
                {
                    // Si la solicitud es nula, lanzar una excepción
                    throw new ArgumentNullException("The request object cannot be null");
                }

                // Mapear la solicitud a una entidad
                var entity = _mapper.Map<TRequest, TEntity>(request);

                // Validar la entidad
                if (!IsValid(entity))
                {
                    // Si la entidad no es válida, lanzar una excepción
                    throw new ValidationException("Argument is invalid.");
                }

                // Agregar la entidad al repositorio
                var added = await _repository.AddAsync(entity);

                // Verificar si la entidad fue agregada correctamente
                if (!added)
                {
                    // Si la entidad no fue agregada, lanzar una excepción
                    throw new InvalidOperationException("The entity could not be added.");
                }

                // Mapear la entidad a un objeto de respuesta y retornarlo
                var response = _mapper.Map<TEntity, TResponse>(entity);
                response.Success = true;
                return response;
            }
            catch (ArgumentNullException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ValidationException ex)
            {
                // Manejar la excepción de validación y generar una respuesta de error
                return HandleException(ex);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción de operación no válida y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return HandleException(ex);
            }
        }

        /// <summary>
        /// ACTualiza una entidad existente de manera asincrónica.
        /// </summary>
        /// <param name="id">El ID de la entidad.</param>
        /// <param name="request">El objeto de solicitud.</param>
        /// <returns>Un objeto de respuesta.</returns>
        public virtual async Task<TResponse> UpdateAsync(Guid id, TRequest request)
        {
            try
            {
                // Verificar si la solicitud es nula
                if (request is null)
                {
                    // Si la solicitud es nula, lanzar una excepción
                    throw new ArgumentNullException("The request object cannot be null");
                }

                // Obtener la entidad existente por su ID. Si no, lanzar una excepción
                var existingEntity = await _repository.GetByIdAsync(id) ?? throw new ArgumentException($"Entity with id {id} not found");

                // ACTualizar las propiedades de la entidad existente con los valores de la solicitud
                var updatedEntity = UpdateProperties(existingEntity, request);

                // Validar la entidad actualizada
                if (!IsValid(updatedEntity))
                {
                    // Si la entidad no es válida, lanzar una excepción
                    throw new ValidationException("Argument is invalid.");
                }

                // ACTualizar la entidad en el repositorio
                bool updated = await _repository.UpdateAsync(updatedEntity);

                // Verificar si la entidad fue actualizada correctamente
                if (!updated)
                {
                    // Si la entidad no fue actualizada, lanzar una excepción
                    throw new InvalidOperationException("The entity could not be updated.");
                }

                // Mapear la entidad actualizada a un objeto de respuesta y retornarlo
                var response = _mapper.Map<TEntity, TResponse>(updatedEntity);
                response.Success = true;
                return response;
            }
            catch (ArgumentNullException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ArgumentException ex)
            {
                // Manejar la excepción de argumento inválido y generar una respuesta de error
                return HandleException(ex);
            }
            catch (ValidationException ex)
            {
                // Manejar la excepción de validación y generar una respuesta de error
                return HandleException(ex);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción de operación no válida y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Elimina una entidad por su ID de manera asincrónica.
        /// </summary>
        /// <param name="id">El ID de la entidad.</param>
        /// <returns>Un objeto de respuesta.</returns>
        public virtual async Task<TResponse> DeleteAsync(Guid id)
        {
            try
            {
                // Obtener la entidad por su ID. Si no, lanzar una excepción
                var entity = await _repository.GetByIdAsync(id) ?? throw new ArgumentNullException($"Entity with id {id} not found");

                // Eliminar la entidad del repositorio
                var deleted = await _repository.RemoveAsync(entity);

                // Verificar si la entidad fue eliminada correctamente
                if (!deleted)
                {
                    // Si la entidad no fue eliminada, lanzar una excepción
                    throw new InvalidOperationException("The entity could not be deleted.");
                }

                // Mapear la entidad eliminada a un objeto de respuesta y retornarlo
                var response = _mapper.Map<TEntity, TResponse>(entity);
                response.Success = true;
                return response;
            }
            catch (ArgumentNullException ex)
            {
                // Manejar la excepción de argumento nulo y generar una respuesta de error
                return HandleException(ex);
            }
            catch (InvalidOperationException ex)
            {
                // Manejar la excepción de operación no válida y generar una respuesta de error
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y generar una respuesta de error
                return HandleException(ex);
            }
        }

        // Métodos abstractos que deben ser implementados por las clases derivadas

        /// <summary>
        /// ACTualiza las propiedades de una entidad existente con los valores de una nueva entidad.
        /// </summary>
        /// <param name="existingEntity">La entidad existente que se va a actualizar.</param>
        /// <param name="request">La entidad con los nuevos valores.</param>
        /// <returns>La entidad actualizada.</returns>
        protected abstract TEntity UpdateProperties(TEntity existingEntity, TRequest request);

        /// <summary>
        /// Verifica si una entidad es válida.
        /// </summary>
        /// <param name="entity">La entidad que se va a validar.</param>
        /// <returns>True si la entidad es válida, de lo contrario False.</returns>
        protected abstract bool IsValid(TEntity entity);
    }
}
