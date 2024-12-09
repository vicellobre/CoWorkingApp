using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Users.Commands.CreateUser;

/// <summary>
/// Validador para el comando <see cref="CreateUserCommand"/>.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateUserCommandValidator"/>.
    /// </summary>
    public CreateUserCommandValidator()
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

        RuleFor(x => x.Email)
           .NotNull()
           .NotEmpty()
           .MinimumLength(Email.MinLength)
           .MaximumLength(Email.MaxLength)
           .EmailAddress()
           .Matches(Email.EmailPattern);

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(Password.MinLength)
            .MaximumLength(Password.MaxLength)
            .Matches(Password.Pattern);
    }
}
