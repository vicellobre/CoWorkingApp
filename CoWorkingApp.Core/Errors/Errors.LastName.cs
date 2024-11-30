using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.Errors;

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
        /// Indica que el apellido solo puede contener caracteres alfabéticos.
        /// </summary>
        public static readonly Error InvalidSpecialCharacters = Error.Create("LastName.InvalidSpecialCharacters", "The last name can only contain alphabetic characters.");

        /// <summary>
        /// Indica que el apellido no puede exceder la longitud especificada.
        /// </summary>
        /// <param name="length">La longitud máxima permitida para el apellido.</param>
        /// <returns>Una nueva instancia de <see cref="Error"/> con un mensaje que indica que el apellido es demasiado largo.</returns>
        public static readonly Func<int, Error> TooLong = length => Error.Create("LastName.TooLong", $"The last name cannot exceed {length} characters.");
    }
}
