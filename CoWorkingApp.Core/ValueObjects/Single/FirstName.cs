using System.Text.RegularExpressions;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa el valor del nombre del usuario.
/// </summary>
public readonly record struct FirstName
{
    /// <summary>
    /// Obtiene el nombre del usuario.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Longitud mínima permitida para el nombre.
    /// </summary>
    public const int MinLength = 1;

    /// <summary>
    /// Longitud máxima permitida para el nombre.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Patrón de expresión regular para validar el formato del nombre.
    /// </summary>
    private const string Pattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑçÇüÜàÀèÈìÌòÒùÙâêÊîôûäëïöüß]+(?:\s[a-zA-ZáéíóúÁÉÍÓÚñÑçÇüÜàÀèÈìÌòÒùÙâêÊîôûäëïöüß]+)*$";

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="FirstName"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public FirstName()
    {
        //throw new InvalidOperationException("Use the static Create method to instantiate FirstName.");
    }

    /// <summary>
    /// Constructor privado para inicializar el valor del nombre.
    /// </summary>
    /// <param name="value">El valor del nombre.</param>
    private FirstName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="FirstName"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor del nombre.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="FirstName"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<FirstName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<FirstName>.Failure(Errors.FirstName.IsNullOrEmpty);
        }

        if (value.Length < MinLength)
        {
            return Result<FirstName>.Failure(Errors.FirstName.TooShort(MinLength));
        }

        if (value.Length > MaxLength)
        {
            return Result<FirstName>.Failure(Errors.FirstName.TooLong(MaxLength));
        }

        if (!Regex.IsMatch(value, Pattern))
        {
            return Result<FirstName>.Failure(Errors.FirstName.InvalidFormat);
        }

        return Result<FirstName>.Success(new FirstName(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena del nombre.
    /// </summary>
    /// <returns>El nombre como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="FirstName"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="firstName">El valor de <see cref="FirstName"/> a convertir.</param>
    public static implicit operator string(FirstName firstName) => firstName.Value;
}
