using Asp.Versioning;
using CoWorkingApp.Application.Users.Commands.AuthenticateUser;
using CoWorkingApp.Application.Users.Commands.CreateUser;
using CoWorkingApp.Application.Users.Commands.DeleteUser;
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
    public async Task<ActionResult<GetAllUsersResponse>> GetAll()
    {
        _logger.LogInformation("Starting request to get all users.");

        GetAllUsersQuery query = new();

        var result = await _sender.Send(query);

        var response = (GetAllUsersResponse)result.Value;

        return result.Match(
            onSuccess: _ =>
            {
                _logger.LogInformation("Request to get all users completed successfully. Response: {@Response}", response);
                return Ok(response);
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to get all users failed. Error: {@Error}", result);
                return Problem<GetAllUsersResponse>(result);
            });
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
        _logger.LogInformation("Starting request to get user with ID {UserId}.", id);

        GetUserByIdQuery query = new(id);

        var result = await _sender.Send(query);

        var response = (GetUserResponse)result.Value;

        return result.Match(
            onSuccess: _ =>
            {
                _logger.LogInformation("Request to get user with ID {UserId} completed successfully.", id);
                return Ok(response);
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to get user with ID {UserId} failed.", id);
                return Problem<GetUserResponse>(result);
            });
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
        _logger.LogInformation("Starting request to get user with email {UserEmail}.", email);

        GetUserByEmailQuery query = new(email);

        var result = await _sender.Send(query);

        var response = (GetUserResponse)result.Value;

        return result.Match(
            onSuccess: _ =>
            {
                _logger.LogInformation("Request to get user with email {UserEmail} completed successfully.", email);
                return Ok(response);
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to get user with email {UserEmail} failed.", email);
                return Problem<GetUserResponse>(result);
            });
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
        _logger.LogInformation("Starting request to create a new user.");

        var command = (CreateUserCommand)request;

        var result = await _sender.Send(command);

        var response = (CreateUserResponse)result.Value;

        return result.Match(
            onSuccess: _ =>
            {
                _logger.LogInformation("Request to create a new user completed successfully. UserId: {UserId}", response.UserId);
                var uri = Url.Action(nameof(GetById), new { id = response.UserId });
                return Created(uri, response);
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to create a new user failed.");
                return Problem<CreateUserResponse>(result);
            });
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
        _logger.LogInformation("Starting login request for user with email {UserEmail}.", request.Email);

        var command = (AuthenticateUserCommand)request;

        var result = await _sender.Send(command);

        var response = (LoginUserResponse)result.Value;

        return result.Match(
            onSuccess: _ =>
            {
                _logger.LogInformation("Login request for user with email {UserEmail} completed successfully.", request.Email);
                return Ok(response);
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Login request for user with email {UserEmail} failed.", request.Email);
                return Problem<LoginUserResponse>(result);
            });
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
        _logger.LogInformation("Starting request to update user with ID {UserId}.", id);

        var command = request.ToUpdateUserCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ =>
            {
                _logger.LogInformation("Request to update user with ID {UserId} completed successfully.", id);
                return NoContent();
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to update user with ID {UserId} failed.", id);
                return Problem(result);
            });
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
        _logger.LogInformation("Starting request to update name of user with ID {UserId}.", id);

        var command = request.ToUpdateUserNameCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: () =>
            {
                _logger.LogInformation("Request to update name of user with ID {UserId} completed successfully.", id);
                return NoContent();
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to update name of user with ID {UserId} failed.", id);
                return Problem(result);
            });
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
        _logger.LogInformation("Starting request to update email of user with ID {UserId}.", id);

        var command = request.ToUpdateUserEmailCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: () =>
            {
                _logger.LogInformation("Request to update email of user with ID {UserId} completed successfully.", id);
                return NoContent();
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to update email of user with ID {UserId} failed.", id);
                return Problem(result);
            });
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
        _logger.LogInformation("Starting request to update password of user with ID {UserId}.", id);

        var command = request.ToUpdateUserPasswordCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: () =>
            {
                _logger.LogInformation("Request to update password of user with ID {UserId} completed successfully.", id);
                return NoContent();
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to update password of user with ID {UserId} failed.", id);
                return Problem(result);
            });
    }

    /// <summary>
    /// Elimina un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <returns>El resultado de la operación de eliminación.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Starting request to delete user with ID {UserId}.", id);

        var command = new DeleteUserCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            onSuccess: _ =>
            {
                _logger.LogInformation("Request to delete user with ID {UserId} completed successfully.", id);
                return NoContent();
            },
            onFailure: _ =>
            {
                _logger.LogWarning("Request to delete user with ID {UserId} failed.", id);
                return Problem(result);
            });
    }
}
