using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.Primitives;

/// <summary>
/// Interfaz que representa el resultado de una operación.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Indica si la operación fue exitosa.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Indica si la operación falló.
    /// </summary>
    bool IsFailure { get; }

    /// <summary>
    /// Colección de errores asociados con la operación.
    /// </summary>
    ICollection<Error> Errors { get; }

    /// <summary>
    /// Obtiene el primer error de la colección de errores.
    /// </summary>
    public Error FirstError { get; }
}

/// <summary>
/// Interfaz genérica que representa el resultado de una operación con un valor de retorno.
/// <para>Deriva de <see cref="IResult"/>.</para>
/// </summary>
/// <typeparam name="T">El tipo del valor de retorno.</typeparam>
public interface IResult<T> : IResult
{
    /// <summary>
    /// El valor retornado por la operación.
    /// </summary>
    public T Value { get; }
}

