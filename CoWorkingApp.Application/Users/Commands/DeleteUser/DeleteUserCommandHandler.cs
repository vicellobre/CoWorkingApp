using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Users.Commands.DeleteUser;

/// <summary>
/// Maneja el comando para eliminar un usuario.
/// </summary>
public sealed record class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, DeleteUserCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DeleteUserCommandHandler"/>.
    /// </summary>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de usuarios o la unidad de trabajo es null.</exception>
    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="DeleteUserCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando de eliminación del usuario.</returns>
    public async Task<Result<DeleteUserCommandResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Result.Failure<DeleteUserCommandResponse>(Errors.User.NotFound(request.UserId));
        }

        _userRepository.Remove(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return (DeleteUserCommandResponse)user;
    }
}
