using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Seats.Commands.UpdateSeat;

/// <summary>
/// Validador para el comando <see cref="UpdateSeatCommand"/>.
/// </summary>
public class UpdateSeatCommandValidator : AbstractValidator<UpdateSeatCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateSeatCommandValidator"/>.
    /// </summary>
    public UpdateSeatCommandValidator()
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
