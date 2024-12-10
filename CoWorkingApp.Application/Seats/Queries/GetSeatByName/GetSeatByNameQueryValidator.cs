using CoWorkingApp.Core.ValueObjects.Composite;
using FluentValidation;

namespace CoWorkingApp.Application.Seats.Queries.GetSeatByName;

/// <summary>
/// Validador para la consulta <see cref="GetSeatByNameQuery"/>.
/// </summary>
internal class GetSeatByNameQueryValidator : AbstractValidator<GetSeatByNameQuery>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetSeatByNameQueryValidator"/>.
    /// </summary>
    public GetSeatByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(SeatName.MinLength)
            .MaximumLength(SeatName.MaxLength)
            .Matches(SeatName.Pattern);
    }
}
