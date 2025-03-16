using CoWorkingApp.Core.Shared;
using CoWorkingApp.Presentation.Errors.Extensions;
using CoWorkingApp.Presentation.Problems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoWorkingApp.Presentation.Abstracts;

/// <summary>
/// Clase base para controladores de la API que proporciona manejo de errores y funcionalidad común.
/// </summary>
[ApiController]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Interfaz para enviar solicitudes (comandos y consultas) a través de MediatR.
    /// </summary>
    protected readonly ISender _sender;

    /// <summary>
    /// Logger para registrar eventos y mensajes de diagnóstico.
    /// </summary>
    protected readonly ILogger _logger;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ApiController"/>.
    /// </summary>
    /// <param name="sender">El <see cref="ISender"/> utilizado para enviar solicitudes.</param>
    /// <param name="logger">El <see cref="ILogger"/> utilizado para registrar eventos y mensajes de diagnóstico.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el <paramref name="sender"/> o el <paramref name="logger"/> es <see langword="null"/>.</exception>
    protected ApiController(ISender sender, ILogger logger) : base()
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Crea una respuesta de creación con la URI del recurso creado.
    /// </summary>
    /// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
    /// <param name="actionName">El nombre de la acción.</param>
    /// <param name="routeValues">Los valores de la ruta para generar la URI.</param>
    /// <param name="response">La respuesta de la creación.</param>
    /// <returns>Un <see cref="CreatedResult"/> con la URI del recurso creado.</returns>
    protected ActionResult<TResponse> CreatedAtAction<TResponse>(string actionName, object routeValues, TResponse response)
    {
        var uri = Url.Action(actionName, routeValues);
        return Created(uri, response);
    }

    /// <summary>
    /// Maneja un error y devuelve una respuesta HTTP adecuada basada en el tipo de error.
    /// </summary>
    /// <param name="error">El <see cref="Error"/> que contiene información sobre el error ocurrido.</param>
    /// <returns>Un <see cref="IActionResult"/> que representa la respuesta HTTP adecuada para el error.</returns>
    protected IActionResult HandleFailure(Error error) =>
        Problem(
            title: error.Code,
            type: error.Type.ToString(),
            statusCode: error.Type.ToStatusCode(),
            detail: error.Message
        );

    /// <summary>
    /// Maneja un error y devuelve una respuesta HTTP adecuada basada en el tipo de error.
    /// </summary>
    /// <typeparam name="T">El tipo de datos contenidos en el resultado.</typeparam>
    /// <param name="error">El <see cref="Error"/> que contiene información sobre el error ocurrido.</param>
    /// <returns>Un <see cref="ActionResult{T}"/> que representa la respuesta HTTP adecuada para el error.</returns>
    protected ActionResult<T> HandleFailureFromT<T>(Error error) =>
        Problem(
            title: error.Code,
            type: error.Type.ToString(),
            statusCode: error.Type.ToStatusCode(),
            detail: error.Message
        );

    /// <summary>
    /// Crea una respuesta de problema para un resultado fallido.
    /// </summary>
    /// <typeparam name="T">El tipo de resultado esperado.</typeparam>
    /// <param name="result">El resultado que contiene el estado de éxito o fallo y los errores correspondientes.</param>
    /// <returns>Un <see cref="ActionResult{T}"/> que contiene la información detallada del problema.</returns>
    protected ActionResult<T> Problem<T>(Result result)
    {
        var problemDetails = ProblemDetailsFactory.FromResult(result);
        return new ObjectResult(problemDetails);
    }

    /// <summary>
    /// Crea una respuesta de problema para un resultado fallido sin tipo específico.
    /// </summary>
    /// <param name="result">El resultado que contiene el estado de éxito o fallo y los errores correspondientes.</param>
    /// <returns>Un <see cref="ActionResult"/> que contiene la información detallada del problema.</returns>
    protected ActionResult Problem(Result result)
    {
        var problemDetails = ProblemDetailsFactory.FromResult(result);
        return new ObjectResult(problemDetails);
    }

    /// <summary>
    /// Crea una respuesta de problema para un error específico.
    /// </summary>
    /// <typeparam name="T">El tipo de resultado esperado.</typeparam>
    /// <param name="error">El error que contiene el código y mensaje del problema.</param>
    /// <returns>Un <see cref="ActionResult{T}"/> que contiene la información detallada del problema.</returns>
    protected ActionResult<T> Problem<T>(Error error)
    {
        var problemDetails = ProblemDetailsFactory.FromError(error);
        return new ObjectResult(problemDetails);
    }
}
