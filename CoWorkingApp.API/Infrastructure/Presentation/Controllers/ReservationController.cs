using CoWorkingApp.Core.Application.Contracts.Services;
using CoWorkingApp.Core.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.API.Infrastructure.Presentation.Controllers
{
    /// <summary>
    /// Controlador para las operaciones relacionadas con la entidad Reservation.
    /// </summary>
    [ApiController]
    [Route("api/[controller]s")] // Se utiliza el plural "reservations" en la ruta para seguir convenciones RESTful
    public class ReservationController : ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>
    {
        /// <summary>
        /// Constructor de la clase ReservationController.
        /// </summary>
        /// <param name="service">Instancia del servicio de reservaciones.</param>
        /// <param name="logger">Instancia del logger.</param>
        public ReservationController(IReservationService? service, ILogger<ControllerGeneric<IReservationService, ReservationRequest, ReservationResponse>>? logger) : base(service, logger) { }

        /// <summary>
        /// Obtiene reservaciones por fecha.
        /// </summary>
        /// <param name="date">Fecha de las reservaciones a obtener.</param>
        /// <returns>ActionResult con la colección de reservaciones correspondientes a la fecha o un error interno.</returns>
        [HttpGet("bydate")]
        public virtual async Task<ActionResult<IEnumerable<ReservationResponse>>> GetByDate([FromQuery] DateTime date)
        {
            try
            {
                var reservations = await _service.GetByDateAsync(date);

                if (reservations.Any(e => !e.Success))
                {
                    // Si hay respuestas no exitosas, se construye una lista de respuestas de error
                    var errorResponse = reservations.Where(e => !e.Success).ToList();
                    // Se actualiza el mensaje de cada respuesta de error y se agregan los mensajes de error a la lista de errores
                    errorResponse.ForEach(response =>
                    {
                        response.Message = "Error occurred while retrieving reservations";
                        response.Errors.Add(response.Message);
                    });

                    // Se devuelve un estado de error con las respuestas de error
                    return NotFound(errorResponse);
                }

                return Ok(reservations);
            }
            catch (Exception)
            {
                // Maneja cualquier excepción inesperada
                var exception = new Exception("An unexpected error occurred while getting reservations.");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }

        /// <summary>
        /// Obtiene reservaciones por ID de usuario.
        /// </summary>
        /// <param name="userId">ID del usuario para el cual se obtendrán las reservaciones.</param>
        /// <returns>ActionResult con la colección de reservaciones correspondientes al usuario o un error interno.</returns>
        [HttpGet("byuser/{userId}")]
        public virtual async Task<ActionResult<IEnumerable<ReservationResponse>>> GetByUserId(Guid userId)
        {
            try
            {
                var reservations = await _service.GetByUserIdAsync(userId);

                if (reservations.Any(e => !e.Success))
                {
                    // Si hay respuestas no exitosas, se construye una lista de respuestas de error
                    var errorResponse = reservations.Where(e => !e.Success).ToList();
                    // Se actualiza el mensaje de cada respuesta de error y se agregan los mensajes de error a la lista de errores
                    errorResponse.ForEach(response =>
                    {
                        response.Message = "Error occurred while retrieving reservation";
                        response.Errors.Add(response.Message);
                    });

                    // Se devuelve un estado de error con las respuestas de error
                    return NotFound(errorResponse);
                }

                return Ok(reservations);
            }
            catch (Exception)
            {
                // Maneja cualquier excepción inesperada
                var exception = new Exception("An unexpected error occurred while getting reservations.");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }

        /// <summary>
        /// Obtiene reservaciones por ID de asiento.
        /// </summary>
        /// <param name="seatId">ID del asiento para el cual se obtendrán las reservaciones.</param>
        /// <returns>ActionResult con la colección de reservaciones correspondientes al asiento o un error interno.</returns>
        [HttpGet("byseat/{seatId}")]
        public virtual async Task<ActionResult<IEnumerable<ReservationResponse>>> GetBySeatId(Guid seatId)
        {
            try
            {
                var reservations = await _service.GetBySeatIdAsync(seatId);

                if (reservations.Any(e => !e.Success))
                {
                    // Si hay respuestas no exitosas, se construye una lista de respuestas de error
                    var errorResponse = reservations.Where(e => !e.Success).ToList();
                    // Se actualiza el mensaje de cada respuesta de error y se agregan los mensajes de error a la lista de errores
                    errorResponse.ForEach(response =>
                    {
                        response.Message = "Error occurred while retrieving reservation";
                        response.Errors.Add(response.Message);
                    });

                    // Se devuelve un estado de error con las respuestas de error
                    return NotFound(errorResponse);
                }

                return Ok(reservations);
            }
            catch (Exception)
            {
                // Maneja cualquier excepción inesperada
                var exception = new Exception("An unexpected error occurred while getting reservations.");
                _logger.LogError(exception, exception.Message);
                return StatusCode(500, HandleException(exception));
            }
        }
    }
}
