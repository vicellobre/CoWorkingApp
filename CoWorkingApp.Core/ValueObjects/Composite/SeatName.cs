using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Core.ValueObjects.Composite;

/// <summary>
/// Representa el nombre del asiento, compuesto por número de asiento y fila.
/// </summary>
public record struct SeatName
{
    /// <summary>
    /// El carácter separador utilizado en el nombre del asiento.
    /// </summary>
    private const string Separator = "-";

    /// <summary>
    /// El número esperado de partes en el nombre del asiento.
    /// </summary>
    private const int ExpectedPartCount = 2;

    /// <summary>
    /// Longitud máxima permitida para el nombre.
    /// </summary>
    public const int MaxLength = 50;

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
    public readonly string Value => $"{Row.Value}{Separator}{Number.Value}";

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
    private SeatName(SeatRow seatRow, SeatNumber seatNumber)
    {
        Row = seatRow;
        Number = seatNumber;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="SeatName"/> con los valores especificados.
    /// </summary>
    /// <param name="seatRow">La fila del asiento.</param>
    /// <param name="seatNumber">El número del asiento.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="SeatName"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<SeatName> Create(string? seatRow, string? seatNumber)
    {
        var seatRowResult = SeatRow.Create(seatRow);
        if (seatRowResult.IsFailure)
        {
            return Result<SeatName>.Failure(seatRowResult.FirstError);
        }

        var seatNumberResult = SeatNumber.Create(seatNumber);
        if (seatNumberResult.IsFailure)
        {
            return Result<SeatName>.Failure(seatNumberResult.FirstError);
        }

        return Result<SeatName>.Success(new SeatName(seatRowResult.Value, seatNumberResult.Value));
    }

    /// <summary>
    /// Convierte una cadena en una instancia de <see cref="SeatName"/>.
    /// </summary>
    /// <param name="value">El valor en cadena para convertir.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="SeatName"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<SeatName> ConvertFromString(string value)
    {
        if (!value.Contains(Separator))
        {
            return Result.Failure<SeatName>(Errors.SeatName.InvalidFormat);
        }

        var parts = value.Split(Separator);
        if (parts.Length != ExpectedPartCount)
        {
            return Result.Failure<SeatName>(Errors.SeatName.InvalidFormat);
        }

        return Create(parts.First(), parts.Last());
    }

    /// <summary>
    /// Devuelve una representación en cadena del nombre del asiento.
    /// </summary>
    /// <returns>El nombre del asiento como una cadena.</returns>
    public override readonly string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="SeatName"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="seatName">El valor de <see cref="SeatName"/> a convertir.</param>
    public static implicit operator string(SeatName seatName) => seatName.Value;
}
