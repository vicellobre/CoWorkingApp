namespace CoWorkingApp.Core.Shared;

/// <summary>
/// Interfaz marcadora para representar una solicitud sin respuesta.
/// </summary>
public interface IRequest { }

/// <summary>
/// Interfaz marcadora para representar una solicitud con respuesta.
/// </summary>
/// <typeparam name="TResponse">El tipo de la respuesta de la solicitud.</typeparam>
public interface IRequest<out TResponse> { }
