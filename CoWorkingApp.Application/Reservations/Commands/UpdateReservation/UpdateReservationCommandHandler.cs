using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Commands.UpdateReservation;

/// <summary>
/// Maneja el comando para actualizar una reserva.
/// </summary>
public sealed class UpdateReservationCommandHandler : ICommandHandler<UpdateReservationCommand, UpdateReservationCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateReservationCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando alguno de los repositorios o la unidad de trabajo es null.</exception>
    public UpdateReservationCommandHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository,
        IUserRepository userRepository, ISeatRepository seatRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="UpdateReservationCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando para actualizar una reserva.</returns>
    public async Task<Result<UpdateReservationCommandResponse>> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);
        if (reservation == null)
        {
            return Result.Failure<UpdateReservationCommandResponse>(Errors.Reservation.NotFound(request.ReservationId));
        }

        if (reservation.UserId != request.UserId)
        {
            var user = await _userRepository.GetByIdAsNoTrackingAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Failure<UpdateReservationCommandResponse>(Errors.User.NotFound(request.UserId));
            }

            reservation.ChangeUser(user);
        }

        if (reservation.SeatId != request.SeatId)
        {
            var seat = await _seatRepository.GetByIdAsNoTrackingAsync(request.SeatId, cancellationToken);
            if (seat == null)
            {
                return Result.Failure<UpdateReservationCommandResponse>(Errors.Seat.NotFound(request.SeatId));
            }

            reservation.ChangeSeat(seat);
        }

        if (reservation.Date != request.Date)
        {
            var changeDateResult = reservation.ChangeDate(request.Date);
            if (changeDateResult.IsFailure)
            {
                return Result.Failure<UpdateReservationCommandResponse>(changeDateResult.Errors); ;
            }
        }

        bool isAvailable = await _seatRepository.IsAvailable(reservation.SeatId, reservation.Date, cancellationToken);
        if (!isAvailable)
        {
            return Result.Failure<UpdateReservationCommandResponse>(Errors.Seat.NotAvailable(request.SeatId, request.Date));
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return (UpdateReservationCommandResponse)reservation;
    }
}
