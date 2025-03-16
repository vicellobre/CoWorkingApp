using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Queries.GetUserById;

/// <summary>
/// Consulta para obtener un usuario por su identificador.
/// </summary>
public record class GetUserByIdQuery : IQuery<GetUserByIdQueryResponse>
{
    /// <summary>
    /// El identificador del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetUserByIdQuery"/>.
    /// </summary>
    /// <param name="userId">El identificador del usuario.</param>
    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}
