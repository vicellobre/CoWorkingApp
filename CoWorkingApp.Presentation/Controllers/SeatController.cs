using CoWorkingApp.Application.Seats.Services.Contracts;
using CoWorkingApp.Application.Seats.Services.DTOs;
using CoWorkingApp.Presentation.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoWorkingApp.Presentation.Controllers;

/// <summary>
/// Controlador para las operaciones relacionadas con la entidad Seat.
/// </summary>
[ApiController]
//[ApiExplorerSettings(GroupName = "v2")]
[Route("api/[controller]s")] // Se utiliza el plural "seats" en la ruta para seguir convenciones RESTful
public class SeatController : ControllerGeneric<ISeatService, SeatServiceRequest, SeatServiceResponse>
{
    /// <summary>
    /// Constructor de la clase SeatController.
    /// </summary>
    /// <param name="service">Instancia del servicio de asientos.</param>
    /// <param name="logger">Instancia del logger.</param>
    public SeatController(ISeatService? service, ILogger<ControllerGeneric<ISeatService, SeatServiceRequest, SeatServiceResponse>>? logger) : base(service, logger) { }

    /// <summary>
    /// Obtiene los asientos disponibles.
    /// </summary>
    /// <returns>ActionResult con la colección de asientos disponibles o un error interno.</returns>
    [HttpGet("availables")]
    [AllowAnonymous] // Se permite acceso anónimo para obtener los asientos disponibles
    public async Task<ActionResult<IEnumerable<SeatServiceResponse>>> GetAvailables()
    {
        try
        {
            // Llama al servicio para obtener los asientos disponibles
            var seats = await _service.GetAvailablesAsync();

            if (seats.Any(e => !e.Success))
            {
                // Si hay respuestas no exitosas, se construye una lista de respuestas de error
                var errorResponse = seats.Where(e => !e.Success).ToList();
                // Se actualiza el mensaje de cada respuesta de error y se agregan los mensajes de error a la lista de errores
                errorResponse.ForEach(response =>
                {
                    response.Message = "Error occurred while retrieving seat";
                    response.Errors.Add(response.Message);
                });

                // Se devuelve un estado de error con las respuestas de error
                return NotFound(errorResponse);
            }

            return Ok(seats);
        }
        catch (Exception)
        {
            // Maneja cualquier excepción inesperada
            var exception = new Exception("An unexpected error occurred while getting available seats.");
            _logger.LogError(exception, exception.Message);
            return StatusCode(500, HandleException(exception));
        }
    }

    /// <summary>
    /// Obtiene asientos por nombre.
    /// </summary>
    /// <param name="name">Nombre de los asientos a obtener.</param>
    /// <returns>ActionResult con la colección de asientos correspondientes al nombre o un error interno.</returns>
    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<SeatServiceResponse>> GetByName(string name)
    {
        try
        {
            // Llama al servicio para obtener los asientos por nombre
            var seat = await _service.GetByNameAsync(name);

            if (!seat.Success)
            {
                // Maneja el caso específico de que la entidad no se encuentre
                seat.Message = "Entity not found by name";
                seat.Errors.Add(seat.Message);

                return NotFound(seat);
            }

            return Ok(seat);
        }
        catch (Exception)
        {
            // Maneja cualquier otra excepción inesperada
            var exception = new Exception("An unexpected error occurred while getting seat by name.");
            _logger.LogError(exception, exception.Message);
            return StatusCode(500, HandleException(exception));
        }
    }

}
