using System.Text.RegularExpressions;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa el valor del apellido del usuario.
/// </summary>
public readonly record struct LastName
{
    /// <summary>
    /// Obtiene el apellido del usuario.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Longitud mínima permitida para el apellido.
    /// </summary>
    public const int MinLength = 1;

    /// <summary>
    /// Longitud máxima permitida para el apellido.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Patrón de expresión regular para validar el formato del apellido.
    /// </summary>
    private const string Pattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑçÇüÜàÀèÈìÌòÒùÙâêÊîôûäëïöüß]+(?:\s[a-zA-ZáéíóúÁÉÍÓÚñÑçÇüÜàÀèÈìÌòÒùÙâêÊîôûäëïöüß]+)*$";

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="LastName"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public LastName()
    {
        throw new InvalidOperationException("Use the static Create method to instantiate LastName.");
    }

    /// <summary>
    /// Constructor privado para inicializar el valor del apellido.
    /// </summary>
    /// <param name="value">El valor del apellido.</param>
    private LastName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="LastName"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor del apellido.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="LastName"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<LastName>.Failure(Errors.LastName.IsNullOrEmpty);
        }

        if (value.Length < MinLength)
        {
            return Result<LastName>.Failure(Errors.LastName.TooShort(MinLength));
        }

        if (value.Length > MaxLength)
        {
            return Result<LastName>.Failure(Errors.LastName.TooLong(MaxLength));
        }

        if (!Regex.IsMatch(value, Pattern))
        {
            return Result<LastName>.Failure(Errors.LastName.InvalidFormat);
        }

        return Result<LastName>.Success(new LastName(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena del apellido.
    /// </summary>
    /// <returns>El apellido como una cadena.</returns>
    public override string ToString() => Value;
}
