using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.DomainErrors;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa una fecha en el sistema.
/// </summary>
public readonly record struct Date
{
    /// <summary>
    /// Obtiene la fecha.
    /// </summary>
    public DateTime Value { get; }

    /// <summary>
    /// Constructor privado para inicializar la fecha.
    /// </summary>
    /// <param name="value">El valor de la fecha.</param>
    private Date(DateTime value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Date"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor de la fecha.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="Date"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<Date> Create(DateTime value)
    {
        if (value == default)
        {
            return Result<Date>.Failure(Errors.Date.Invalid);
        }

        return Result<Date>.Success(new(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena de la fecha.
    /// </summary>
    /// <returns>La fecha como una cadena.</returns>
    public override string ToString() => Value.ToString();//"o"

    /// <summary>
    /// Define una conversión implícita de <see cref="Date"/> a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="date">La instancia de <see cref="Date"/> a convertir.</param>
    public static implicit operator DateTime(Date date) => date.Value;
}
