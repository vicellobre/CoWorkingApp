using FluentValidation;

namespace CoWorkingApp.Application.Reservations.Commands.UpdateReservation;

/// <summary>
/// Validador para el comando <see cref="UpdateReservationCommand"/>.
/// </summary>
public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateReservationCommandValidator"/>.
    /// </summary>
    public UpdateReservationCommandValidator()
    {
        RuleFor(x => x.Date)
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.Now.Date);
    }
}
