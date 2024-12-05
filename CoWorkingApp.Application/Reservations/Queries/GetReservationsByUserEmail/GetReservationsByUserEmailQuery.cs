using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserEmail;

/// <summary>
/// Consulta para obtener las reservas por el correo electrónico del usuario.
/// </summary>
/// <param name="UserEmail">El correo electrónico del usuario.</param>
public readonly record struct GetReservationsByUserEmailQuery(string UserEmail) : IQuery<IEnumerable<GetReservationsByUserEmailQueryResponse>>;
