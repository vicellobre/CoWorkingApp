using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkingApp.Persistence.ValueConverters;

/// <summary>
/// Proporciona la conversión entre <see cref="Date"/> y <see cref="DateTime"/> para la persistencia en la base de datos.
/// </summary>
public class DateConverter : ValueConverter<Date, DateTime>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DateConverter"/>.
    /// </summary>
    public DateConverter() : base(
        date => date,
        dateTime => Date.Create(dateTime).Value)
    {
    }
}
