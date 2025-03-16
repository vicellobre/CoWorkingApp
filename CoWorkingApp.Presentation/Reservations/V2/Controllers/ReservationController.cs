using Asp.Versioning;
using CoWorkingApp.Application.Reservations.Commands.CreateReservation;
using CoWorkingApp.Application.Reservations.Commands.DeleteReservation;
using CoWorkingApp.Application.Reservations.Queries.GetReservationById;
using CoWorkingApp.Application.Reservations.Queries.GetReservations;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatId;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserId;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.Reservations.V2.Extensions;
using CoWorkingApp.Presentation.Reservations.V2.Models.CreateReservation;
using CoWorkingApp.Presentation.Reservations.V2.Models.GetReservation;
using CoWorkingApp.Presentation.Reservations.V2.Models.GetReservations;
using CoWorkingApp.Presentation.Reservations.V2.Models.UpdateReservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;

namespace CoWorkingApp.Presentation.Reservations.V2.Controllers;

/// <summary>
/// Controlador para gestionar las reservas.
/// </summary>
[ApiController]
[AllowAnonymous]
[ApiVersion(2)]
[Route("api/v{v:apiVersion}/reservations")]
public class ReservationController : ApiController
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ReservationController"/>.
    /// </summary>
    /// <param name="sender">El objeto ISender para enviar comandos y consultas.</param>
    /// <param name="logger">El objeto ILogger para registrar información.</param>
    public ReservationController(ISender sender, ILogger<ReservationController> logger) : base(sender, logger) { }

    /// <summary>
    /// Obtiene todas las reservas.
    /// </summary>
    /// <returns>Una lista de todas las reservas.</returns>
    [HttpGet]
    [EnableQuery]
    [AllowAnonymous]
    public async Task<ActionResult<GetReservationsResponse>> GetAll()
    {
        GetReservationsQuery query = new();

        var result = await _sender.Send(query);

        var response = result.Value.ToGetReservationsResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetReservationsResponse>(result));
    }

    /// <summary>
    /// Obtiene una reserva por su identificador.
    /// </summary>
    /// <param name="id">El identificador de la reserva.</param>
    /// <returns>La reserva correspondiente al identificador proporcionado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<GetReservationResponse>> GetById(Guid id)
    {
        GetReservationByIdQuery query = new(id);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetReservationResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetReservationResponse>(result));
    }

    /// <summary>
    /// Obtiene las reservas por fecha.
    /// </summary>
    /// <param name="date">La fecha de las reservas.</param>
    /// <returns>Una lista de reservas para la fecha especificada.</returns>
    [HttpGet("bydate")]
    public async Task<ActionResult<GetReservationsResponse>> GetByDate(DateTime date)
    {
        GetReservationsByDateQuery query = new(date);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetReservationsResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetReservationsResponse>(result));
    }

    /// <summary>
    /// Obtiene las reservas por identificador de usuario.
    /// </summary>
    /// <param name="userId">El identificador del usuario.</param>
    /// <returns>Una lista de reservas para el usuario especificado.</returns>
    [HttpGet("byuser/{userId}")]
    public async Task<ActionResult<GetReservationsResponse>> GetByUserId(Guid userId)
    {
        GetReservationsByUserIdQuery query = new(userId);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetReservationsResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetReservationsResponse>(result));
    }

    /// <summary>
    /// Obtiene las reservas por identificador de asiento.
    /// </summary>
    /// <param name="seatId">El identificador del asiento.</param>
    /// <returns>Una lista de reservas para el asiento especificado.</returns>
    [HttpGet("byseat/{seatId}")]
    public async Task<ActionResult<GetReservationsResponse>> GetBySeatId(Guid seatId)
    {
        GetReservationsBySeatIdQuery query = new(seatId);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetReservationsResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetReservationsResponse>(result));
    }

    /// <summary>
    /// Crea una nueva reserva.
    /// </summary>
    /// <param name="request">La solicitud para crear una nueva reserva.</param>
    /// <returns>La respuesta con los detalles de la reserva creada.</returns>
    [HttpPost]
    public async Task<ActionResult<CreateReservationResponse>> Create([FromBody] CreateReservationRequest request)
    {
        var command = request.ToCreateReservationCommand();

        var result = await _sender.Send(command);

        var response = result.Value.ToCreateReservationResponse();

        return result.Match(
            onSuccess: _ => CreatedAtAction(nameof(GetById), new { id = response.ReservationId }, response),
            onFailure: _ => Problem<CreateReservationResponse>(result));
    }

    /// <summary>
    /// Actualiza una reserva existente.
    /// </summary>
    /// <param name="id">El identificador de la reserva a actualizar.</param>
    /// <param name="request">La solicitud con los nuevos datos de la reserva.</param>
    /// <returns>Una respuesta indicando el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateReservationRequest request)
    {
        var command = request.ToUpdateReservationCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ => NoContent(),
            onFailure: error => Problem(result));
    }

    /// <summary>
    /// Elimina una reserva existente.
    /// </summary>
    /// <param name="id">El identificador de la reserva a eliminar.</param>
    /// <returns>Una respuesta indicando el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        DeleteReservationCommand command = new(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: value => NoContent(),
            onFailure: error => Problem(result));
    }
}

