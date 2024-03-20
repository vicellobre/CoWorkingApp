namespace CoWorkingApp.Core.Application.Contracts.Requests
{
    /// <summary>
    /// Define un manejador para una solicitud con una respuesta.
    /// </summary>
    /// <typeparam name="TRequest">El tipo de solicitud que se maneja</typeparam>
    /// <typeparam name="TResponse">El tipo de respuesta del manejador</typeparam>
    public interface IRequestHandler<in TRequest, out TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Maneja una solicitud.
        /// </summary>
        /// <param name="message">El mensaje de solicitud</param>
        /// <returns>La respuesta de la solicitud</returns>
        TResponse Handle(TRequest message);
    }

    /// <summary>
    /// Define un manejador para una solicitud sin un valor de retorno.
    /// </summary>
    /// <typeparam name="TRequest">El tipo de solicitud que se maneja</typeparam>
    public interface IRequestHandler<in TRequest> where TRequest : IRequest
    {
        /// <summary>
        /// Maneja una solicitud.
        /// </summary>
        /// <param name="message">El mensaje de solicitud</param>
        void Handle(TRequest message);
    }
}
