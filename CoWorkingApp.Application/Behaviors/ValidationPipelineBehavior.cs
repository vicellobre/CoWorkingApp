using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Core.Shared;
using FluentValidation;
using MediatR;

namespace CoWorkingApp.Application.Behaviors;

/// <summary>
/// Comportamiento de pipeline de validación para solicitudes de MediatR.
/// </summary>
/// <typeparam name="TRequest">El tipo de la solicitud.</typeparam>
/// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : MediatR.IRequest<Result<TResponse>>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ValidationPipelineBehavior{TRequest, TResponse}"/>.
    /// </summary>
    /// <param name="validators">La colección de validadores para la solicitud.</param>
    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    /// <summary>
    /// Maneja el comportamiento de validación para la solicitud.
    /// </summary>
    /// <param name="request">La solicitud.</param>
    /// <param name="next">El siguiente delegado en el pipeline.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>La respuesta de la solicitud.</returns>
    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => Error.Create(
                failure.PropertyName,
                failure.ErrorMessage,
                ErrorType.Validation))
            .Distinct()
            .ToArray();

        if (!errors.IsEmpty())
        {
            return Result<TResponse>.Failure(errors);
        }

        return await next();
    }
}
