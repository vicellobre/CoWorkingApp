using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Users.Commands.CreateUser;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Application.Seats.Commands.CreateSeat;

/// <summary>
/// Maneja el comando para crear un nuevo asiento.
/// </summary>
public sealed class CreateSeatCommandHandler : ICommandHandler<CreateSeatCommand, CreateSeatCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateSeatCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de asientos o la unidad de trabajo es null.</exception>
    public CreateSeatCommandHandler(IUnitOfWork unitOfWork, ISeatRepository seatRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="CreateSeatCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando de creación del asiento.</returns>
    public async Task<Result<CreateSeatCommandResponse>> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
    {
        SeatName name = SeatName.ConvertFromString(request.Name).Value;
        var seatResult = Seat.Create(
            Guid.NewGuid(),
            name.Number,
            name.Row,
            request.Description);

        Seat seat = seatResult.Value;
        if (!await _seatRepository.IsNameUniqueAsync(seat.Name, cancellationToken))
        {
            return Result.Failure<CreateSeatCommandResponse>(Errors.Seat.NameAlreadyInUse);
        }

        _seatRepository.Add(seat);
        await _unitOfWork.CommitAsync(cancellationToken);

        return (CreateSeatCommandResponse)seat;
    }
}
