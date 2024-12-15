using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Queries.GetUserById;

/// <summary>
/// Consulta para obtener un usuario por su identificador.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
public readonly record struct GetUserByIdQuery(Guid UserId) : IQuery<GetUserByIdQueryResponse>;
