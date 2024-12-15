using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserEmail;

/// <summary>
/// Consulta para obtener las reservas por el correo electrónico del usuario.
/// </summary>
/// <param name="UserEmail">El correo electrónico del usuario.</param>
public record struct GetReservationsByUserEmailQuery(string UserEmail) : IQuery<IEnumerable<GetReservationsByUserEmailQueryResponse>>, IInputFilter
{
    /// <summary>
    /// Filtra y normaliza el correo electrónico del usuario.
    /// </summary>
    public void Filter()
    {
        UserEmail = UserEmail
            .GetValueOrDefault(string.Empty)
            .Trim()
            .ToLowerInvariant();
    }
}
