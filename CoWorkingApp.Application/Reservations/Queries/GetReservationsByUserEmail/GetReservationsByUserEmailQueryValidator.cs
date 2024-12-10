using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Reservations.Queries.GetReservationsByUserEmail;

/// <summary>
/// Validador para la consulta <see cref="GetReservationsByUserEmailQuery"/>.
/// </summary>
internal class GetReservationsByUserEmailQueryValidator : AbstractValidator<GetReservationsByUserEmailQuery>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetReservationsByUserEmailQueryValidator"/>.
    /// </summary>
    public GetReservationsByUserEmailQueryValidator()
    {
        RuleFor(x => x.UserEmail)
           .NotNull()
           .NotEmpty()
           .MinimumLength(Email.MinLength)
           .MaximumLength(Email.MaxLength)
           .EmailAddress()
           .Matches(Email.EmailPattern);
    }
}
