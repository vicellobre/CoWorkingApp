using Asp.Versioning;
using CoWorkingApp.Application.Users.Commands.DeleteUser;
using CoWorkingApp.Application.Users.Queries.GetUserByEmail;
using CoWorkingApp.Application.Users.Queries.GetUserById;
using CoWorkingApp.Application.Users.Queries.GetUsers;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.Users.V2.Extensions;
using CoWorkingApp.Presentation.Users.V2.Models.CreateUser;
using CoWorkingApp.Presentation.Users.V2.Models.GetUser;
using CoWorkingApp.Presentation.Users.V2.Models.GetUsers;
using CoWorkingApp.Presentation.Users.V2.Models.LoginUser;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUser;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserEmail;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserName;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;

namespace CoWorkingApp.Presentation.Users.V2.Controllers;

/// <summary>
/// Controlador para manejar las operaciones relacionadas con usuarios en la versión 2 de la API.
/// </summary>
[ApiController]
[ApiVersion(2)]
[Route("api/v{v:apiVersion}/users")]
public class UserController : ApiController
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserController"/>.
    /// </summary>
    /// <param name="sender">El <see cref="ISender"/> utilizado para enviar solicitudes.</param>
    /// <param name="logger">El <see cref="ILogger{UserController}"/> utilizado para registrar eventos y mensajes de diagnóstico.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el <paramref name="sender"/> o el <paramref name="logger"/> es <see langword="null"/>.</exception>
    public UserController(ISender sender, ILogger<UserController> logger) : base(sender, logger) { }

    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    /// <returns>Una lista de todos los usuarios.</returns>
    [HttpGet()]
    [EnableQuery]
    [AllowAnonymous]
    public async Task<ActionResult<GetUsersResponse>> GetAll()
    {
        GetUsersQuery query = new();

        var result = await _sender.Send(query);

        var response = result.Value.ToGetUsersResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetUsersResponse>(result));
    }

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <returns>Los detalles del usuario.</returns>
    [HttpGet("{id:guid}")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<GetUserResponse>> GetById(Guid id)
    {
        GetUserByIdQuery query = new(id);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetUserResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetUserResponse>(result));
    }

    /// <summary>
    /// Obtiene un usuario por su correo electrónico.
    /// </summary>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <returns>Los detalles del usuario.</returns>
    [HttpGet("email/{email}")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<GetUserResponse>> GetByEmail(string email)
    {
        GetUserByEmailQuery query = new(email);

        var result = await _sender.Send(query);

        var response = result.Value.ToGetUserResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<GetUserResponse>(result));
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="request">Los datos del usuario a crear.</param>
    /// <returns>El resultado de la operación de creación.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
    {
        var command = request.ToCreateUserCommand();

        var result = await _sender.Send(command);

        var response = result.Value.ToCreateUserResponse();

        return result.Match(
            onSuccess: _ => CreatedAtAction(nameof(GetById), new { id = response.Id }, response),
            onFailure: _ => Problem<CreateUserResponse>(result));
    }

    /// <summary>
    /// Permite que los usuarios se autentiquen y obtengan un token JWT.
    /// </summary>
    /// <param name="request">La solicitud de inicio de sesión del usuario.</param>
    /// <returns>Un <see cref="ActionResult{LoginUserResponse}"/> que contiene la información de la respuesta de inicio de sesión del usuario y el token JWT.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginUserResponse>> Login([FromBody] LoginUserRequest request)
    {
        var command = request.ToAuthenticateUserCommand();

        var result = await _sender.Send(command);

        var response = result.Value.ToLoginUserResponse();

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: _ => Problem<LoginUserResponse>(result));
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">Los datos del usuario a actualizar.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var command = request.ToUpdateUserCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ => NoContent(),
            onFailure: _ => Problem(result));
    }

    /// <summary>
    /// Actualiza el nombre de un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">Los nuevos nombres del usuario.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id:guid}/name")]
    public async Task<ActionResult> UpdateName(Guid id, [FromBody] UpdateUserNameRequest request)
    {
        var command = request.ToUpdateUserNameCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: NoContent,
            onFailure: _ => Problem(result));
    }

    /// <summary>
    /// Actualiza el correo electrónico de un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">La solicitud de actualización del correo electrónico del usuario.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id:guid}/email")]
    public async Task<ActionResult> UpdateEmail(Guid id, [FromBody] UpdateUserEmailRequest request)
    {
        var command = request.ToUpdateUserEmailCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: NoContent,
            onFailure: _ => Problem(result));
    }

    /// <summary>
    /// Actualiza la contraseña de un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">La solicitud de actualización de la contraseña del usuario.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id:guid}/password")]
    public async Task<ActionResult> UpdatePassword(Guid id, [FromBody] UpdateUserPasswordRequest request)
    {
        var command = request.ToUpdateUserPasswordCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: NoContent,
            onFailure: _ => Problem(result));
    }

    /// <summary>
    /// Elimina un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <returns>El resultado de la operación de eliminación.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ => NoContent(),
            onFailure: _ => Problem(result));
    }
}
