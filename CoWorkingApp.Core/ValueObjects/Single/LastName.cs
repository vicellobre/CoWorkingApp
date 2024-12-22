using System.Text.RegularExpressions;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Extensions;
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
    public const int MinLength = 2;

    /// <summary>
    /// Longitud máxima permitida para el apellido.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Patrón de expresión regular para validar el formato del apellido.
    /// </summary>
    public const string Pattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑçÇüÜàÀèÈìÌòÒùÙâêÊîôûäëïöüß\s]+$";

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
    public static Result<LastName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<LastName>.Failure(Errors.LastName.IsNullOrEmpty);
        }

        List<Error> errors = [];

        if (value.Length < MinLength)
        {
            errors.Add(Errors.LastName.TooShort(MinLength));
        }

        if (value.Length > MaxLength)
        {
            errors.Add(Errors.LastName.TooLong(MaxLength));
        }

        if (!Regex.IsMatch(value, Pattern))
        {
            errors.Add(Errors.LastName.InvalidFormat);
        }

        return errors.IsEmpty()
            ? Result<LastName>.Success(new(value))
            : Result<LastName>.Failure(errors);
    }

    /// <summary>
    /// Devuelve una representación en cadena del apellido.
    /// </summary>
    /// <returns>El apellido como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="LastName"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="lastName">El valor de <see cref="LastName"/> a convertir.</param>
    public static implicit operator string(LastName lastName) => lastName.Value;
}
