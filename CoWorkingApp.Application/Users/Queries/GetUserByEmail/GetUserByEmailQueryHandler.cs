using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// Maneja la consulta para obtener un usuario por su correo electrónico.
/// </summary>
public sealed class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, GetUserByEmailResponse>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetUserByEmailQueryHandler"/>.
    /// </summary>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de usuarios es null.</exception>
    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetUserByEmailQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta de la consulta para obtener un usuario por su correo electrónico.</returns>
    public async Task<Result<GetUserByEmailResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);

        var user = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);
        if (user is null)
        {
            return Result.Failure<GetUserByEmailResponse>(Errors.User.EmailNotExist(request.Email));
        }

        return (GetUserByEmailResponse)user;
    }
}
