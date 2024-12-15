using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Commands.DeleteReservation;

/// <summary>
/// Maneja el comando para eliminar una reserva.
/// </summary>
public sealed class DeleteReservationCommandHandler : ICommandHandler<DeleteReservationCommand, DeleteReservationCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DeleteReservationCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    public DeleteReservationCommandHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="DeleteReservationCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando para eliminar una reserva.</returns>
    public async Task<Result<DeleteReservationCommandResponse>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsNoTrackingAsync(request.ReservationId, cancellationToken);
        if (reservation == null)
        {
            return Result.Failure<DeleteReservationCommandResponse>(Errors.Reservation.NotFound(request.ReservationId));
        }

        _reservationRepository.Remove(reservation);
        await _unitOfWork.CommitAsync(cancellationToken);

        return (DeleteReservationCommandResponse)reservation;
    }
}
