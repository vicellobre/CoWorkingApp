namespace CoWorkingApp.Core.Shared;

/// <summary>
/// Clase base abstracta para los mensajes de respuesta del sistema.
/// </summary>
public abstract record ResponseMessage
{
    /// <summary>
    /// Obtiene o establece un valor que indica si la operación fue exitosa.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Obtiene o establece el mensaje asociado a la operación.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Obtiene o establece la lista de errores asociados a la operación, si los hay.
    /// </summary>
    public List<string> Errors { get; set; } = new List<string>();

    /// <summary>
    /// Maneja las excepciones y construye una respuesta de error coherente.
    /// </summary>
    /// <typeparam name="TResponse">El tipo de respuesta que se va a devolver.</typeparam>
    /// <param name="ex">La excepción que se ha producido.</param>
    /// <returns>Una instancia de la respuesta de error.</returns>
    public static TResponse HandleException<TResponse>(Exception ex) where TResponse : ResponseMessage, new()
    {
        var response = new TResponse { Success = false, Message = ex.Message };
        response.Errors.Add(ex.Message);
        return response;
    }
}
