using CoWorkingApp.Core.Enumerations;

namespace CoWorkingApp.Core.Shared;

/// <summary>
/// Representa un error con un código y un mensaje descriptivo.
/// </summary>
public readonly record struct Error
{
    /// <summary>
    /// Código del error.
    /// </summary>
    public string Code { get; init; }

    /// <summary>
    /// Mensaje descriptivo del error.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Tipo del error.
    /// </summary>
    public ErrorType Type { get; init; }

    /// <summary>
    /// Constructor por defecto que lanza una excepción. 
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="Error"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Se lanza siempre para indicar que este constructor no debe ser usado directamente.</exception>
    public Error()
    {
        throw new InvalidOperationException("Use the static Create method to instantiate Error");
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura <see cref="Error"/> con el código, mensaje y tipo especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <param name="type">El tipo del error.</param>
    /// <exception cref="ArgumentNullException">Se lanza cuando el código o el mensaje son nulos.</exception>
    private Error(string? code, string? message, ErrorType type)
    {
        if (code is null)
        {
            throw new ArgumentNullException(nameof(code), "Error code cannot be null.");
        }

        if (message is null)
        {
            throw new ArgumentNullException(nameof(message), "Error message cannot be null.");
        }

        Code = !string.IsNullOrWhiteSpace(code) ? code : string.Empty;
        Message = !string.IsNullOrWhiteSpace(message) ? message : string.Empty;
        Type = type;
    }

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> con el código, mensaje y tipo especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <param name="type">El tipo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Create(string? code, string? message, ErrorType type) => new(code, message, type);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.Failure"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Failure(string? code, string? message) => new(code, message, ErrorType.Failure);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.Unexpected"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Unexpected(string? code, string? message) => new(code, message, ErrorType.Unexpected);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.Validation"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Validation(string? code, string? message) => new(code, message, ErrorType.Validation);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.Conflict"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Conflict(string? code, string? message) => new(code, message, ErrorType.Conflict);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.NotFound"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error NotFound(string? code, string? message) => new(code, message, ErrorType.NotFound);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.Unauthorized"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Unauthorized(string? code, string? message) => new(code, message, ErrorType.Unauthorized);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.Forbidden"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Forbidden(string? code, string? message) => new(code, message, ErrorType.Forbidden);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> de tipo <see cref="ErrorType.Exception"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Exception(string? code, string? message) => new(code, message, ErrorType.Exception);
}
