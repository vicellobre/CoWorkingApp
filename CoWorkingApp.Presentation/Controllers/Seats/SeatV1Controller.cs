using CoWorkingApp.Application.Seats.Commands.CreateSeat;
using CoWorkingApp.Application.Seats.Commands.DeleteSeat;
using CoWorkingApp.Application.Seats.Commands.UpdateSeat;
using CoWorkingApp.Application.Seats.Queries.GetAllSeats;
using CoWorkingApp.Application.Seats.Queries.GetSeatById;
using CoWorkingApp.Application.Seats.Queries.GetSeatByName;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.DTOs.Seats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace CoWorkingApp.Presentation.Controllers.Seats;

/// <summary>
/// Controlador para manejar las operaciones relacionadas con los asientos en la versión 1 de la API.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/v1/seats")]
public class SeatV1Controller : ApiController
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SeatV1Controller"/>.
    /// </summary>
    /// <param name="sender">El <see cref="ISender"/> utilizado para enviar solicitudes.</param>
    public SeatV1Controller(ISender sender) : base(sender) { }

    /// <summary>
    /// Obtiene todos los asientos.
    /// </summary>
    /// <returns>Una lista de todos los asientos.</returns>
    [HttpGet]
    [EnableQuery]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            GetAllSeatsQuery query = new();

            var response = await _sender.Send(query);

            return response.IsSuccess
                ? Ok(response.Value)
                : BadRequest(response.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                Result.Failure(ex));
        }
    }

    /// <summary>
    /// Obtiene un asiento por su ID.
    /// </summary>
    /// <param name="id">El ID del asiento.</param>
    /// <returns>El asiento con el ID especificado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            GetSeatByIdQuery query = new(id);

            var response = await _sender.Send(query);

            return response.IsSuccess
                ? Ok(response.Value)
                : NotFound(response.FirstError);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                Result.Failure(ex));
        }
    }

    /// <summary>
    /// Obtiene un asiento por su nombre.
    /// </summary>
    /// <param name="name">El nombre del asiento.</param>
    /// <returns>El asiento con el nombre especificado.</returns>
    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        try
        {
            GetSeatByNameQuery query = new(name);

            var response = await _sender.Send(query);

            return response.IsSuccess
                ? Ok(response) : NotFound(response.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                Result.Failure(ex));
        }
    }

    //[HttpGet("availables")]
    //[AllowAnonymous]
    //public async Task<IActionResult> GetAvailables()
    //{
    //    try
    //    {
    //        GetAvailableSeatsQuery query = new();

    //        var response = await _sender.Send(query);

    //        return response.IsSuccess
    //            ? Ok(response) : NotFound(response.Errors);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(
    //            StatusCodes.Status500InternalServerError,
    //            Result.Failure(ex));
    //    }
    //}

    /// <summary>
    /// Crea un nuevo asiento.
    /// </summary>
    /// <param name="request">Los datos del asiento a crear.</param>
    /// <returns>El resultado de la operación de creación.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SeatRequest request)
    {
        try
        {
            CreateSeatCommand command = new(
                request.Name!,
                request.Description!);

            var response = await _sender.Send(command);

            return response.IsSuccess
                ? Ok(response.Value)
                : BadRequest(response.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                Result.Failure(ex));
        }
    }

    /// <summary>
    /// Actualiza un asiento existente.
    /// </summary>
    /// <param name="id">El ID del asiento a actualizar.</param>
    /// <param name="request">Los nuevos datos del asiento.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SeatRequest request)
    {
        try
        {
            UpdateSeatCommand command = new(
                id,
                request.Name!,
                request.Description!);

            var response = await _sender.Send(command);

            return response.IsSuccess
                ? Ok(response.Value)
                : BadRequest(response.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                Result.Failure(ex));
        }
    }

    /// <summary>
    /// Elimina un asiento por su ID.
    /// </summary>
    /// <param name="id">El ID del asiento a eliminar.</param>
    /// <returns>El resultado de la operación de eliminación.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            DeleteSeatCommand command = new(id);

            var response = await _sender.Send(command);

            return response.IsSuccess
                ? Ok(response.Value)
                : BadRequest(response.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                Result.Failure(ex));
        }
    }
}
