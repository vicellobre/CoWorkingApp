using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Representa la ausencia de errores.
    /// </summary>
    public readonly static Error None = Error.Create(string.Empty, string.Empty, ErrorType.None);

    /// <summary>
    /// Representa un error cuando el valor especificado es nulo.
    /// </summary>
    public readonly static Error NullValue = Error.Validation("Error.NullValue", "The specified result value is null.");

    /// <summary>
    /// Representa un error desconocido.
    /// </summary>
    public readonly static Error Unknown = Error.Unexpected("Error.Unknown", "An unknown error occurred.");

    /// <summary>
    /// Representa una colección de errores vacía.
    /// </summary>
    public static ICollection<Error> EmptyErrors { get; } = [];
}
