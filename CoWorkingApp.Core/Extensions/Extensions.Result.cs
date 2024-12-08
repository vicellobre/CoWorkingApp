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
    /// Ejecuta una de las funciones proporcionadas dependiendo del estado del <see cref="Result"/>.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante de la operación.</typeparam>
    /// <param name="result">El resultado de la operación que se va a evaluar.</param>
    /// <param name="onSuccess">Función a ejecutar si el resultado es exitoso.</param>
    /// <param name="onFailure">Función a ejecutar si el resultado es fallido, que recibe el primer error como parámetro.</param>
    /// <returns>El valor retornado por la función <paramref name="onSuccess"/> si la operación fue exitosa, o por la función <paramref name="onFailure"/> si la operación falló.</returns>
    public static TValue Match<TValue>(this Result result, Func<TValue> onSuccess, Func<Error, TValue> onFailure) =>
        result.IsSuccess ? onSuccess() : onFailure(result.FirstError);

    /// <summary>
    /// Ejecuta una de las funciones proporcionadas dependiendo del estado del <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="result">El resultado de la operación que se va a evaluar.</param>
    /// <param name="onSuccess">Función a ejecutar si el resultado es exitoso, que recibe el valor como parámetro.</param>
    /// <param name="onFailure">Función a ejecutar si el resultado es fallido, que recibe el primer error como parámetro.</param>
    /// <returns>El valor retornado por la función <paramref name="onSuccess"/> si la operación fue exitosa, o por la función <paramref name="onFailure"/> si la operación falló.</returns>
    public static TValue Match<TValue>(this Result<TValue> result, Func<TValue, TValue> onSuccess, Func<Error, TValue> onFailure) =>
        result.IsSuccess ? onSuccess(result.Value) : onFailure(result.FirstError);

    /// <summary>
    /// Ejecuta una de las funciones proporcionadas dependiendo del estado del <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="result">El resultado de la operación que se va a evaluar.</param>
    /// <param name="onSuccess">Función a ejecutar si el resultado es exitoso, que recibe el valor como parámetro.</param>
    /// <param name="onFailure">Función a ejecutar si el resultado es fallido, que recibe el primer error como parámetro.</param>
    /// <returns>El valor retornado por la función <paramref name="onSuccess"/> si la operación fue exitosa, o por la función <paramref name="onFailure"/> si la operación falló.</returns>
    public static TResult Match<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> onSuccess, Func<Error, TResult> onFailure) =>
        result.IsSuccess ? onSuccess(result.Value) : onFailure(result.FirstError);
}
