using CoWorkingApp.Core.Application.Abstracts;
using CoWorkingApp.Core.Application.Contracts.Requests;
using CoWorkingApp.Core.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.API.Infrastructure.Presentation.Controllers
{
    /// <summary>
    /// Clase base genérica para controladores API que utiliza servicios genéricos.
    /// </summary>
    /// <typeparam name="TService">Tipo del servicio que implementa la interfaz <see cref="IService{TRequest, TResponse}"/>.</typeparam>
    /// <typeparam name="TRequest">Tipo de la solicitud que implementa la interfaz <see cref="IRequest"/>.</typeparam>
    /// <typeparam name="TResponse">Tipo de la respuesta que hereda de <see cref="ResponseMessage"/>.</typeparam>
    [ApiController]
    public class ControllerGeneric<TService, TRequest, TResponse> : ControllerBase
        where TService : IService<TRequest, TResponse>  // Restricción para el tipo de servicio
        where TRequest : IRequest                       // Restricción para el tipo de solicitud
        where TResponse : ResponseMessage, new()        // Restricción para el tipo de respuesta
    {
        /// <summary>
        /// Instancia del servicio utilizada por el controlador.
        /// </summary>
        protected readonly TService _service;

        /// <summary>
        /// Instancia del logger utilizada para el registro de eventos y errores.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Constructor del controlador genérico.
        /// </summary>
        /// <param name="service">Instancia del servicio.</param>
        /// <param name="logger">Instancia del logger.</param>
        public ControllerGeneric(TService? service, ILogger? logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Maneja las excepciones y construye una respuesta de error coherente.
        /// </summary>
        protected TResponse HandleException(Exception ex)
        {
            return ResponseMessage.HandleException<TResponse>(ex);
        }

        /// <summary>
        /// Obtiene todos los elementos y devuelve una lista de entidades o un mensaje de error si ocurre un error.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TResponse>>> GetAll()
        {
            try
            {
                // Intenta obtener todas las entidades desde el servicio
                var entities = await _service.GetAllAsync();

                // Verifica si alguna de las respuestas no fue exitosa
                if (entities.Any(e => !e.Success))
                {
                    // Si hay respuestas no exitosas, se construye una lista de respuestas de error
                    var errorResponse = entities.Where(e => !e.Success).ToList();
                    // Se actualiza el mensaje de cada respuesta de error y se agregan los mensajes de error a la lista de errores
                    errorResponse.ForEach(response =>
                    {
                        response.Message = "Error occurred while retrieving entity";
                        response.Errors.Add(response.Message);
                    });

                    // Se devuelve un estado de error con las respuestas de error
                    return StatusCode(500, errorResponse);
                }

                // Si todas las respuestas fueron exitosas, se devuelve un estado OK con las entidades recuperadas
                return Ok(entities);
            }
            catch (Exception)
            {
                // Maneja cualquier otra excepción inesperada
                var exception = new Exception("An unexpected error occurred while retrieving all entities");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID de manera asincrónica.
        /// </summary>
        /// <param name="id">ID de la entidad a buscar.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<TResponse>> GetById(Guid id)
        {
            try
            {
                // Llama al servicio para obtener la entidad por su ID
                var entity = await _service.GetByIdAsync(id);

                if (!entity.Success)
                {
                    // Maneja el caso específico de que la entidad no se encuentre
                    entity.Message = "Entity not found";
                    entity.Errors.Add(entity.Message);

                    return NotFound(entity);
                }

                return Ok(entity);
            }
            catch (Exception)
            {
                // Maneja cualquier otra excepción inesperada
                var exception = new Exception("Error retrieving entity by ID");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }

        /// <summary>
        /// Crea una nueva entidad de manera asincrónica.
        /// </summary>
        /// <param name="entityRequest">Entidad a crear.</param>
        [HttpPost]
        public async Task<ActionResult<TResponse>> Create([FromBody] TRequest entityRequest)
        {
            try
            {
                // Llama al servicio para crear una nueva entidad
                var entity = await _service.CreateAsync(entityRequest);

                if (!entity.Success)
                {
                    entity.Message = "Error occurred while creating the entity.";
                    entity.Errors.Add(entity.Message);

                    return BadRequest(entity);
                }

                return StatusCode(201, entity);
            }
            catch (Exception)
            {
                // Maneja cualquier otra excepción inesperada
                var exception = new Exception("An unexpected error occurred while creating the entity.");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }

        /// <summary>
        /// ACTualiza una entidad existente de manera asincrónica.
        /// </summary>
        /// <param name="id">ID de la entidad a actualizar.</param>
        /// <param name="entityRequest">Datos actualizados de la entidad.</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<TResponse>> Update(Guid id, [FromBody] TRequest entityRequest)
        {
            try
            {
                // Llama al servicio para actualizar la entidad
                var response = await _service.UpdateAsync(id, entityRequest);

                if (!response.Success)
                {
                    response.Message = "Error occurred while updating the entity.";
                    response.Errors.Add(response.Message);
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                // Maneja cualquier otra excepción inesperada
                var exception = new Exception("An unexpected error occurred while updating the entity.");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }


        /// <summary>
        /// Elimina una entidad por su ID de manera asincrónica.
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TResponse>> Delete(Guid id)
        {
            try
            {
                // Llama al servicio para eliminar la entidad por su ID
                var response = await _service.DeleteAsync(id);

                if (!response.Success)
                {
                    response.Message = "Error occurred while deleting the entity.";
                    response.Errors.Add(response.Message);
                    return BadRequest(response);
                }

                return StatusCode(204, response);
            }
            catch (Exception)
            {
                // Maneja cualquier otra excepción inesperada
                var exception = new Exception("An unexpected error occurred while deleting the entity.");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }


    }
}
