using CoWorkingApp.Application.Users.Services.Contracts;
using CoWorkingApp.Application.Users.Services.DTOs;
using CoWorkingApp.Presentation.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoWorkingApp.Presentation.Controllers;

/// <summary>
/// Controlador para las operaciones relacionadas con la entidad User.
/// </summary>
[ApiController]
//[ApiExplorerSettings(GroupName = "v1")]
[Route("api/[controller]s")] // Se utiliza el plural "users" en la ruta para seguir convenciones RESTful
public class UserController : ControllerGeneric<IUserService, UserServiceRequest, UserServiceResponse>
{
    /// <summary>
    /// Constructor de la clase UserController.
    /// </summary>
    /// <param name="service">Instancia del servicio de usuarios.</param>
    /// <param name="logger">Instancia del logger.</param>
    public UserController(IUserService? service, ILogger<ControllerGeneric<IUserService, UserServiceRequest, UserServiceResponse>>? logger) : base(service, logger) { }

    /// <summary>
    /// Obtiene un usuario por su dirección de correo electrónico.
    /// </summary>
    /// <param name="email">Dirección de correo electrónico del usuario.</param>
    /// <returns>ActionResult con el usuario correspondiente al correo electrónico o NotFound si no existe.</returns>
    [HttpGet("email/{email}")]
    [ResponseCache(Duration = 60)] // Se agrega caché HTTP para mejorar el rendimiento
    public async Task<ActionResult<UserServiceResponse>> GetByEmail(string email)
    {
        try
        {
            // Llama al servicio para obtener un usuario por su dirección de correo electrónico
            var response = await _service.GetByEmailAsync(email);

            if (!response.Success)
            {
                response.Message = "Error occurred while retrieving the user by email.";
                response.Errors.Add(response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }
        catch (Exception)
        {
            // Maneja cualquier otra excepción inesperada
            var exception = new Exception("An unexpected error occurred while getting the user by email.");
            _logger.LogError(exception, exception.Message);
            return StatusCode(500, HandleException(exception));
        }
    }

    /// <summary>
    /// Registra un nuevo usuario.
    /// </summary>
    /// <param name="userRequest">Objeto que contiene la información del usuario a registrar.</param>
    /// <returns>
    /// Un objeto <see cref="ActionResult{UserResponse}"/> que representa el resultado de la operación.
    /// <para>Devuelve un estado 201 si el registro es exitoso, 
    /// o un estado 400 si hay errores de validación. En caso de error inesperado, 
    /// devuelve un estado 500.</para>
    /// </returns>
    [HttpPost("register")]
    [AllowAnonymous] // Permite el acceso a este método sin autenticación
    public override async Task<ActionResult<UserServiceResponse>> Create([FromBody] UserServiceRequest userRequest)
    {
        try
        {
            // Llama al servicio para crear una nueva entidad
            var entity = await _service.CreateAsync(userRequest);

            if (!entity.Success)
            {
                entity.Message = "Error occurred while creating the entity.";
                entity.Errors.Add(entity.Message);

                return BadRequest(entity);
            }

            return StatusCode(201, entity);
        }
        catch (Exception)
        {
            // Maneja cualquier otra excepción inesperada
            var exception = new Exception("An unexpected error occurred while creating the entity.");
            _logger.LogError(exception, exception.Message);
            return StatusCode(500, HandleException(exception));
        }
    }
}
