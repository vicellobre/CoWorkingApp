using CoWorkingApp.Core.Enumerations;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Core.Primitives;
using CoWorkingApp.Core.Shared;
using FluentValidation;
using MediatR;

namespace CoWorkingApp.Application.Behaviors;

/// <summary>
/// Comportamiento de pipeline de validación para solicitudes de MediatR.
/// </summary>
/// <typeparam name="TRequest">El tipo de la solicitud.</typeparam>
/// <typeparam name="TResponse">El tipo de la respuesta.</typeparam>
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
    where TResponse : IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ValidationPipelineBehavior{TRequest, TResponse}"/>.
    /// </summary>
    /// <param name="validators">La colección de validadores para la solicitud.</param>
    /// <exception cref="ArgumentNullException">Lanzada cuando la colección de validadores es null.</exception>
    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    /// <summary>
    /// Maneja el comportamiento de validación para la solicitud.
    /// </summary>
    /// <param name="request">La solicitud.</param>
    /// <param name="next">El siguiente delegado en el pipeline.</param>
    /// <param name="cancellationToken">El token de cancelación.</param>
    /// <returns>La respuesta de la solicitud.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationResults = _validators
            .Select(validator => validator.Validate(request))
            .ToList();

        Error[] errors = validationResults
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
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    /// <summary>
    /// Crea un resultado de validación con los errores especificados.
    /// </summary>
    /// <typeparam name="TResult">El tipo del resultado de la solicitud.</typeparam>
    /// <param name="errors">La colección de errores de validación.</param>
    /// <returns>Un resultado de validación con los errores especificados.</returns>
    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : IResult
    {
        if (typeof(TResult) == typeof(IResult))
        {
            return (TResult)(IResult)Result.Failure(errors);
        }

        object result = typeof(Result<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(Result.Failure), new[] { typeof(ICollection<Error>) })!
            .Invoke(null, new object[] { errors })!;

        return (TResult)result;
    }
}
