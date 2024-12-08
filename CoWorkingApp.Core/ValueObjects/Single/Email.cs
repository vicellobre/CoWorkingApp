using System.Text.RegularExpressions;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa el valor del correo electrónico del usuario.
/// </summary>
public readonly record struct Email
{
    /// <summary>
    /// Obtiene el correo electrónico del usuario.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Longitud mínima permitida para el correo electrónico.
    /// </summary>
    public const int MinLength = 5;

    /// <summary>
    /// Longitud máxima permitida para el correo electrónico.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Patrón de expresión regular para validar el formato del correo electrónico.
    /// </summary>
    private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="Email"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public Email()
    {
        //throw new InvalidOperationException("Use the static Create method to instantiate Email.");
    }

    /// <summary>
    /// Constructor privado para inicializar el valor del correo electrónico.
    /// </summary>
    /// <param name="value">El valor del correo electrónico.</param>
    private Email(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Email"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor del correo electrónico.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="Email"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<Email> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<Email>.Failure(Errors.Email.IsNullOrEmpty);
        }

        if (value.Length < MinLength)
        {
            return Result<Email>.Failure(Errors.Email.TooShort(MinLength));
        }

        if (value.Length > MaxLength)
        {
            return Result<Email>.Failure(Errors.Email.TooLong(MaxLength));
        }

        if (!Regex.IsMatch(value, EmailPattern))
        {
            return Result<Email>.Failure(Errors.Email.InvalidFormat);
        }

        return Result<Email>.Success(new Email(value));
    }

    /// <summary>
    /// Devuelve una representación en cadena del correo electrónico.
    /// </summary>
    /// <returns>El correo electrónico como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="Email"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="email">El valor de <see cref="Email"/> a convertir.</param>
    public static implicit operator string(Email email) => email.Value;
}
