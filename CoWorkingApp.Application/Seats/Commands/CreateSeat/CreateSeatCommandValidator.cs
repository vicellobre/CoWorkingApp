using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Seats.Commands.CreateSeat;

/// <summary>
/// Validador para el comando <see cref="CreateSeatCommand"/>.
/// </summary>
public class CreateSeatCommandValidator : AbstractValidator<CreateSeatCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateSeatCommandValidator"/>.
    /// </summary>
    public CreateSeatCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(SeatName.MinLength)
            .MaximumLength(SeatName.MaxLength)
            .Matches(SeatName.Pattern);

        RuleFor(x => x.Description)
            .NotNull()
            .MaximumLength(Description.MaxLength);
    }
}
