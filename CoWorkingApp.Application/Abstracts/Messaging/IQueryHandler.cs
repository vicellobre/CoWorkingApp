using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Abstracts.Messaging;

/// <summary>
/// Interfaz para manejar consultas con un tipo de retorno especificado.
/// </summary>
/// <typeparam name="TQuery">El tipo de la consulta.</typeparam>
/// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
/// <seealso cref="IQuery{TResponse}"/>
public interface IQueryHandler<TQuery, TResponse> : MediatR.IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;