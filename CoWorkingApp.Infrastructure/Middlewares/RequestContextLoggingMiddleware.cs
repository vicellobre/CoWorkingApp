using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace CoWorkingApp.Infrastructure.Middlewares
{
    /// <summary>
    /// Middleware para agregar un CorrelationId a los logs de la solicitud.
    /// </summary>
    public class RequestContextLoggingMiddleware
    {
        /// <summary>
        /// Nombre del encabezado HTTP que contiene el CorrelationId.
        /// </summary>
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        /// <summary>
        /// Delegado de la siguiente función en la canalización de middleware.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RequestContextLoggingMiddleware"/>.
        /// </summary>
        /// <param name="next">El siguiente delegado en la canalización de middleware.</param>
        public RequestContextLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoca el middleware para agregar el CorrelationId al contexto de logs.
        /// </summary>
        /// <param name="context">El contexto HTTP de la solicitud actual.</param>
        /// <returns>Una tarea que representa la ejecución del middleware.</returns>
        public Task Invoke(HttpContext context)
        {
            string correlationId = GetCorrelationId(context);

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                return _next.Invoke(context);
            }
        }

        /// <summary>
        /// Obtiene el CorrelationId de los encabezados de la solicitud o genera uno nuevo si no está presente.
        /// </summary>
        /// <param name="context">El contexto HTTP de la solicitud actual.</param>
        /// <returns>El CorrelationId de la solicitud.</returns>
        private static string GetCorrelationId(HttpContext context)
        {
            context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out StringValues correlationId);
            return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
        }
    }
}
