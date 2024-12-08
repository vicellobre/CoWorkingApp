using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoWorkingApp.Presentation.Exceptions.Handlers;

/// <summary>
/// Manejador de excepciones personalizado que utiliza IProblemDetailsService para escribir detalles del problema en la respuesta HTTP.
/// </summary>
/// <param name="problemDetailsService">El servicio utilizado para escribir detalles del problema.</param>
public class CustomExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    /// <summary>
    /// Intenta manejar la excepción y escribir los detalles del problema en la respuesta HTTP.
    /// </summary>
    /// <param name="httpContext">El contexto HTTP actual.</param>
    /// <param name="exception">La excepción que ocurrió.</param>
    /// <param name="cancellationToken">El token de cancelación para esta operación asincrónica.</param>
    /// <returns>Un ValueTask que representa el resultado de la operación, devolviendo true si la excepción fue manejada exitosamente; de lo contrario, false.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Title = exception.GetType().Name,
            Type = "InternalServerError",
            Detail = exception.Message,
            Status = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            }
        };

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}
