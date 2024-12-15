using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Seats.Commands.DeleteSeat;

/// <summary>
/// Maneja el comando para eliminar un asiento.
/// </summary>
public sealed class DeleteSeatCommandHandler : ICommandHandler<DeleteSeatCommand, DeleteSeatCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DeleteSeatCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de asientos o la unidad de trabajo es null.</exception>
    public DeleteSeatCommandHandler(IUnitOfWork unitOfWork, ISeatRepository seatRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="DeleteSeatCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando de eliminación del asiento.</returns>
    public async Task<Result<DeleteSeatCommandResponse>> Handle(DeleteSeatCommand request, CancellationToken cancellationToken)
    {
        var seat = await _seatRepository.GetByIdAsync(request.SeatId, cancellationToken);
        if (seat == null)
        {
            return Result.Failure<DeleteSeatCommandResponse>(Errors.Seat.NotFound(request.SeatId));
        }

        _seatRepository.Remove(seat);
        await _unitOfWork.CommitAsync(cancellationToken);

        return (DeleteSeatCommandResponse)seat;
    }
}
