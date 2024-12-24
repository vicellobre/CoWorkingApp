using CoWorkingApp.Core.Enumerations;
using Microsoft.AspNetCore.Http;

namespace CoWorkingApp.Presentation.Errors.Extensions;

public static class ErrorTypeExtensions
{
    /// <summary>
    /// Convierte ErrorType a HttpStatusCode.
    /// </summary>
    /// <param name="errorType">El tipo de error.</param>
    /// <returns>El código de estado HTTP correspondiente.</returns>
    public static int ToStatusCode(this ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Exception => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }
}
