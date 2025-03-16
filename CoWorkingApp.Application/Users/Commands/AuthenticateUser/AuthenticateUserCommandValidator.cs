using CoWorkingApp.Core.Errors;
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
                .WithErrorCode(ERRORS.Email.IsNullOrEmpty.Code)
                .WithMessage(ERRORS.Email.IsNullOrEmpty.Message)
            .NotEmpty()
                .WithErrorCode(ERRORS.Email.IsNullOrEmpty.Code)
                .WithMessage(ERRORS.Email.IsNullOrEmpty.Message)
            .MinimumLength(Email.MinLength)
                .WithErrorCode(ERRORS.Email.TooShort(Email.MinLength).Code)
                .WithMessage(ERRORS.Email.TooShort(Email.MinLength).Message)
            .MaximumLength(Email.MaxLength)
                .WithErrorCode(ERRORS.Email.TooLong(Email.MaxLength).Code)
                .WithMessage(ERRORS.Email.TooLong(Email.MaxLength).Message)
            .EmailAddress()
                .WithErrorCode(ERRORS.Email.InvalidFormat.Code)
                .WithMessage(ERRORS.Email.InvalidFormat.Message)
            .Matches(Email.EmailPattern)
                .WithErrorCode(ERRORS.Email.InvalidFormat.Code)
                .WithMessage(ERRORS.Email.InvalidFormat.Message);

        RuleFor(x => x.Password)
            .NotNull()
                //.WithErrorCode(Errors.Password.IsNullOrEmpty.Code)
                .WithMessage(ERRORS.Password.IsNullOrEmpty.Message)
            .NotEmpty()
                //.WithErrorCode(Errors.Password.IsNullOrEmpty.Code)
                .WithMessage(ERRORS.Password.IsNullOrEmpty.Message)
            .MinimumLength(Password.MinLength)
                //.WithErrorCode(Errors.Password.TooShort(Password.MinLength).Code)
                .WithMessage(ERRORS.Password.TooShort(Password.MinLength).Message)
            .MaximumLength(Password.MaxLength)
                //.WithErrorCode(Errors.Password.TooLong(Password.MaxLength).Code)
                .WithMessage(ERRORS.Password.TooLong(Password.MaxLength).Message)
            .Matches(Password.Pattern)
                //.WithErrorCode(Errors.Password.InvalidFormat.Code)
                .WithMessage(ERRORS.Password.InvalidFormat.Message);
    }
}
