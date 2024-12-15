using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Presentation.Abstracts;

/// <summary>
/// Clase base para controladores de la API que proporciona manejo de errores y funcionalidad común.
/// </summary>
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Interfaz para enviar solicitudes (comandos y consultas) a través de MediatR.
    /// </summary>
    protected readonly ISender _sender;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ApiController"/>.
    /// </summary>
    /// <param name="sender">El <see cref="ISender"/> utilizado para enviar solicitudes.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el <paramref name="sender"/> es <see langword="null"/>.</exception>
    protected ApiController(ISender sender) : base() => _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    /// <summary>
    /// Maneja un error y devuelve una respuesta HTTP adecuada basada en el tipo de error.
    /// </summary>
    /// <typeparam name="T">El tipo de datos contenidos en el resultado.</typeparam>
    /// <param name="error">El <see cref="Error"/> que contiene información sobre el error ocurrido.</param>
    /// <returns>Un <see cref="IActionResult"/> que representa la respuesta HTTP adecuada para el error.</returns>
    protected IActionResult HandleFailure(Error error)
    {
        int statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Exception => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest,
        };

        return Problem(
            title: error.Code,
            type: error.Type.ToString(),
            statusCode: statusCode,
            detail: error.Message
        );
    }

    /// <summary>
    /// Maneja un error y devuelve una respuesta HTTP adecuada basada en el tipo de error.
    /// </summary>
    /// <typeparam name="T">El tipo de datos contenidos en el resultado.</typeparam>
    /// <param name="error">El <see cref="Error"/> que contiene información sobre el error ocurrido.</param>
    /// <returns>Un <see cref="ActionResult{T}"/> que representa la respuesta HTTP adecuada para el error.</returns>
    protected ActionResult<T> HandleFailure<T>(Error error)
    {
        int statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Exception => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest,
        };

        return Problem(
            title: error.Code,
            type: error.Type.ToString(),
            statusCode: statusCode,
            detail: error.Message
        );
    }

}
