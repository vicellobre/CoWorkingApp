using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Application.Users.Commands.AuthenticateUser;

/// <summary>
/// Maneja el comando para autenticar un usuario.
/// </summary>
public sealed class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, AuthenticateUserCommandResponse>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="AuthenticateUserCommandHandler"/>.
    /// </summary>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de usuarios es null.</exception>
    public AuthenticateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="AuthenticateUserCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando de autenticación del usuario.</returns>
    public async Task<Result<AuthenticateUserCommandResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var passwordResult = Password.Create(request.Password);

        var user = await _userRepository.AuthenticateAsync(emailResult.Value, passwordResult.Value, cancellationToken);
        if (user is null)
        {
            return Result.Failure<AuthenticateUserCommandResponse>(Errors.User.InvalidCredentials);
        }

        return (AuthenticateUserCommandResponse)user;
    }
}
