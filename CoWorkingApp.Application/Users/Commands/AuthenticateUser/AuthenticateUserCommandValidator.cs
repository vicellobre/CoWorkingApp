using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Users.Commands.AuthenticateUser;

/// <summary>
/// Validador para el comando <see cref="AuthenticateUserCommand"/>.
/// </summary>
internal class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="AuthenticateUserCommandValidator"/>.
    /// </summary>
    public AuthenticateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
                .WithErrorCode(Errors.Email.IsNullOrEmpty.Code)
                .WithMessage(Errors.Email.IsNullOrEmpty.Message)
            .NotEmpty()
                .WithErrorCode(Errors.Email.IsNullOrEmpty.Code)
                .WithMessage(Errors.Email.IsNullOrEmpty.Message)
            .MinimumLength(Email.MinLength)
                .WithErrorCode(Errors.Email.TooShort(Email.MinLength).Code)
                .WithMessage(Errors.Email.TooShort(Email.MinLength).Message)
            .MaximumLength(Email.MaxLength)
                .WithErrorCode(Errors.Email.TooLong(Email.MaxLength).Code)
                .WithMessage(Errors.Email.TooLong(Email.MaxLength).Message)
            .EmailAddress()
                .WithErrorCode(Errors.Email.InvalidFormat.Code)
                .WithMessage(Errors.Email.InvalidFormat.Message)
            .Matches(Email.EmailPattern)
                .WithErrorCode(Errors.Email.InvalidFormat.Code)
                .WithMessage(Errors.Email.InvalidFormat.Message);

        RuleFor(x => x.Password)
            .NotNull()
                //.WithErrorCode(Errors.Password.IsNullOrEmpty.Code)
                .WithMessage(Errors.Password.IsNullOrEmpty.Message)
            .NotEmpty()
                //.WithErrorCode(Errors.Password.IsNullOrEmpty.Code)
                .WithMessage(Errors.Password.IsNullOrEmpty.Message)
            .MinimumLength(Password.MinLength)
                //.WithErrorCode(Errors.Password.TooShort(Password.MinLength).Code)
                .WithMessage(Errors.Password.TooShort(Password.MinLength).Message)
            .MaximumLength(Password.MaxLength)
                //.WithErrorCode(Errors.Password.TooLong(Password.MaxLength).Code)
                .WithMessage(Errors.Password.TooLong(Password.MaxLength).Message)
            .Matches(Password.Pattern)
                //.WithErrorCode(Errors.Password.InvalidFormat.Code)
                .WithMessage(Errors.Password.InvalidFormat.Message);
    }
}
