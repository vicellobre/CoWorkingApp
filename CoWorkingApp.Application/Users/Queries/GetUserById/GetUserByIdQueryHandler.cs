using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Queries.GetUserById;

/// <summary>
/// Maneja la consulta para obtener un usuario por su identificador.
/// </summary>
public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetUserByIdQueryHandler"/>.
    /// </summary>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de usuarios es null.</exception>
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetUserByIdQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta de la consulta para obtener un usuario por su identificador.</returns>
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<GetUserByIdQueryResponse>(Errors.User.NotFound(request.UserId));
        }

        return (GetUserByIdQueryResponse)user;
    }
}
