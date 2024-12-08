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
    /// Inicializa una nueva instancia de la estructura <see cref="Error"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <exception cref="ArgumentNullException">Se lanza cuando el código o el mensaje son nulos.</exception>
    private Error(string? code, string? message)
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
    }

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Error"/> con el código y mensaje especificados.
    /// </summary>
    /// <param name="code">El código del error.</param>
    /// <param name="message">El mensaje descriptivo del error.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Error"/>.</returns>
    public static Error Create(string? code, string? message) => new(code, message);
}
