using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa una descripción en el sistema.
/// </summary>
public readonly record struct Description
{
    /// <summary>
    /// Longitud máxima permitida para la descripción.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Obtiene el valor de la descripción.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Constructor privado para inicializar el valor de la descripción.
    /// </summary>
    /// <param name="value">El valor de la descripción.</param>
    private Description(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Description"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor de la descripción.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="Description"/> si es exitoso.</returns>
    public static Result<Description> Create(string? value)
    {
        value ??= string.Empty;
        return Result<Description>.Success(new(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena de la descripción.
    /// </summary>
    /// <returns>La descripción como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="Description"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="description">El valor de <see cref="Description"/> a convertir.</param>
    public static implicit operator string(Description description) => description.Value;
}
