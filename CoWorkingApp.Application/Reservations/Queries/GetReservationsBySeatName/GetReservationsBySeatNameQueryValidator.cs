using CoWorkingApp.Core.ValueObjects.Composite;
using FluentValidation;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsBySeatName;

/// <summary>
/// Validador para la consulta <see cref="GetReservationsBySeatNameQuery"/>.
/// </summary>
public class GetReservationsBySeatNameQueryValidator : AbstractValidator<GetReservationsBySeatNameQuery>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsBySeatNameQueryValidator"/>.
    /// </summary>
    public GetReservationsBySeatNameQueryValidator()
    {
        RuleFor(x => x.SeatName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(SeatName.MinLength)
            .MaximumLength(SeatName.MaxLength)
            .Matches(SeatName.Pattern);
    }
}
