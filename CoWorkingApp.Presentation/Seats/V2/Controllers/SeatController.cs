using Asp.Versioning;
using CoWorkingApp.Application.Seats.Commands.DeleteSeat;
using CoWorkingApp.Application.Seats.Queries.GetSeatById;
using CoWorkingApp.Application.Seats.Queries.GetSeatByName;
using CoWorkingApp.Application.Seats.Queries.GetSeats;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.Seats.V2.Extensions;
using CoWorkingApp.Presentation.Seats.V2.Models.CreateSeat;
using CoWorkingApp.Presentation.Seats.V2.Models.GetSeat;
using CoWorkingApp.Presentation.Seats.V2.Models.GetSeats;
using CoWorkingApp.Presentation.Seats.V2.Models.UpdateSeat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;

namespace CoWorkingApp.Presentation.Seats.V2.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con los asientos.
/// </summary>
[ApiController]
[AllowAnonymous]
[ApiVersion(2)]
[Route("api/v{v:apiVersion}/seats")]
public class SeatController : ApiController
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SeatController"/>.
    /// </summary>
    /// <param name="sender">El mediador para enviar comandos y consultas.</param>
    /// <param name="logger">El logger para registrar eventos.</param>
    public SeatController(ISender sender, ILogger<SeatController> logger) : base(sender, logger) { }

    /// <summary>
    /// Obtiene todos los asientos.
    /// </summary>
    /// <returns>Una lista de todos los asientos.</returns>
    [HttpGet]
    [EnableQuery]
    [AllowAnonymous]
    public async Task<ActionResult<GetSeatsResponse>> GetAll()
    {
        GetSeatsQuery query = new();

        var result = await _sender.Send(query);

        var response = result.Value.ToGetSeatsResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetSeatsResponse>(result));
    }

    /// <summary>
    /// Obtiene un asiento por su identificador.
    /// </summary>
    /// <param name="id">El identificador del asiento.</param>
    /// <returns>El asiento con el identificador especificado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<GetSeatResponse>> GetById(Guid id)
    {
        GetSeatByIdQuery query = new(id);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetSeatResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetSeatResponse>(result));
    }

    /// <summary>
    /// Obtiene un asiento por su nombre.
    /// </summary>
    /// <param name="name">El nombre del asiento.</param>
    /// <returns>El asiento con el nombre especificado.</returns>
    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<GetSeatResponse>> GetByName(string name)
    {
        GetSeatByNameQuery query = new(name);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetSeatResponse();

        return result.Match(
            onSuccess: value => Ok(response),
            onFailure: error => Problem<GetSeatResponse>(result));
    }

    /// <summary>
    /// Crea un nuevo asiento.
    /// </summary>
    /// <param name="request">La solicitud para crear un nuevo asiento.</param>
    /// <returns>El asiento creado.</returns>
    [HttpPost]
    public async Task<ActionResult<CreateSeatResponse>> Create([FromBody] CreateSeatRequest request)
    {
        var command = request.ToCreateSeatCommand();

        var result = await _sender.Send(command);

        var response = result.Value.ToCreateSeatResponse();

        return result.Match(
            onSuccess: _ => CreatedAtAction(nameof(GetById), new { id = response.Id }, response),
            onFailure: _ => Problem<CreateSeatResponse>(result));
    }

    /// <summary>
    /// Actualiza un asiento existente.
    /// </summary>
    /// <param name="id">El identificador del asiento.</param>
    /// <param name="request">La solicitud para actualizar el asiento.</param>
    /// <returns>Una respuesta indicando el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateSeatRequest request)
    {
        var command = request.ToUpdateSeatCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ => NoContent(),
            onFailure: _ => Problem(result));
    }

    /// <summary>
    /// Elimina un asiento por su identificador.
    /// </summary>
    /// <param name="id">El identificador del asiento.</param>
    /// <returns>Una respuesta indicando el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        DeleteSeatCommand command = new(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ => NoContent(),
            onFailure: _ => Problem(result));
    }
}
