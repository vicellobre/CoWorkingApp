using CoWorkingApp.Core.Extensions;
using ERRORS = CoWorkingApp.Core.DomainErrors.Errors;

namespace CoWorkingApp.Core.Shared;

/// <summary>
/// Representa el resultado de una operación que puede ser exitosa o contener uno o más errores,
/// con un valor resultante.
/// </summary>
/// <typeparam name="TValue">El tipo del valor resultante.</typeparam>
public readonly record struct Result<TValue>
{
    /// <summary>
    /// El valor resultante de la operación.
    /// </summary>
    private readonly TValue? _value;

    /// <summary>
    /// Obtiene la colección de errores asociados con el resultado, si los hay.
    /// </summary>
    public ICollection<Error> Errors { get; init; }

    /// <summary>
    /// Obtiene el valor resultante de la operación si es exitosa; de lo contrario, lanza una excepción.
    /// </summary>
    /// <exception cref="InvalidOperationException">Se lanza cuando se intenta acceder al valor de un resultado fallido.</exception>
    public TValue? Value => IsSuccess ? _value : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    /// <summary>
    /// Indica si el resultado es exitoso.
    /// </summary>
    public bool IsSuccess => Errors.IsEmpty();

    /// <summary>
    /// Indica si el resultado es fallido.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Obtiene el primer error en la colección de errores.
    /// </summary>
    public Error FirstError => Errors.IsEmpty() ? ERRORS.None : Errors.First();

    /// <summary>
    /// Constructor por defecto que lanza una excepción.
    /// Use los métodos estáticos para instanciar <see cref="Result{TValue}"/>:
    /// <list type="bullet">
    /// <item><description><see cref="Success(TValue)"/> para crear un resultado exitoso con un valor.</description></item>
    /// <item><description><see cref="Failure(Error)"/> para crear un resultado fallido con un error.</description></item>
    /// <item><description><see cref="Failure(Exception)"/> para crear un resultado fallido con una excepción.</description></item>
    /// <item><description><see cref="Failure(ICollection{Error})"/> para crear un resultado fallido con una colección de errores.</description></item>
    /// <item><description><see cref="Create(TValue?)"/> para crear un resultado dependiendo si el valor es nulo.</description></item>
    /// </list></summary>
    /// <exception cref="InvalidOperationException">Se lanza cuando se intenta usar el constructor sin parámetros.</exception>
    public Result()
    {
        throw new InvalidOperationException("Use the static methods Success, Failure or Create to instantiate Result.");
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura <see cref="Result{TValue}"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor resultante de la operación.</param>
    private Result(TValue? value)
    {
        Errors = value is not null ? ERRORS.EmptyErrors : [ERRORS.NullValue];
        _value = value;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura <see cref="Result{TValue}"/> con el error especificado.
    /// </summary>
    /// <param name="error">El error resultante de la operación.</param>
    private Result(Error error)
    {
        Errors = !error.Equals(ERRORS.None) ? [error] : [ERRORS.NullValue];
    }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura <see cref="Result{TValue}"/> con la colección de errores especificada.
    /// </summary>
    /// <param name="errors">La colección de errores resultantes de la operación.</param>
    private Result(ICollection<Error> errors)
    {
        //Evaluar cuando la colección tiene un error y es None o todos son None
        Errors = !errors.IsNullOrEmpty() ? errors : [ERRORS.NullValue];
    }

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Result{TValue}"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Success(TValue value) => new(value);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Result{TValue}"/> con el error especificado.
    /// </summary>
    /// <param name="error">El error resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure(Error error) => new(error);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Result{TValue}"/> con la excepción especificada,
    /// utilizando el tipo de la excepción como código de error y el mensaje de la excepción como descripción.
    /// </summary>
    /// <param name="exception">La excepción resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure(Exception exception) =>
        Failure(Error.Create(exception.GetType().Name, exception.Message));

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Result{TValue}"/> con la colección de errores especificada.
    /// </summary>
    /// <param name="errors">La colección de errores resultantes de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Failure(ICollection<Error> errors) => new(errors);

    /// <summary>
    /// Crea una nueva instancia de la estructura <see cref="Result{TValue}"/> con el valor especificado,
    /// o un resultado fallido si el valor es nulo.
    /// </summary>
    /// <param name="value">El valor resultante de la operación.</param>
    /// <returns>Una nueva instancia de la estructura <see cref="Result{TValue}"/>.</returns>
    public static Result<TValue> Create(TValue? value) => value is not null ? Success(value) : Failure(ERRORS.NullValue);

    /// <summary>
    /// Define una conversión implícita de un valor del tipo <typeparamref name="TValue"/> a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">El valor resultante de la operación.</param>
    public static implicit operator Result<TValue>(TValue value) => new(value);

    /// <summary>
    /// Define una conversión implícita de un <see cref="Error"/> a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="error">El error resultante de la operación.</param>
    public static implicit operator Result<TValue>(Error error) => new(error);

    /// <summary>
    /// Define una conversión implícita de una lista de errores a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="errors">La lista de errores resultantes de la operación.</param>
    public static implicit operator Result<TValue>(List<Error> errors) => new(errors);

    /// <summary>
    /// Define una conversión implícita de un conjunto de errores a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="errors">El conjunto de errores resultantes de la operación.</param>
    public static implicit operator Result<TValue>(HashSet<Error> errors) => new(errors);

    /// <summary>
    /// Define una conversión implícita de un arreglo de errores a una instancia de <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="errors">El arreglo de errores resultantes de la operación.</param>
    public static implicit operator Result<TValue>(Error[] errors) => new(errors);
}
