using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa el número de asiento.
/// </summary>
public readonly record struct SeatNumber
{
    /// <summary>
    /// Obtiene el número del asiento.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="SeatNumber"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public SeatNumber()
    {
        //throw new InvalidOperationException("Use the static Create method to instantiate SeatNumber.");
    }

    /// <summary>
    /// Constructor privado para inicializar el valor del número de asiento.
    /// </summary>
    /// <param name="value">El valor del número de asiento.</param>
    private SeatNumber(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="SeatNumber"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor del número de asiento.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="SeatNumber"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<SeatNumber> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<SeatNumber>.Failure(Errors.SeatNumber.IsNullOrEmpty);
        }

        return Result<SeatNumber>.Success(new SeatNumber(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena del número de asiento.
    /// </summary>
    /// <returns>El número de asiento como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="SeatNumber"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="seatNumber">El valor de <see cref="SeatNumber"/> a convertir.</param>
    public static implicit operator string(SeatNumber seatNumber) => seatNumber.Value;
}
