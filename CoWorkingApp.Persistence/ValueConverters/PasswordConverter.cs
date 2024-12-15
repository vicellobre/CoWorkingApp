using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkingApp.Persistence.ValueConverters;

/// <summary>
/// Proporciona la conversión entre <see cref="Password"/> y <see cref="string"/> para la persistencia en la base de datos.
/// </summary>
public class PasswordConverter : ValueConverter<Password, string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="PasswordConverter"/>.
    /// </summary>
    public PasswordConverter() : base(
        password => password.Value,
        value => Password.Create(value).Value)
    {
    }
}
