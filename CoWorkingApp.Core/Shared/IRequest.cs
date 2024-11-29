namespace CoWorkingApp.Core.Shared;

/// <summary>
/// Interfaz marcadora para representar una solicitud sin respuesta.
/// </summary>
public interface IRequest { }

/// <summary>
/// Interfaz marcadora para representar una solicitud con respuesta.
/// </summary>
/// <typeparam name="TResponse">Tipo de respuesta</typeparam>
public interface IRequest<out TResponse> { }
