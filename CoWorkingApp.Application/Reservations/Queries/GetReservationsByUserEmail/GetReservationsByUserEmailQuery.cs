using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserEmail;

/// <summary>
/// Consulta para obtener las reservas por el correo electrónico del usuario.
/// </summary>
public record class GetReservationsByUserEmailQuery : IQuery<GetReservationsByUserEmailQueryResponse>, IInputFilter
{
    /// <summary>
    /// El correo electrónico del usuario.
    /// </summary>
    public string UserEmail { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByUserEmailQuery"/>.
    /// </summary>
    /// <param name="userEmail">El correo electrónico del usuario.</param>
    public GetReservationsByUserEmailQuery(string userEmail)
    {
        UserEmail = userEmail;
    }

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
