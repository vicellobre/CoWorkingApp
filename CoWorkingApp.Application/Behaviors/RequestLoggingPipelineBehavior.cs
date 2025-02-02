using CoWorkingApp.Core.Primitives;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace CoWorkingApp.Application.Behaviors
{
    /// <summary>
    /// Comportamiento de pipeline para registrar eventos importantes de la solicitud.
    /// </summary>
    /// <typeparam name="TRequest">El tipo de la solicitud.</typeparam>
    /// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
    public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : MediatR.IRequest<TResponse>
        where TResponse : IResult
    {
        private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RequestLoggingPipelineBehavior{TRequest, TResponse}"/>.
        /// </summary>
        /// <param name="logger">El logger para registrar eventos.</param>
        public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Maneja el comportamiento de logging para la solicitud.
        /// </summary>
        /// <param name="request">La solicitud.</param>
        /// <param name="next">El siguiente delegado en el pipeline.</param>
        /// <param name="cancellationToken">El token de cancelación.</param>
        /// <returns>La respuesta de la solicitud.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Obtiene el nombre del tipo de la solicitud para usarlo en los logs
            string requestName = typeof(TRequest).Name;

            // Registra un mensaje informativo indicando que la solicitud está siendo procesada
            _logger.LogInformation("Processing request {RequestName}", requestName);

            // Llama al siguiente delegado en el pipeline para obtener la respuesta
            TResponse result = await next();

            // Verifica si la solicitud fue exitosa
            if (result.IsSuccess)
            {
                // Registra un mensaje informativo indicando que la solicitud se completó con éxito
                _logger.LogInformation("Request {RequestName} completed successfully", requestName);
            }
            else
            {
                // Si la solicitud no fue exitosa, agrega la propiedad de error al contexto de logs
                using (LogContext.PushProperty("Error", result.Errors, true))
                {
                    // Registra un mensaje de error indicando que la solicitud se completó con errores
                    _logger.LogError("Request {RequestName} completed with error", requestName);
                }
            }

            // Devuelve la respuesta de la solicitud
            return result;
        }
    }
}
