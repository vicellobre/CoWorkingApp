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

    /// <summary>
    /// Ejecuta una función si el resultado es exitoso.
    /// </summary>
    /// <typeparam name="T">El tipo de retorno de la función.</typeparam>
    /// <param name="result">El resultado de la operación.</param>
    /// <param name="func">La función a ejecutar.</param>
    /// <returns>El resultado de la función, o el valor por defecto de <typeparamref name="T"/> si no se ejecuta.</returns>
    public static T? OnSuccess<T>(this Result result, Func<T> func)
    {
        return result.IsSuccess ? func() : default;
    }

    /// <summary>
    /// Ejecuta una función si el resultado es fallido.
    /// </summary>
    /// <typeparam name="T">El tipo de retorno de la función.</typeparam>
    /// <param name="result">El resultado de la operación.</param>
    /// <param name="func">La función a ejecutar.</param>
    /// <returns>El resultado de la función, o el valor por defecto de <typeparamref name="T"/> si no se ejecuta.</returns>
    public static T? OnFailure<T>(this Result result, Func<Error, T> func)
    {
        return result.IsFailure ? func(result.Error) : default;
    }

    /// <summary>
    /// Ejecuta una función si el resultado es exitoso.
    /// </summary>
    /// <typeparam name="T">El tipo de retorno de la función.</typeparam>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="result">El resultado de la operación.</param>
    /// <param name="func">La función a ejecutar.</param>
    /// <returns>El resultado de la función, o el valor por defecto de <typeparamref name="T"/> si no se ejecuta.</returns>
    public static T? OnSuccess<T, TValue>(this Result<TValue> result, Func<TValue?, T> func)
    {
        return result.IsSuccess ? func(result.Value) : default;
    }

    /// <summary>
    /// Ejecuta una función si el resultado es fallido.
    /// </summary>
    /// <typeparam name="T">El tipo de retorno de la función.</typeparam>
    /// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
    /// <param name="result">El resultado de la operación.</param>
    /// <param name="func">La función a ejecutar.</param>
    /// <returns>El resultado de la función, o el valor por defecto de <typeparamref name="T"/> si no se ejecuta.</returns>
    public static T? OnFailure<T, TValue>(this Result<TValue> result, Func<Error, T> func)
    {
        return result.IsFailure ? func(result.FirstError) : default;
    }
}
