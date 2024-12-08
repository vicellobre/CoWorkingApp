using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa la fila del asiento.
/// </summary>
public readonly record struct SeatRow
{
    /// <summary>
    /// Obtiene la fila del asiento.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Constructor privado para inicializar el valor de la fila del asiento.
    /// </summary>
    /// <param name="value">El valor de la fila del asiento.</param>
    private SeatRow(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="SeatRow"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor de la fila del asiento.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="SeatRow"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<SeatRow> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<SeatRow>.Failure(Errors.SeatRow.IsNullOrEmpty);
        }

        return Result<SeatRow>.Success(new SeatRow(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena de la fila del asiento.
    /// </summary>
    /// <returns>La fila del asiento como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="SeatRow"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="seatRow">El valor de <see cref="SeatRow"/> a convertir.</param>
    public static implicit operator string(SeatRow seatRow) => seatRow.Value;
}
