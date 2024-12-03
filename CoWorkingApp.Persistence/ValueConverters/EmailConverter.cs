using CoWorkingApp.Core.ValueObjects.Single;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkingApp.Persistence.ValueConverters;

/// <summary>
/// Proporciona la conversión entre <see cref="Email"/> y <see cref="string"/> para la persistencia en la base de datos.
/// </summary>
public class EmailConverter : ValueConverter<Email, string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="EmailConverter"/>.
    /// </summary>
    public EmailConverter() : base(
        email => email,//Permite que Email sea almacenado como una cadena en la base de datos.
        value => Email.Create(value).Value)//Permite que una cadena recuperada de la base de datos sea convertida nuevamente a un Email
    {
    }
}
