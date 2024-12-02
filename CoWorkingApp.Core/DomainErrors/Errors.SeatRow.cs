using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    public static class SeatRow
    {
        /// <summary>
        /// La fila del asiento no puede ser nula o vacía.
        /// </summary>
        public static readonly Error IsNullOrEmpty = Error.Create("SeatRow.IsNullOrEmpty", "Seat row cannot be null or empty.");
    }
}
