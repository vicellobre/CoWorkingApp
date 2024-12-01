using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Errores relacionados con el primer nombre.
    /// </summary>
    public static class FirstName
    {
        /// <summary>
        /// Indica que el primer nombre no puede estar vacío.
        /// </summary>
        public static readonly Error IsNullOrEmpty = Error.Create("FirstName.IsNullOrEmpty", "The first name cannot be empty.");

        /// <summary>
        /// Indica que el formato del primer nombre es inválido.
        /// </summary>
        public static readonly Error InvalidFormat = Error.Create("FirstName.InvalidFormat", "First name format is invalid.");

        /// <summary>
        /// Indica que el primer nombre no puede exceder la longitud especificada.
        /// </summary>
        /// <param name="length">La longitud máxima permitida para el primer nombre.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el primer nombre es demasiado largo.</returns>
        public static readonly Func<int, Error> TooLong = length => Error.Create("FirstName.TooLong", $"First name cannot be longer than {length} characters.");

        /// <summary>
        /// Indica que el primer nombre no puede estar por debajo de la longitud establecida.
        /// </summary>
        /// <param name="length">La longitud mínima permitida para el primer nombre.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el primer nombre es demasiado corto.</returns>
        public static readonly Func<int, Error> TooShort = length => Error.Create("FirstName.TooShort", $"First name must be at least {length} characters.");
    }
}
