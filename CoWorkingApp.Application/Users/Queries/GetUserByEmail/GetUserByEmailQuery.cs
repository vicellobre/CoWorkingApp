using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// Consulta para obtener un usuario por su correo electrónico.
/// </summary>
/// <param name="Email">El correo electrónico del usuario.</param>
public readonly record struct GetUserByEmailQuery(string Email) : IQuery<GetUserByEmailResponse>;
