using FluentValidation;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByDate;

/// <summary>
/// Validador para la consulta <see cref="GetReservationsByDateQuery"/>.
/// </summary>
internal class GetReservationsByDateQueryValidator : AbstractValidator<GetReservationsByDateQuery>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByDateQueryValidator"/>.
    /// </summary>
    public GetReservationsByDateQueryValidator()
    {
        RuleFor(x => x.DateTime)
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.Now.Date);
    }
}
