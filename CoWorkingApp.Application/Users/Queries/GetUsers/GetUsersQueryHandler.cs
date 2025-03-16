using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Users.Extensions;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Errors;
using CoWorkingApp.Core.Shared;
using Microsoft.IdentityModel.Tokens;

namespace CoWorkingApp.Application.Users.Queries.GetUsers;

/// <summary>
/// Maneja la consulta para obtener usuarios.
/// </summary>
public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersQueryResponse>
{
    /// <summary>
    /// El repositorio de usuarios.
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetUsersQueryHandler"/>.
    /// </summary>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de usuarios es null.</exception>
    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetUsersQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Un resultado que contiene una lista de respuestas de la consulta para obtener usuarios.</returns>
    public async Task<Result<GetUsersQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsNoTrackingAsync(cancellationToken);

        if (users.IsNullOrEmpty())
        {
            return Result.Failure<GetUsersQueryResponse>(ERRORS.User.NoUsersFound);
        }

        var userResponses = users.Select(user => user.ToUserResponse());

        return new GetUsersQueryResponse(userResponses);
    }
}

