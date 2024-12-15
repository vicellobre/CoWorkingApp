using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Queries.GetAllUsers;

/// <summary>
/// Consulta para obtener todos los usuarios.
/// </summary>
public readonly record struct GetAllUsersQuery() : IQuery<IEnumerable<GetAllUsersQueryResponse>>;
