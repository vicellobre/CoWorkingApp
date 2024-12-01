using System.Text.RegularExpressions;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa el valor de la contraseña del usuario.
/// </summary>
public readonly record struct Password
{
    /// <summary>
    /// Obtiene la contraseña del usuario.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Longitud mínima permitida para la contraseña.
    /// </summary>
    public const int MinLength = 8;

    /// <summary>
    /// Longitud máxima permitida para la contraseña.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Patrón de expresión regular para validar el formato de la contraseña.
    /// Debe contener al menos una letra minúscula, una letra mayúscula, un dígito y un carácter especial.
    /// </summary>
    private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="Password"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public Password()
    {
        throw new InvalidOperationException("Use the static Create method to instantiate Password.");
    }

    /// <summary>
    /// Constructor privado para inicializar el valor de la contraseña.
    /// </summary>
    /// <param name="value">El valor de la contraseña.</param>
    private Password(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Password"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor de la contraseña.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="Password"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<Password> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Password>.Failure(Errors.Password.IsNullOrEmpty);
        }

        if (value.Length < MinLength)
        {
            return Result<Password>.Failure(Errors.Password.TooShort(MinLength));
        }

        if (value.Length > MaxLength)
        {
            return Result<Password>.Failure(Errors.Password.TooLong(MaxLength));
        }

        if (!Regex.IsMatch(value, PasswordPattern))
        {
            return Result<Password>.Failure(Errors.Password.InvalidFormat);
        }

        return Result<Password>.Success(new Password(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena de la contraseña.
    /// </summary>
    /// <returns>La contraseña como una cadena.</returns>
    public override string ToString() => Value;
}
