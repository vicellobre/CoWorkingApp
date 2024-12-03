using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Abstracts.Messaging;

/// <summary>
/// Interfaz para manejar comandos sin tipo de retorno.
/// </summary>
/// <typeparam name="TCommand">El tipo del comando.</typeparam>
/// <seealso cref="ICommand"/>
public interface ICommandHandler<TCommand> : MediatR.IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

/// <summary>
/// Interfaz para manejar comandos con un tipo de retorno especificado.
/// </summary>
/// <typeparam name="TCommand">El tipo del comando.</typeparam>
/// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
/// <seealso cref="ICommand{TResponse}"/>
public interface ICommandHandler<TCommand, TResponse> : MediatR.IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
