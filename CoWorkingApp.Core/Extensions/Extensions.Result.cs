using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.Extensions;

/// <summary>
/// Proporciona métodos de extensión para convertir valores y colecciones de errores a instancias de <see cref="Result{TValue}"/>.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Crea una instancia de <see cref="Result{TValue}"/> con el valor especificado.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="value">El valor resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> ToResult<TValue>(this TValue value) => value;

    /// <summary>
    /// Crea una instancia de <see cref="Result{TValue}"/> con el error especificado.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="error">El error resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> ToResult<TValue>(this Error error) => error;

    /// <summary>
    /// Convierte una colección de errores a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="errors">La colección de errores a convertir.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> ToResult<TValue>(this ICollection<Error> errors) => errors.ToList();

    /// <summary>
    /// Convierte una lista de errores a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="errors">La lista de errores a convertir.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> ToResult<TValue>(this List<Error> errors) => errors;

    /// <summary>
    /// Convierte un conjunto de errores a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="errors">El conjunto de errores a convertir.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> ToResult<TValue>(this HashSet<Error> errors) => errors;

    /// <summary>
    /// Convierte un arreglo de errores a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="errors">El arreglo de errores a convertir.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> ToResult<TValue>(this Error[] errors) => errors;
}
