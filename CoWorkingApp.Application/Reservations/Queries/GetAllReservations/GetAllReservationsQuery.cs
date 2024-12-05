using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetAllReservations;

/// <summary>
/// Consulta para obtener todas las reservas.
/// </summary>
public readonly record struct GetAllReservationsQuery : IQuery<IEnumerable<GetAllReservationsQueryResponse>>;
