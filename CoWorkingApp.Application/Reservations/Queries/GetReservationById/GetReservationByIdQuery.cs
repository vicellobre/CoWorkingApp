using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationById;

/// <summary>
/// Consulta para obtener una reserva por su identificador.
/// </summary>
/// <param name="ReservationId">El identificador de la reserva.</param>
public readonly record struct GetReservationByIdQuery(Guid ReservationId) : IQuery<GetReservationByIdQueryResponse>;
