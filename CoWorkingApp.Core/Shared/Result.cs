using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Core.Primitives;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Core.Shared;

/// <summary>
/// Representa el resultado de una operación que puede ser exitosa o contener un error.
/// Implementa <see cref="IResult"/>.
/// </summary>
public readonly struct Result : IResult
{
    /// <summary>
    /// Colección de errores asociados con el resultado.
    /// </summary>
    public ICollection<Error> Errors { get; init; }

    /// <summary>
    /// El primer error en la colección de errores, o un error vacío si la colección está vacía.
    /// </summary>
    public Error FirstError => Errors.IsEmpty() ? ERRORS.None : Errors.First();

    /// <summary>
    /// Indica si el resultado es exitoso.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Indica si el resultado es fallido.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Constructor por defecto que lanza una excepción.
    /// Use los métodos estáticos para instanciar <see cref="Result"/>:
    /// <list type="bullet">
    /// <item><description><see cref="Success()"/> para crear un resultado exitoso.</description></item>
    /// <item><description><see cref="Failure(Error)"/> para crear un resultado fallido con un error.</description></item>
    /// <item><description><see cref="Failure(Exception)"/> para crear un resultado fallido con una excepción.</description></item>
    /// </list>
    /// </summary>
    /// <exception cref="InvalidOperationException">Se lanza cuando se intenta usar el constructor sin parámetros.</exception>
    public Result()
    {
        throw new InvalidOperationException("Use the static methods Success or Failure to instantiate Result.");
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura <see cref="Result"/> con el estado de éxito y la colección de errores especificados.
    /// </summary>
    /// <param name="isSuccess">Indica si la operación fue exitosa.</param>
    /// <param name="errors">La colección de errores asociados con el resultado.</param>
    /// <exception cref="InvalidOperationException">Se lanza si el estado de éxito no concuerda con la colección de errores.</exception>
    private Result(bool isSuccess, ICollection<Error> errors)
    {
        if (isSuccess && !errors.IsEmpty())
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && errors.IsEmpty())
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura <see cref="Result"/> con el estado de éxito y el error especificado.
    /// </summary>
    /// <param name="isSuccess">Indica si la operación fue exitosa.</param>
    /// <param name="error">El error asociado con el resultado, si lo hay.</param>
    /// <exception cref="InvalidOperationException">Se lanza si el estado de éxito no concuerda con el error.</exception>
    private Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != ERRORS.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == ERRORS.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = [error];
    }

    /// <summary>
    /// Ejecuta una acción si el resultado es exitoso y devuelve el resultado.
    /// </summary>
    /// <param name="action">La acción a ejecutar si el resultado es exitoso.</param>
    /// <returns>La instancia actual de <see cref="Result"/>.</returns>
    public Result OnSuccess(Action action)
    {
        if (IsSuccess)
        {
            action();
        }
        return this;
    }

    /// <summary>
    /// Ejecuta una acción si el resultado es fallido y devuelve el resultado.
    /// </summary>
    /// <param name="action">La acción a ejecutar si el resultado es fallido.</param>
    /// <returns>La instancia actual de <see cref="Result"/>.</returns>
    public Result OnFailure(Action action)
    {
        if (IsFailure)
        {
            action();
        }
        return this;
    }

    /// <summary>
    /// Crea un resultado exitoso sin valor asociado.
    /// </summary>
    /// <returns>Una nueva instancia de la estructura <see cref="Result"/> que representa un resultado exitoso.</returns>
    public static Result Success() => new(true, ERRORS.None);

    /// <summary>
    /// Crea un resultado fallido con el error especificado.
    /// </summary>
    /// <param name="error">El error asociado con el resultado.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result"/> que representa un resultado fallido.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Crea un resultado fallido con la colección de errores especificada.
    /// </summary>
    /// <param name="errors">La colección de errores resultantes de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result"/> que representa un resultado fallido.</returns>
    public static Result Failure(ICollection<Error> errors) => new(false, errors);

    /// <summary>
    /// Crea un resultado fallido con la excepción especificada,
    /// utilizando el tipo de la excepción como código de error y el mensaje de la excepción como descripción.
    /// </summary>
    /// <param name="exception">La excepción resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result"/> que representa un resultado fallido.</returns>
    public static Result Failure(Exception exception) => Failure(Error.Exception(exception.GetType().Name, exception.Message));

    /// <summary>
    /// Crea un resultado exitoso con el valor especificado.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante de la operación.</typeparam>
    /// <param name="value">El valor resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Success(value);

    /// <summary>
    /// Crea un resultado fallido con el error especificado.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante de la operación.</typeparam>
    /// <param name="error">El error resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure<TValue>(Error error) => Result<TValue>.Failure(error);

    /// <summary>
    /// Crea un resultado fallido con la excepción especificada,
    /// utilizando el tipo de la excepción como código de error y el mensaje de la excepción como descripción.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante de la operación.</typeparam>
    /// <param name="exception">La excepción resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure<TValue>(Exception exception) =>
        Result<TValue>.Failure(Error.Exception(exception.GetType().Name, exception.Message));

    /// <summary>
    /// Crea un resultado fallido con la colección de errores especificada.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante de la operación.</typeparam>
    /// <param name="errors">La colección de errores resultantes de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure<TValue>(ICollection<Error> errors) => Result<TValue>.Failure(errors);

    /// <summary>
    /// Crea un resultado basado en el valor especificado.
    /// </summary>
    /// <typeparam name="TValue">El tipo del valor resultante de la operación.</typeparam>
    /// <param name="value">El valor resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Create<TValue>(TValue? value) => Result<TValue>.Create(value);
}
