using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserName;

/// <summary>
/// Validador para el comando <see cref="UpdateUserNameCommand"/>.
/// </summary>
internal class UpdateUserNameCommandValidator : AbstractValidator<UpdateUserNameCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserNameCommandValidator"/>.
    /// </summary>
    public UpdateUserNameCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(FirstName.MinLength)
            .MaximumLength(FirstName.MaxLength)
            .Matches(FirstName.Pattern);

        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(LastName.MinLength)
            .MaximumLength(LastName.MaxLength)
            .Matches(LastName.Pattern);
    }
}
