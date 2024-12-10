using CoWorkingApp.Application.Contracts;
using MediatR;

namespace CoWorkingApp.Application.Behaviors;

/// <summary>
/// Comportamiento para filtrar y normalizar el input antes de que llegue al manejador.
/// </summary>
/// <typeparam name="TRequest">El tipo de solicitud.</typeparam>
/// <typeparam name="TResponse">El tipo de respuesta.</typeparam>
public class InputFilterBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="InputFilterBehavior{TRequest, TResponse}"/>.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo utilizada para la gestión de datos.</param>
    public InputFilterBehavior() { }

    /// <summary>
    /// Maneja el comportamiento de filtrado y normalización para la solicitud.
    /// </summary>
    /// <param name="request">La solicitud.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <param name="next">El siguiente delegado en el pipeline.</param>
    /// <returns>La respuesta de la solicitud.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IInputFilter filterableRequest)
        {
            filterableRequest.Filter();
        }

        return await next();
    }
}
