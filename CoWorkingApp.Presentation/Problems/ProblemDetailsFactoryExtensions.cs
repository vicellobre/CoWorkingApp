using CoWorkingApp.Core.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CoWorkingApp.Presentation.Problems
{
    /// <summary>
    /// Proporciona métodos de extensión para la clase <see cref="ProblemDetailsFactory"/>.
    /// </summary>
    public static class ProblemDetailsFactoryExtensions
    {
        /// <summary>
        /// Crea un objeto <see cref="ProblemDetails"/> que contiene información detallada sobre un problema específico, basado en un <see cref="Result"/>.
        /// </summary>
        /// <param name="factory">Instancia de <see cref="ProblemDetailsFactory"/> utilizada para la extensión del método.</param>
        /// <param name="result">El resultado que contiene el estado de éxito o fallo y los errores correspondientes.</param>
        /// <param name="title">El título del problema.</param>
        /// <param name="status">El código de estado HTTP asociado con el problema.</param>
        /// <returns>Un <see cref="ProblemDetails"/> que contiene información detallada sobre el problema.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si el resultado es exitoso.</exception>
        public static ProblemDetails FromResult<T>(this ProblemDetailsFactory factory, Result<T> result, string title, int status)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot create ProblemDetails from a successful result.");
            }

            return new ProblemDetails
            {
                Title = title,
                Type = result.FirstError.Code,
                Detail = result.FirstError.Message,
                Status = status,
                Extensions = { { nameof(result.Errors), result.Errors } }
            };
        }
    }
}
