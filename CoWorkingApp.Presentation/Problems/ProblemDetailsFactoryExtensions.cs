using CoWorkingApp.Core.Shared;
using CoWorkingApp.Presentation.Errors.Extensions;
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
        /// Verifica si el resultado es fallido.
        /// </summary>
        /// <param name="result">El resultado que contiene el estado de éxito o fallo.</param>
        /// <exception cref="InvalidOperationException">Se lanza si el resultado es exitoso.</exception>
        private static void EnsureFailure(Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot create ProblemDetails from a successful result.");
            }
        }

        /// <summary>
        /// Crea un objeto <see cref="ProblemDetails"/> que contiene información detallada sobre un problema específico, basado en un <see cref="Result"/>.
        /// </summary>
        /// <param name="factory">Instancia de <see cref="ProblemDetailsFactory"/> utilizada para la extensión del método.</param>
        /// <param name="result">El resultado que contiene el estado de éxito o fallo y los errores correspondientes.</param>
        /// <returns>Un <see cref="ProblemDetails"/> que contiene información detallada sobre el problema.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si el resultado es exitoso.</exception>
        public static ProblemDetails FromResult(this ProblemDetailsFactory factory, Result result)
        {
            EnsureFailure(result);

            return new ProblemDetails
            {
                Title = result.FirstError.Code,
                Type = result.FirstError.Type.ToString(),
                Detail = result.FirstError.Message,
                Status = result.FirstError.Type.ToStatusCode(),
                Extensions = { { nameof(result.Errors), result.Errors } }
            };
        }

        /// <summary>
        /// Crea un objeto <see cref="ProblemDetails"/> que contiene información detallada sobre un problema específico, basado en un <see cref="Error"/>.
        /// </summary>
        /// <param name="factory">Instancia de <see cref="ProblemDetailsFactory"/> utilizada para la extensión del método.</param>
        /// <param name="error">El error que contiene el código y mensaje del problema.</param>
        /// <returns>Un <see cref="ProblemDetails"/> que contiene información detallada sobre el problema.</returns>
        public static ProblemDetails FromError(this ProblemDetailsFactory factory, Error error) =>
           new()
           {
               Title = error.Code,
               Type = error.Type.ToString(),
               Detail = error.Message,
               Status = error.Type.ToStatusCode(),
               Extensions = { { nameof(error.StackTrace), error.StackTrace } }
           };
    }
}
