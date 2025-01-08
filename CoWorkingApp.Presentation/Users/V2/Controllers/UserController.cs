using Asp.Versioning;
using CoWorkingApp.Application.Users.Commands.AuthenticateUser;
using CoWorkingApp.Application.Users.Commands.CreateUser;
using CoWorkingApp.Application.Users.Commands.DeleteUser;
using CoWorkingApp.Application.Users.Commands.UpdateUserEmail;
using CoWorkingApp.Application.Users.Commands.UpdateUserPassword;
using CoWorkingApp.Application.Users.Queries.GetAllUsers;
using CoWorkingApp.Application.Users.Queries.GetUserByEmail;
using CoWorkingApp.Application.Users.Queries.GetUserById;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.Users.V2.Models.CreateUser;
using CoWorkingApp.Presentation.Users.V2.Models.GetallUsers;
using CoWorkingApp.Presentation.Users.V2.Models.GetUser;
using CoWorkingApp.Presentation.Users.V2.Models.LoginUser;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUser;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserEmail;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserName;
using CoWorkingApp.Presentation.Users.V2.Models.UpdateUserPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Presentation.Users.V2.Controllers;

/// <summary>
/// Controlador para manejar las operaciones relacionadas con usuarios en la versión 1 de la API.
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
    public UserController(ISender sender) : base(sender) { }

    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    /// <returns>Una lista de todos los usuarios.</returns>
    [HttpGet()]
    public async Task<ActionResult<GetAllUsersResponse>> GetAll()
    {
        GetAllUsersQuery query = new();

        var result = await _sender.Send(query);

        var response = (GetAllUsersResponse)result.Value;

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: error => Problem<GetAllUsersResponse>(result));
    }

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <returns>Los detalles del usuario.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserResponse>> GetById(Guid id)
    {
        GetUserByIdQuery query = new(id);

        var result = await _sender.Send(query);

        var response = (GetUserResponse)result.Value;

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: error => Problem<GetUserResponse>(result));
    }

    /// <summary>
    /// Obtiene un usuario por su correo electrónico.
    /// </summary>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <returns>Los detalles del usuario.</returns>
    [HttpGet("email/{email}")]
    public async Task<ActionResult<GetUserResponse>> GetByEmail(string email)
    {
        GetUserByEmailQuery query = new(email);

        var result = await _sender.Send(query);

        var response = (GetUserResponse)result.Value;

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: error => Problem<GetUserResponse>(result));
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
        var command = (CreateUserCommand)request;

        var result = await _sender.Send(command);

        var response = (CreateUserResponse)result.Value;

        return result.Match(
            onSuccess: _ =>
            {
                var uri = Url.Action(nameof(GetById), new { id = response.UserId });
                return Created(uri, response);
            },
            onFailure: error => Problem<CreateUserResponse>(result));
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
        var command = (AuthenticateUserCommand)request;

        var result = await _sender.Send(command);

        var response = (LoginUserResponse)result.Value;

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: error => Problem<LoginUserResponse>(result));
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">Los datos del usuario a actualizar.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var command = request.ToUpdateUserCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ => NoContent(),
            onFailure: error => Problem(result));
    }

    /// <summary>
    /// Actualiza el nombre de un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">Los nuevos nombres del usuario.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id}/name")]
    public async Task<ActionResult> UpdateName(Guid id, [FromBody] UpdateUserNameRequest request)
    {
        var command = request.ToUpdateUserNameCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: NoContent,
            onFailure: error => Problem(result));
    }

    /// <summary>
    /// Actualiza el correo electrónico de un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">La solicitud de actualización del correo electrónico del usuario.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id}/email")]
    public async Task<ActionResult> UpdateEmail(Guid id, [FromBody] UpdateUserEmailRequest request)
    {
        UpdateUserEmailCommand command = request.ToUpdateUserEmailCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: NoContent,
            onFailure: error => Problem(result));
    }

    /// <summary>
    /// Actualiza la contraseña de un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <param name="request">La solicitud de actualización de la contraseña del usuario.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id}/password")]
    public async Task<ActionResult> UpdatePassword(Guid id, [FromBody] UpdateUserPasswordRequest request)
    {
        UpdateUserPasswordCommand command = request.ToUpdateUserPasswordCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: NoContent,
            onFailure: error => Problem(result));
    }

    /// <summary>
    /// Elimina un usuario con el identificador especificado.
    /// </summary>
    /// <param name="id">El identificador del usuario a eliminar.</param>
    /// <returns>El resultado de la operación de eliminación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        DeleteUserCommand command = new(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ => NoContent(),
            onFailure: error => Problem(result));
    }
}
