using CoWorkingApp.Application.Users.Commands.CreateUser;
using CoWorkingApp.Application.Users.Commands.DeleteUser;
using CoWorkingApp.Application.Users.Commands.UpdateUser;
using CoWorkingApp.Application.Users.Queries.GetAllUsers;
using CoWorkingApp.Application.Users.Queries.GetUserByEmail;
using CoWorkingApp.Application.Users.Queries.GetUserById;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Presentation.Abstracts;
using CoWorkingApp.Presentation.DTOs.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace CoWorkingApp.Presentation.Controllers.Users;

/// <summary>
/// Controlador para manejar las operaciones relacionadas con usuarios en la versión 1 de la API.
/// </summary>
[ApiController]
[Route("api/v1/users")]
public class UserV1Controller : ApiController
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UserV1Controller"/>.
    /// </summary>
    /// <param name="sender">El <see cref="ISender"/> utilizado para enviar solicitudes.</param>
    public UserV1Controller(ISender sender) : base(sender) { }

    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    /// <returns>Una lista de todos los usuarios.</returns>
    [HttpGet]
    [EnableQuery]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        GetAllUsersQuery query = new();

        var response = await _sender.Send(query);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">El ID del usuario.</param>
    /// <returns>El usuario con el ID especificado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetUserByIdQuery query = new(id);

        var response = await _sender.Send(query);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Obtiene un usuario por su correo electrónico.
    /// </summary>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <returns>El usuario con el correo electrónico especificado.</returns>
    [HttpGet("email/{email}")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        GetUserByEmailQuery query = new(email);

        var response = await _sender.Send(query);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="request">Los datos del usuario a crear.</param>
    /// <returns>El resultado de la operación de creación.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserRequest request)
    {
        CreateUserCommand command = new(
            request.FirstName!,
            request.LastName!,
            request.Email!,
            request.Password!);

        var response = await _sender.Send(command);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="id">El ID del usuario a actualizar.</param>
    /// <param name="request">Los nuevos datos del usuario.</param>
    /// <returns>El resultado de la operación de actualización.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserRequest request)
    {
        UpdateUserCommand command = new(
                id,
                request.FirstName!,
                request.LastName!,
                request.Email!,
                request.Password!);

        var response = await _sender.Send(command);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }

    /// <summary>
    /// Elimina un usuario por su ID.
    /// </summary>
    /// <param name="id">El ID del usuario a eliminar.</param>
    /// <returns>El resultado de la operación de eliminación.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        DeleteUserCommand command = new(id);

        var response = await _sender.Send(command);

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => HandleFailure(response.FirstError));
    }
}
