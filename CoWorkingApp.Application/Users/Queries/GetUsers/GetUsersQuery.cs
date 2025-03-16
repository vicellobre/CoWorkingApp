using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Queries.GetUsers;

/// <summary>
/// Consulta para obtener usuarios.
/// </summary>
public record class GetUsersQuery : IQuery<GetUsersQueryResponse>;
