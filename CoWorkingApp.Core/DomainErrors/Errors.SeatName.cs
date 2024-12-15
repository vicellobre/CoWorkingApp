using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Errores relacionados con los nombres de asientos.
    /// </summary>
    public static class SeatName
    {
        /// <summary>
        /// Error para formato inválido.
        /// </summary>
        public static readonly Error InvalidFormat = Error.Validation("InvalidFormat", "The value must be in the format 'Row-Number'.");

        /// <summary>
        /// Error para valor nulo o vacío.
        /// </summary>
        public static readonly Error IsNullOrEmpty = Error.Validation("IsNullOrEmpty", "The value cannot be null or empty.");
    }
}