using CoWorkingApp.Core.DomainErrors;

namespace CoWorkingApp.Core.Shared;



/// <summary>
/// Representa el resultado de una operación que puede ser exitosa o contener un error.
/// </summary>
public readonly struct Result
{
    /// <summary>
    /// Obtiene el error asociado con el resultado, si lo hay.
    /// </summary>
    public Error Error { get; init; }

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
    /// </list></summary>
    /// <exception cref="InvalidOperationException">Se lanza cuando se intenta usar el constructor sin parámetros.</exception>
    public Result()
    {
        throw new InvalidOperationException("Use the static methods Success or Failure to instantiate Result.");
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura <see cref="Result"/> con el estado de éxito y el error especificados.
    /// </summary>
    /// <param name="isSuccess">Indica si la operación fue exitosa.</param>
    /// <param name="error">El error asociado con el resultado, si lo hay.</param>
    private Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Errors.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Errors.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Ejecuta una acción si el resultado es exitoso.
    /// </summary>
    /// <param name="action">La acción a ejecutar.</param>
    /// <returns>El propio resultado.</returns>
    public Result OnSuccess(Action action)
    {
        if (IsSuccess)
        {
            action();
        }
        return this;
    }

    /// <summary>
    /// Ejecuta una acción si el resultado es fallido.
    /// </summary>
    /// <param name="action">La acción a ejecutar.</param>
    /// <returns>El propio resultado.</returns>
    public Result OnFailure(Action<Error> action)
    {
        if (IsFailure)
        {
            action(Error);
        }
        return this;
    }

    /// <summary>
    /// Crea un resultado exitoso sin valor asociado.
    /// </summary>
    /// <returns>Una nueva instancia de la estructura <see cref="Result"/> que representa un resultado exitoso.</returns>
    public static Result Success() => new(true, Errors.None);

    /// <summary>
    /// Crea un resultado fallido con el error especificado.
    /// </summary>
    /// <param name="error">El error asociado con el resultado.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result"/> que representa un resultado fallido.</returns>
    public static Result Failure(Error error) => new(false, error);

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
