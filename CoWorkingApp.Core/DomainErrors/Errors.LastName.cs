using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Errores relacionados con el apellido.
    /// </summary>
    public static class LastName
    {
        /// <summary>
        /// Indica que el apellido no puede estar vacío.
        /// </summary>
        public static readonly Error IsNullOrEmpty = Error.Create("LastName.IsNullOrEmpty", "The last name cannot be empty.");

        /// <summary>
        /// Indica que el formato del apellido es inválido.
        /// </summary>
        public static readonly Error InvalidFormat = Error.Create("LastName.InvalidFormat", "The last name format is invalid.");

        /// <summary>
        /// Indica que el apellido no puede exceder la longitud especificada.
        /// </summary>
        /// <param name="length">La longitud máxima permitida para el apellido.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el apellido es demasiado largo.</returns>
        public static readonly Func<int, Error> TooLong = length => Error.Create("LastName.TooLong", $"The last name cannot exceed {length} characters.");

        /// <summary>
        /// Indica que el apellido no puede estar por debajo de la longitud establecida.
        /// </summary>
        /// <param name="length">La longitud mínima permitida para el apellido.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el apellido es demasiado corto.</returns>
        public static readonly Func<int, Error> TooShort = length => Error.Create("LastName.TooShort", $"The last name must be at least {length} characters.");
    }
}
