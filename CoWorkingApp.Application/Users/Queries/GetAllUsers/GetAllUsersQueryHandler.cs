using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Users.DTO;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Queries.GetAllUsers;

/// <summary>
/// Maneja la consulta para obtener todos los usuarios.
/// </summary>
public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetAllUsersQueryResponse>
{
    /// <summary>
    /// El repositorio de usuarios.
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetAllUsersQueryHandler"/>.
    /// </summary>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de usuarios es null.</exception>
    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetAllUsersQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Un resultado que contiene una lista de respuestas de la consulta para obtener todos los usuarios.</returns>
    public async Task<Result<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsNoTrackingAsync(cancellationToken);

        var userDtos = users?.Select(user => (UserDto)user).ToList() ?? [];

        return new GetAllUsersQueryResponse(userDtos);
    }
}
