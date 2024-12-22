using System.Text.RegularExpressions;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Extensions;
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
    public const int MinLength = 5;

    /// <summary>
    /// Longitud máxima permitida para la contraseña.
    /// </summary>
    public const int MaxLength = 100;

    /// <summary>
    /// Patrón de expresión regular para validar el formato de la contraseña.
    /// Debe contener al menos una letra minúscula, una letra mayúscula, un dígito y un carácter especial.
    /// </summary>
    public const string Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).*$";

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
    public static Result<Password> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Password>.Failure(Errors.Password.IsNullOrEmpty);
        }

        List<Error> errors = [];

        if (value.Length < MinLength)
        {
            errors.Add(Errors.Password.TooShort(MinLength));
        }

        if (value.Length > MaxLength)
        {
            errors.Add(Errors.Password.TooLong(MaxLength));
        }

        if (!Regex.IsMatch(value, Pattern))
        {
            errors.Add(Errors.Password.InvalidFormat);
        }

        return errors.IsEmpty()
            ? Result<Password>.Success(new(value))
            : Result<Password>.Failure(errors);
    }

    /// <summary>
    /// Devuelve una representación en cadena de la contraseña.
    /// </summary>
    /// <returns>La contraseña como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="Password"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="password">El valor de <see cref="Password"/> a convertir.</param>
    public static implicit operator string(Password password) => password.Value;
}
