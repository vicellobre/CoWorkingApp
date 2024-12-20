using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Application.Seats.Commands.UpdateSeat;

/// <summary>
/// Maneja el comando para actualizar un asiento.
/// </summary>
public sealed class UpdateSeatCommandHandler : ICommandHandler<UpdateSeatCommand, UpdateSeatCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateSeatCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de asientos o la unidad de trabajo es null.</exception>
    public UpdateSeatCommandHandler(IUnitOfWork unitOfWork, ISeatRepository seatRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="UpdateSeatCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando de actualización del asiento.</returns>
    public async Task<Result<UpdateSeatCommandResponse>> Handle(UpdateSeatCommand request, CancellationToken cancellationToken)
    {
        var seat = await _seatRepository.GetByIdAsync(request.SeatId, cancellationToken);
        if (seat is null)
        {
            return Result.Failure<UpdateSeatCommandResponse>(Errors.Seat.NotFound(request.SeatId));
        }

        SeatName name = SeatName.CreateFromString(request.Name).Value;
        seat.ChangeName(name.Number, name.Row);
        seat.ChangeDescription(request.Description);

        await _unitOfWork.CommitAsync(cancellationToken);

        return (UpdateSeatCommandResponse)seat;
    }
}
