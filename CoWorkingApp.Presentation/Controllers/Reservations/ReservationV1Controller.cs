using CoWorkingApp.Application.Reservations.Commands.CreateReservation;
using CoWorkingApp.Application.Reservations.Commands.DeleteReservation;
using CoWorkingApp.Application.Reservations.Commands.UpdateReservation;
using CoWorkingApp.Application.Reservations.Queries.GetAllReservations;
using CoWorkingApp.Application.Reservations.Queries.GetReservationById;
using CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.DTOs.Reservations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace CoWorkingApp.Presentation.Controllers.Reservations;

/// <summary>
/// Controlador para manejar las operaciones relacionadas con las reservas en la versión 1 de la API.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/v1/reservations")]
public class ReservationV1Controller : ApiController
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ReservationV1Controller"/>.
    /// </summary>
    /// <param name="sender">El <see cref="ISender"/> utilizado para enviar solicitudes.</param>
    public ReservationV1Controller(ISender sender) : base(sender) { }

    /// <summary>
    /// Obtiene todas las reservas.
    /// </summary>
    /// <returns>Una lista de todas las reservas.</returns>
    [HttpGet]
    [EnableQuery]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        GetAllReservationsQuery query = new();

        var response = await _sender.Send(query);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Obtiene una reserva por su ID.
    /// </summary>
    /// <param name="id">El ID de la reserva.</param>
    /// <returns>La reserva con el ID especificado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetReservationByIdQuery query = new(id);

        var response = await _sender.Send(query);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Obtiene las reservas por fecha.
    /// </summary>
    /// <param name="date">La fecha de las reservas.</param>
    /// <returns>Las reservas en la fecha especificada.</returns>
    [HttpGet("bydate")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        GetReservationsByDateQuery query = new(date);

        var response = await _sender.Send(query);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="userId"></param>
    ///// <returns></returns>
    //[HttpGet("byuser/{userId}")]
    //public async Task<IActionResult> GetByUserId(Guid userId)
    //{
    //        GetReservationsByUserIdQuery query = new(userId);

    //        var response = await _sender.Send(query);

    //        return response.IsSuccess
    //            ? Ok(response.Value)
    //            : NotFound(response.FirstError);
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="seatId"></param>
    ///// <returns></returns>
    //[HttpGet("byseat/{seatId}")]
    //public async Task<IActionResult> GetBySeatId(Guid seatId)
    //{
    //        GetReservationsBySeatIdQuery query = new(seatId);

    //        var response = await _sender.Send(query);

    //        return response.IsSuccess
    //            ? Ok(response.Value)
    //            : NotFound(response.FirstError);
    //}

    /// <summary>
    /// Crea una nueva reserva.
    /// </summary>
    /// <param name="request">Los datos de la reserva a crear.</param>
    /// <returns>El resultado de la operación de creación.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReservationRequest request)
    {
        CreateReservationCommand command = new(
            request.Date,
            request.UserId,
            request.SeatId);

        var response = await _sender.Send(command);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Actualiza una reserva existente.
    /// </summary>
    /// <param name="id">El ID de la reserva a actualizar.</param>
    /// <param name="request">Los nuevos datos de la reserva.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ReservationRequest request)
    {
        UpdateReservationCommand command = new(
            id,
            request.Date,
            request.UserId,
            request.SeatId);

        var response = await _sender.Send(command);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Elimina una reserva por su ID.
    /// </summary>
    /// <param name="id">El ID de la reserva a eliminar.</param>
    /// <returns>El resultado de la operación de eliminación.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        DeleteReservationCommand command = new(id);

        var response = await _sender.Send(command);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }
}
