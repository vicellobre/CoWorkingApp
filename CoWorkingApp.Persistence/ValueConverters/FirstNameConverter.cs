using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkingApp.Persistence.ValueConverters;

/// <summary>
/// Proporciona la conversión entre <see cref="FirstName"/> y <see cref="string"/> para la persistencia en la base de datos.
/// </summary>
public class FirstNameConverter : ValueConverter<FirstName, string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="FirstNameConverter"/>.
    /// </summary>
    public FirstNameConverter() : base(
        firstName => firstName,
        value => FirstName.Create(value).Value)
    {
    }
}
