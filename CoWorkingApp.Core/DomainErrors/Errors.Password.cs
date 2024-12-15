using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Errores relacionados con el password.
    /// </summary>
    public static class Password
    {
        /// <summary>
        /// Indica que el password no puede estar vacío.
        /// </summary>
        public static readonly Error IsNullOrEmpty = Error.Validation("Password.IsNullOrEmpty", "The password cannot be empty.");

        /// <summary>
        /// Indica que el password debe de seguir el formato establecido.
        /// </summary>
        public static readonly Error InvalidFormat = Error.Validation("Password.InvalidFormat", "The password does not meet the formatting guidelines.");

        /// <summary>
        /// Indica que el password no puede exceder la longitud especificada.
        /// </summary>
        /// <param name="length">La longitud máxima permitida para el password.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el password es demasiado largo.</returns>
        public static readonly Func<int, Error> TooLong = length => Error.Validation("Password.TooLong", $"The password cannot exceed {length} characters.");

        /// <summary>
        /// Indica que el password no puede estar por debajo de la longitud establecida.
        /// </summary>
        /// <param name="length">La longitud mínima permitida para el password.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el password es demasiado corto.</returns>
        public static readonly Func<int, Error> TooShort = length => Error.Validation("Password.TooShort", $"The password must be at least {length} characters.");
    }
}
