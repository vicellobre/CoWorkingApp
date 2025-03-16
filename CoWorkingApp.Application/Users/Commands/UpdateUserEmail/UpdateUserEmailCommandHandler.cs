using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.Errors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserEmail;

/// <summary>
/// Manejador para el comando <see cref="UpdateUserEmailCommand"/>.
/// </summary>
public sealed class UpdateUserEmailCommandHandler : ICommandHandler<UpdateUserEmailCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserEmailCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">El <see cref="IUnitOfWork"/> utilizado para gestionar transacciones.</param>
    /// <param name="userRepository">El <see cref="IUserRepository"/> utilizado para acceder a los datos del usuario.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el <paramref name="unitOfWork"/> o el <paramref name="userRepository"/> son <see langword="null"/>.</exception>
    public UpdateUserEmailCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja el comando <see cref="UpdateUserEmailCommand"/> para actualizar el correo electrónico de un usuario.
    /// </summary>
    /// <param name="request">El comando de actualización del correo electrónico del usuario.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>Un <see cref="Result"/> indicando el éxito o fracaso de la operación.</returns>
    public async Task<Result> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Errors);
        }

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Result.Failure(ERRORS.User.NotFound(request.UserId));
        }

        Email email = emailResult.Value;
        user.ChangeEmail(email);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
