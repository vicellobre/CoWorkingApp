using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkingApp.Persistence.ValueConverters;

/// <summary>
/// Proporciona la conversión entre <see cref="Description"/> y <see cref="string"/> para la persistencia en la base de datos.
/// </summary>
public class DescriptionConverter : ValueConverter<Description, string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DescriptionConverter"/>.
    /// </summary>
    public DescriptionConverter() : base(
        description => description,
        value => Description.Create(value).Value)
    {
    }
}
