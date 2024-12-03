using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Commands.CreateUser;

/// <summary>
/// Maneja el comando para crear un nuevo usuario.
/// </summary>
public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateUserCommandHandler"/>.
    /// </summary>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de usuarios o la unidad de trabajo es null.</exception>
    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="CreateUserCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>El identificador del usuario creado.</returns>
    public async Task<Result<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = User.Create(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        User user = userResult.Value;
        if (!await _userRepository.IsEmailUniqueAsync(user.Credentials.Email, cancellationToken))
        {
            return Result.Failure<CreateUserCommandResponse>(Errors.User.EmailAlreadyInUse);
        }

        _userRepository.Add(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return (CreateUserCommandResponse)user;
    }
}
