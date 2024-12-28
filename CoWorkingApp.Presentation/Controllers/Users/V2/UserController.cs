using Asp.Versioning;
using CoWorkingApp.Application.Users.Commands.CreateUser;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Presentation.Controllers.Users.V2;

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
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="request">Los datos del usuario a crear.</param>
    /// <returns>El resultado de la operación de creación.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
    {
        CreateUserCommand command = (CreateUserCommand)request;

        var result = await _sender.Send(command);

        CreateUserResponse response = (CreateUserResponse)result.Value;

        return result.Match(
            onSuccess: _ => Ok(response),
            onFailure: error => Problem<CreateUserResponse>(result));
    }
}
