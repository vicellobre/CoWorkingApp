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
    /// Crea un objeto <see cref="ProblemDetails"/> que contiene información detallada sobre un problema específico.
    /// </summary>
    /// <param name="title">El título del problema.</param>
    /// <param name="status">El código de estado HTTP asociado con el problema.</param>
    /// <param name="error">El error principal que describe el problema.</param>
    /// <param name="errors">Errores adicionales relacionados con el problema (opcional).</param>
    /// <returns>Un <see cref="ProblemDetails"/> que contiene información detallada sobre el problema.</returns>
    protected static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

    /// <summary>
    /// Maneja un resultado fallido y devuelve una respuesta HTTP adecuada basada en el tipo de error.
    /// </summary>
    /// <typeparam name="T">El tipo de datos contenidos en el resultado.</typeparam>
    /// <param name="result">El <see cref="Result{T}"/> que contiene el estado de éxito o fallo y los errores correspondientes.</param>
    /// <returns>Un <see cref="IActionResult"/> que representa la respuesta HTTP adecuada para el error.</returns>
    /// <exception cref="InvalidOperationException">Se lanza si el resultado indica éxito en lugar de fallo.</exception>
    protected IActionResult HandleFailure<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return result.FirstError.Type switch
        {
            ErrorType.Validation => BadRequest(
                CreateProblemDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.FirstError,
                    [.. result.Errors])),
            ErrorType.NotFound => NotFound(
                CreateProblemDetails(
                    "Not Found",
                    StatusCodes.Status404NotFound,
                    result.FirstError)),
            ErrorType.Conflict => Conflict(
                CreateProblemDetails(
                    "Conflict",
                    StatusCodes.Status409Conflict,
                    result.FirstError)),
            ErrorType.Unauthorized => Unauthorized(
                CreateProblemDetails(
                    "Unauthorized",
                    StatusCodes.Status401Unauthorized,
                    result.FirstError)),
            ErrorType.Forbidden => StatusCode(
                StatusCodes.Status403Forbidden,
                CreateProblemDetails(
                    "Forbidden",
                    StatusCodes.Status403Forbidden,
                    result.FirstError)),
            _ => BadRequest(
                CreateProblemDetails(
                    "Bad Request",
                    StatusCodes.Status400BadRequest,
                    result.FirstError))
        };
    }
}
