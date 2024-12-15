using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatByName;

/// <summary>
/// Consulta para obtener un asiento por su nombre.
/// </summary>
/// <param name="Name">El nombre del asiento.</param>
public record struct GetSeatByNameQuery(string Name) : IQuery<GetSeatByNameQueryResponse>, IInputFilter
{
    /// <summary>
    /// Filtra y normaliza el nombre del asiento.
    /// </summary>
    public void Filter()
    {
        Name = Name
            .GetValueOrDefault(string.Empty)
            .Trim();
    }
}
