using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Presentation.Problems;
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
    /// Maneja un resultado fallido y devuelve una respuesta HTTP adecuada basada en el tipo de error.
    /// </summary>
    /// <typeparam name="T">El tipo de datos contenidos en el resultado.</typeparam>
    /// <param name="result">El <see cref="Result"/> que contiene el estado de éxito o fallo y los errores correspondientes.</param>
    /// <returns>Un <see cref="IActionResult"/> que representa la respuesta HTTP adecuada para el error.</returns>
    protected IActionResult HandleFailure<T>(Result<T> result)
    {
        return result.FirstError.Type switch
        {
            ErrorType.Validation => BadRequest(
                ProblemDetailsFactory.FromResult(result, "Validation Error", StatusCodes.Status400BadRequest)),
            ErrorType.NotFound => NotFound(
                ProblemDetailsFactory.FromResult(result, "Not Found", StatusCodes.Status404NotFound)),
            ErrorType.Conflict => Conflict(
                ProblemDetailsFactory.FromResult(result, "Conflict", StatusCodes.Status409Conflict)),
            ErrorType.Unauthorized => Unauthorized(
                ProblemDetailsFactory.FromResult(result, "Unauthorized", StatusCodes.Status401Unauthorized)),
            ErrorType.Forbidden => StatusCode(
                StatusCodes.Status403Forbidden,
                ProblemDetailsFactory.FromResult(result, "Forbidden", StatusCodes.Status403Forbidden)),
            ErrorType.Exception => StatusCode(
                StatusCodes.Status500InternalServerError,
                ProblemDetailsFactory.FromResult(result, "Internal Server Error", StatusCodes.Status500InternalServerError)),
            _ => BadRequest(
                ProblemDetailsFactory.FromResult(result, "Bad Request", StatusCodes.Status400BadRequest))
        };
    }
}
