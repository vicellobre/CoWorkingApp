using FluentValidation;

namespace CoWorkingApp.Application.Reservations.Commands.CreateReservation;

/// <summary>
/// Validador para el comando <see cref="CreateReservationCommand"/>.
/// </summary>
public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateReservationCommandValidator"/>.
    /// </summary>
    public CreateReservationCommandValidator()
    {
        // Validación para la fecha
        RuleFor(x => x.Date)
            .NotNull()
            .GreaterThanOrEqualTo(DateTime.Now.Date); // La fecha no debe ser menor a hoy

        // Validación para el ID del usuario
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty); // El ID del usuario no debe ser un GUID vacío

        // Validación para el ID del asiento
        RuleFor(x => x.SeatId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty); // El ID del asiento no debe ser un GUID vacío
    }
}
