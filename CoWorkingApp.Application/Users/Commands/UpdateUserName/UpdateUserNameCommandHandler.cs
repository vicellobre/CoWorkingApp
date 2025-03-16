using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.Errors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserName;

/// <summary>
/// Manejador para el comando <see cref="UpdateUserNameCommand"/>.
/// </summary>
public sealed class UpdateUserNameCommandHandler : ICommandHandler<UpdateUserNameCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserNameCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">El <see cref="IUnitOfWork"/> utilizado para gestionar transacciones.</param>
    /// <param name="userRepository">El <see cref="IUserRepository"/> utilizado para acceder a los datos del usuario.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el <paramref name="unitOfWork"/> o el <paramref name="userRepository"/> son <see langword="null"/>.</exception>
    public UpdateUserNameCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja el comando <see cref="UpdateUserNameCommand"/> para actualizar el nombre de un usuario.
    /// </summary>
    /// <param name="request">El comando de actualización del nombre del usuario.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>Un <see cref="Result"/> indicando el éxito o fracaso de la operación.</returns>
    public async Task<Result> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        var fullNameResult = FullName.Create(request.FirstName, request.LastName);
        if (fullNameResult.IsFailure)
        {
            return Result.Failure(fullNameResult.Errors);
        }

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure(ERRORS.User.NotFound(request.UserId));
        }

        FullName fullName = fullNameResult.Value;
        user.ChangeName(fullName.FirstName, fullName.LastName);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
