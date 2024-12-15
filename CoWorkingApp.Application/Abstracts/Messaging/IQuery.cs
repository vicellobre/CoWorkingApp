using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Abstracts.Messaging;

/// <summary>
/// Interfaz para una consulta con un tipo de retorno especificado.
/// </summary>
/// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
public interface IQuery<TResponse> : MediatR.IRequest<Result<TResponse>>;

