using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatByName;

/// <summary>
/// Consulta para obtener un asiento por su nombre.
/// </summary>
/// <param name="Name">El nombre del asiento.</param>
public readonly record struct GetSeatByNameQuery(string Name) : IQuery<GetSeatByNameQueryResponse>;
