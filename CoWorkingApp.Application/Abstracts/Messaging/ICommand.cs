using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Abstracts.Messaging;

/// <summary>
/// Interfaz para un comando sin tipo de retorno.
/// </summary>
public interface ICommand : MediatR.IRequest<Result> { }

/// <summary>
/// Interfaz para un comando con un tipo de retorno especificado.
/// </summary>
/// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
public interface ICommand<TResponse> : MediatR.IRequest<Result<TResponse>> { }
