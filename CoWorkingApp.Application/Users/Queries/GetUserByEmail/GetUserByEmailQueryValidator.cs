using CoWorkingApp.Core.ValueObjects.Single;
using FluentValidation;

namespace CoWorkingApp.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// Validador para la consulta <see cref="GetUserByEmailQuery"/>.
/// </summary>
internal class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetUserByEmailQueryValidator"/>.
    /// </summary>
    public GetUserByEmailQueryValidator()
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
