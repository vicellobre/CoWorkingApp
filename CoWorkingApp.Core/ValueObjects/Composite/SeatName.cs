using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Core.ValueObjects.Composite;

/// <summary>
/// Representa el nombre del asiento, compuesto por número de asiento y fila.
/// </summary>
public record struct SeatName
{
    /// <summary>
    /// Obtiene o establece el número del asiento.
    /// </summary>
    public SeatNumber Number { get; set; }

    /// <summary>
    /// Obtiene o establece la fila del asiento.
    /// </summary>
    public SeatRow Row { get; set; }

    /// <summary>
    /// Obtiene el nombre completo del asiento en formato "Fila-Número".
    /// </summary>
    public readonly string Value => $"{Row.Value}-{Number.Value}";

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="SeatName"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public SeatName()
    {
        throw new InvalidOperationException("Use the static Create method to instantiate SeatName.");
    }

    /// <summary>
    /// Constructor privado para inicializar el nombre del asiento.
    /// </summary>
    /// <param name="seatNumber">El número del asiento.</param>
    /// <param name="seatRow">La fila del asiento.</param>
    private SeatName(SeatNumber seatNumber, SeatRow seatRow)
    {
        Number = seatNumber;
        Row = seatRow;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="SeatName"/> con los valores especificados.
    /// </summary>
    /// <param name="seatNumber">El número del asiento.</param>
    /// <param name="seatRow">La fila del asiento.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="SeatName"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<SeatName> Create(string seatNumber, string seatRow)
    {
        var seatNumberResult = SeatNumber.Create(seatNumber);
        if (seatNumberResult.IsFailure)
        {
            return Result<SeatName>.Failure(seatNumberResult.FirstError);
        }

        var seatRowResult = SeatRow.Create(seatRow);
        if (seatRowResult.IsFailure)
        {
            return Result<SeatName>.Failure(seatRowResult.FirstError);
        }

        return Result<SeatName>.Success(new SeatName(seatNumberResult.Value, seatRowResult.Value));
    }

    /// <summary>
    /// Devuelve una representación en cadena del nombre del asiento.
    /// </summary>
    /// <returns>El nombre del asiento como una cadena.</returns>
    public override readonly string ToString() => Value;
}
