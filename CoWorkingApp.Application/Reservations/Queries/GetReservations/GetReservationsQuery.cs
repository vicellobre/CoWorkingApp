using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservations;

/// <summary>
/// Consulta para obtener reservas.
/// </summary>
public record class GetReservationsQuery : IQuery<GetReservationsQueryResponse>;
