using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserPassword;

/// <summary>
/// Validador para el comando <see cref="UpdateUserPasswordCommand"/>.
/// </summary>
internal class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserPasswordCommandValidator"/>.
    /// </summary>
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(Password.MinLength)
            .MaximumLength(Password.MaxLength)
            .Matches(Password.Pattern);
    }
}
