using Asp.Versioning;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Application.Users.Commands.AuthenticateUser;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.Users.V1.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoWorkingApp.Presentation.Users.V1.Controllers;

/// <summary>
/// Controlador para operaciones relacionadas con la autenticación de usuarios.
/// </summary>
[EnableCors("MyPolicy")] // Habilita CORS para este controlador específico
[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/login")] // Ruta del controlador, en plural por convención RESTful
public class LoginUserController : ApiController
{
    /// <summary>
    /// Servicio de autenticación utilizado para manejar la lógica de autenticación de usuarios.
    /// </summary>
    private readonly IAuthService _authService;

    /// <summary>
    /// Constructor de la clase LoginUserController.
    /// </summary>
    /// <param name="sender">El <see cref="ISender"/> utilizado para enviar solicitudes.</param>
    /// <param name="logger">El <see cref="ILogger{LoginUserController}"/> utilizado para registrar eventos y mensajes de diagnóstico.</param>
    /// <param name="authService">Instancia del servicio de autenticación.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el <paramref name="sender"/>, el <paramref name="logger"/> o el <paramref name="authService"/> es <see langword="null"/>.</exception>
    public LoginUserController(ISender sender, ILogger<LoginUserController> logger, IAuthService? authService) : base(sender, logger)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    /// <summary>
    /// Método para realizar la autenticación de un usuario.
    /// </summary>
    /// <param name="request">Datos de usuario (correo y contraseña) para autenticación.</param>
    /// <returns>ActionResult con el token generado o un mensaje de error.</returns>
    [HttpPost]
    [AllowAnonymous] // Permite el acceso a este método sin autenticación
    public async Task<IActionResult> Login([FromBody] UserRequest request)
    {
        try
        {
            AuthenticateUserCommand command = new(
                request.Email!,
                request.Password!);

            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                return Unauthorized(result.Errors);
            }

            var response = result.Value;
            var userResponse = (UserResponse)response;

            return Ok(new { Response = userResponse, response.Token });
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                Result.Failure(ex));
        }
    }
}
