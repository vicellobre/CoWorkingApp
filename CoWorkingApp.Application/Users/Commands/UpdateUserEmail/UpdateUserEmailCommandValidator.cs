using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserEmail;

/// <summary>
/// Validador para el comando <see cref="UpdateUserEmailCommand"/>.
/// </summary>
internal class UpdateUserEmailCommandValidator : AbstractValidator<UpdateUserEmailCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserEmailCommandValidator"/>.
    /// </summary>
    public UpdateUserEmailCommandValidator()
    {
        RuleFor(x => x.Email)
           .NotNull()
           .NotEmpty()
           .MinimumLength(Email.MinLength)
           .MaximumLength(Email.MaxLength)
           .EmailAddress()
           .Matches(Email.EmailPattern);
    }
}
