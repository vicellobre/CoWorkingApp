using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.Contracts.UnitOfWork;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Maneja el comando para crear una nueva reserva.
/// </summary>
public sealed class CreateReservationCommandHandler : ICommandHandler<CreateReservationCommand, CreateReservationCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ISeatRepository _seatRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateReservationCommandHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">La unidad de trabajo.</param>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <param name="seatRepository">El repositorio de asientos.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando alguno de los repositorios o la unidad de trabajo es null.</exception>
    public CreateReservationCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, ISeatRepository seatRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
    }

    /// <summary>
    /// Maneja la lógica para el comando <see cref="CreateReservationCommand"/>.
    /// </summary>
    /// <param name="request">La solicitud del comando.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>La respuesta del comando para crear una nueva reserva.</returns>
    public async Task<Result<CreateReservationCommandResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Result.Failure<CreateReservationCommandResponse>(Errors.User.NotFound(request.UserId));
        }

        var seat = await _seatRepository.GetByIdAsNoTrackingAsync(request.SeatId, cancellationToken);
        if (seat == null)
        {
            return Result.Failure<CreateReservationCommandResponse>(Errors.Seat.NotFound(request.SeatId));
        }

        var reservationResult = Reservation.Create(
            Guid.NewGuid(),
            request.Date,
            user,
            seat);

        Reservation reservation = reservationResult.Value;

        bool isNotAvailable = !await _seatRepository.IsAvailable(request.SeatId, reservation.Date, cancellationToken);
        if (isNotAvailable)
        {
            return Result.Failure<CreateReservationCommandResponse>(Errors.Seat.NotAvailable(request.SeatId, request.Date));
        }

        user.AddReservation(reservation);

        await _unitOfWork.CommitAsync(cancellationToken);

        return (CreateReservationCommandResponse)reservation;
    }
}
