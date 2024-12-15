namespace CoWorkingApp.Core.Enumerations;

/// <summary>
/// Tipos de errores que pueden ocurrir en la aplicación.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Sin error.
    /// </summary>
    None,
    /// <summary>
    /// Error de fallo general.
    /// </summary>
    Failure,
    /// <summary>
    /// Error inesperado.
    /// </summary>
    Unexpected,
    /// <summary>
    /// Error de validación.
    /// </summary>
    Validation,
    /// <summary>
    /// Error de conflicto.
    /// </summary>
    Conflict,
    /// <summary>
    /// Error de recurso no encontrado.
    /// </summary>
    NotFound,
    /// <summary>
    /// Error de no autorizado.
    /// </summary>
    Unauthorized,
    /// <summary>
    /// Error de prohibición.
    /// </summary>
    Forbidden,
    /// <summary>
    /// Error de excepción.
    /// </summary>
    Exception
}
