using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatByName;

/// <summary>
/// Consulta para obtener un asiento por su nombre.
/// </summary>
public record class GetSeatByNameQuery : IQuery<GetSeatByNameQueryResponse>, IInputFilter
{
    /// <summary>
    /// El nombre del asiento.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetSeatByNameQuery"/>.
    /// </summary>
    /// <param name="name">El nombre del asiento.</param>
    public GetSeatByNameQuery(string name)
    {
        Name = name;
    }

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
