using CoWorkingApp.Core.ValueObjects.Composite;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkingApp.Persistence.ValueConverters;

/// <summary>
/// Proporciona la conversión entre <see cref="SeatName"/> y <see cref="string"/> para la persistencia en la base de datos.
/// </summary>
public class SeatNameConverter : ValueConverter<SeatName, string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="SeatNameConverter"/>.
    /// </summary>
    public SeatNameConverter() : base(
        seatName => seatName.ToString(),
        value => SeatName.CreateFromString(value).Value)
    {
    }
}
