using CoWorkingApp.Application.Abstracts.Services;
using CoWorkingApp.Application.Contracts.Adapters;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.Primitives;
using CoWorkingApp.Core.Shared;
using System.ComponentModel.DataAnnotations;

namespace CoWorkingApp.Infrastructure.Abstracts;

/// <summary>
/// Clase abstracta que proporciona una implementación genérica para servicios que manejan operaciones CRUD básicas.
/// </summary>
/// <typeparam name="TRepository">Tipo de repositorio asociado al servicio.</typeparam>
/// <typeparam name="TEntity">Tipo de entidad que se manejará.</typeparam>
/// <typeparam name="TRequest">Tipo de objeto de solicitud (<see cref="IRequest"/>).</typeparam>
/// <typeparam name="TResponse">Tipo de objeto de respuesta (<see cref="IResponse"/>).</typeparam>
public abstract class ServiceGeneric<TRepository, TEntity, TRequest, TResponse> : IService<TRequest, TResponse>
    where TRepository : IRepository<TEntity>
    where TEntity : EntityBase
    where TRequest : IRequest
    where TResponse : ResponseMessage, new()
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly TRepository _repository;
    protected readonly IMapperAdapter _mapper;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ServiceGeneric{TRepository, TEntity, TRequest, TResponse}"/> con las dependencias necesarias.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo para manejar transacciones.</param>
    /// <param name="repository">El repositorio de tipo <typeparamref name="TRepository"/> asociado al servicio.</param>
    /// <param name="mapper">El adaptador de mapeo para convertir entre entidades y DTOs.</param>
    /// <exception cref="ArgumentNullException">Se lanza si alguno de los parámetros es nulo.</exception>
    protected ServiceGeneric(IUnitOfWork? unitOfWork, TRepository? repository, IMapperAdapter? mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Maneja las excepciones y construye una respuesta de error coherente.
    /// </summary>
    /// <param name="ex">La excepción que se va a manejar.</param>
    /// <returns>Un objeto de respuesta de tipo <typeparamref name="TResponse"/> que representa el error.</returns>
    protected TResponse HandleException(Exception ex) => ResponseMessage.HandleException<TResponse>(ex);

    // Implementación de métodos de la interfaz IService

    /// <summary>
    /// Obtiene todas las entidades de tipo <typeparamref name="TResponse"/> de manera asincrónica.
    /// </summary>
    /// <returns>Una colección de objetos de tipo <typeparamref name="TResponse"/>.</returns>
    public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
    {
        try
        {
            // Obtener todas las entidades del repositorio
            var entities = await _repository.GetAllAsNoTrackingAsync();

            // Mapear las entidades a objetos de respuesta y retornarlos
            var responses = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TResponse>>(entities).ToList();
            responses.ForEach(res => res.Success = true);
            return responses;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return [HandleException(ex)];
        }
    }

    /// <summary>
    /// Obtiene una entidad de tipo <typeparamref name="TResponse"/> por su identificador de manera asincrónica.
    /// </summary>
    /// <param name="id">El identificador único de la entidad.</param>
    /// <returns>Un objeto de tipo <typeparamref name="TResponse"/>.</returns>
    public virtual async Task<TResponse> GetByIdAsync(Guid id)
    {
        try
        {
            // Buscar la entidad por su ID en el repositorio. Si no se encuentra, lanzar una excepción
            var entity = await _repository.GetByIdAsNoTrackingAsync(id) ?? throw new ArgumentException($"Entity with id {id} not found");

            // Mapear la entidad a un objeto de respuesta y retornarlo
            var response = _mapper.Map<TEntity, TResponse>(entity);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    /// <summary>
    /// Crea una nueva entidad de tipo <typeparamref name="TResponse"/> de manera asincrónica.
    /// </summary>
    /// <param name="request">La entidad de tipo <typeparamref name="TRequest"/> que se va a crear.</param>
    /// <returns>Un objeto de tipo <typeparamref name="TResponse"/> que representa la entidad creada.</returns>
    public virtual async Task<TResponse> CreateAsync(TRequest? request)
    {
        try
        {
            // Verificar si la solicitud es nula
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request), "The request object cannot be null");
            }

            // Mapear la solicitud a una entidad
            var entity = _mapper.Map<TRequest, TEntity>(request);

            // Validar la entidad
            if (!IsValid(entity))
            {
                throw new ValidationException("Argument is invalid.");
            }

            // Agregar la entidad al repositorio
            _repository.Add(entity);
            await _unitOfWork.CommitAsync();

            // Mapear la entidad a un objeto de respuesta y retornarlo
            var response = _mapper.Map<TEntity, TResponse>(entity);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    /// <summary>
    /// Actualiza una entidad existente de tipo <typeparamref name="TResponse"/> de manera asincrónica.
    /// </summary>
    /// <param name="id">El identificador único de la entidad que se va a actualizar.</param>
    /// <param name="request">La entidad de tipo <typeparamref name="TRequest"/> actualizada.</param>
    /// <returns>Un objeto de tipo <typeparamref name="TResponse"/> que representa la entidad actualizada.</returns>
    public virtual async Task<TResponse> UpdateAsync(Guid id, TRequest? request)
    {
        try
        {
            // Verificar si la solicitud es nula
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request), "The request object cannot be null");
            }

            // Obtener la entidad existente por su ID. Si no se encuentra, lanzar una excepción
            var existingEntity = await _repository.GetByIdAsNoTrackingAsync(id) ?? throw new ArgumentException($"Entity with id {id} not found");

            // Actualizar las propiedades de la entidad existente con los valores de la solicitud
            var updatedEntity = UpdateProperties(existingEntity, request);

            // Validar la entidad actualizada
            if (!IsValid(updatedEntity))
            {
                throw new ValidationException("Argument is invalid.");
            }

            // Actualizar la entidad en el repositorio
            _repository.Update(updatedEntity);
            await _unitOfWork.CommitAsync();

            // Mapear la entidad actualizada a un objeto de respuesta y retornarlo
            var response = _mapper.Map<TEntity, TResponse>(updatedEntity);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    /// <summary>
    /// Elimina una entidad de tipo <typeparamref name="TResponse"/> por su identificador único de manera asincrónica.
    /// </summary>
    /// <param name="id">El identificador único de la entidad.</param>
    /// <returns>Un objeto de tipo <typeparamref name="TResponse"/> que indica el resultado de la eliminación.</returns>
    public virtual async Task<TResponse> DeleteAsync(Guid id)
    {
        try
        {
            // Obtener la entidad por su ID. Si no se encuentra, lanzar una excepción
            var entity = await _repository.GetByIdAsync(id) ?? throw new ArgumentNullException($"Entity with id {id} not found");

            // Eliminar la entidad del repositorio
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();

            // Mapear la entidad eliminada a un objeto de respuesta y retornarlo
            var response = _mapper.Map<TEntity, TResponse>(entity);
            response.Success = true;
            return response;
        }
        catch (Exception ex)
        {
            // Manejar excepciones inesperadas y generar una respuesta de error
            return HandleException(ex);
        }
    }

    // Métodos abstractos que deben ser implementados por las clases derivadas

    /// <summary>
    /// Actualiza las propiedades de una entidad existente de tipo <typeparamref name="TEntity"/> con los valores de una nueva entidad de tipo <typeparamref name="TRequest"/>.
    /// </summary>
    /// <param name="existingEntity">La entidad existente que se va a actualizar.</param>
    /// <param name="request">La entidad de tipo <typeparamref name="TRequest"/> con los nuevos valores.</param>
    /// <returns>La entidad de tipo <typeparamref name="TEntity"/> actualizada.</returns>
    protected abstract TEntity UpdateProperties(TEntity existingEntity, TRequest request);

    /// <summary>
    /// Verifica si una entidad de tipo <typeparamref name="TEntity"/> es válida.
    /// </summary>
    /// <param name="entity">La entidad de tipo <typeparamref name="TEntity"/> que se va a validar.</param>
    /// <returns>True si la entidad de tipo <typeparamref name="TEntity"/> es válida, de lo contrario False.</returns>
    protected abstract bool IsValid(TEntity entity);
}