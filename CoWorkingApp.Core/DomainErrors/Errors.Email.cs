using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Errores relacionados con el email.
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// Indica que el email no puede estar vacío.
        /// </summary>
        public static readonly Error IsNullOrEmpty = Error.Create("Email.IsNullOrEmpty", "The email cannot be empty.");

        /// <summary>
        /// Indica que el email debe de seguir el formato establecido.
        /// </summary>
        public static readonly Error InvalidFormat = Error.Create("Email.InvalidFormat", "The email does not meet the formatting guidelines.");

        /// <summary>
        /// Indica que el email no puede exceder la longitud especificada.
        /// </summary>
        /// <param name="length">La longitud máxima permitida para el email.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el email es demasiado largo.</returns>
        public static readonly Func<int, Error> TooLong = length => Error.Create("Email.TooLong", $"The email cannot exceed {length} characters.");

        /// <summary>
        /// Indica que el email no puede estar por debajo de la longitud establecida.
        /// </summary>
        /// <param name="length">La longitud mínima permitida para el email.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el email es demasiado corto.</returns>
        public static readonly Func<int, Error> TooShort = length => Error.Create("Email.TooShort", $"The email must be at least {length} characters.");
    }
}
