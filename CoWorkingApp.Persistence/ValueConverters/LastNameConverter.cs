using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkingApp.Persistence.ValueConverters;

/// <summary>
/// Proporciona la conversión entre <see cref="LastName"/> y <see cref="string"/> para la persistencia en la base de datos.
/// </summary>
public class LastNameConverter : ValueConverter<LastName, string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="LastNameConverter"/>.
    /// </summary>
    public LastNameConverter() : base(
        lastName => lastName,
        value => LastName.Create(value).Value)
    {
    }
}
