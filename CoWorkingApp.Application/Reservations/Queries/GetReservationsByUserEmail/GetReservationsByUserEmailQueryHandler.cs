using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Core.Contracts.Repositories;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserEmail;

/// <summary>
/// Maneja la consulta para obtener las reservas por el correo electrónico del usuario.
/// </summary>
public sealed class GetReservationsByUserEmailQueryHandler : IQueryHandler<GetReservationsByUserEmailQuery, IEnumerable<GetReservationsByUserEmailQueryResponse>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByUserEmailQueryHandler"/>.
    /// </summary>
    /// <param name="reservationRepository">El repositorio de reservas.</param>
    /// <param name="userRepository">El repositorio de usuarios.</param>
    /// <exception cref="ArgumentNullException">Lanzado cuando el repositorio de reservas o el repositorio de usuarios es null.</exception>
    public GetReservationsByUserEmailQueryHandler(IReservationRepository reservationRepository, IUserRepository userRepository)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Maneja la lógica para la consulta <see cref="GetReservationsByUserEmailQuery"/>.
    /// </summary>
    /// <param name="request">La solicitud de la consulta.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Una colección de respuestas de la consulta para obtener las reservas por el correo electrónico del usuario.</returns>
    public async Task<Result<IEnumerable<GetReservationsByUserEmailQueryResponse>>> Handle(GetReservationsByUserEmailQuery request, CancellationToken cancellationToken)
    {
        Email email = Email.Create(request.UserEmail).Value;

        bool notFound = await _userRepository.GetByEmailAsync(email, cancellationToken) is null;
        if (notFound)
        {
            return Result.Failure<IEnumerable<GetReservationsByUserEmailQueryResponse>>(Errors.User.EmailNotExist(request.UserEmail));
        }

        var reservations = await _reservationRepository.GetByUserEmailAsNoTrackingAsync(email, cancellationToken);
        return reservations.Select(reservation => (GetReservationsByUserEmailQueryResponse)reservation).ToList();
    }
}
