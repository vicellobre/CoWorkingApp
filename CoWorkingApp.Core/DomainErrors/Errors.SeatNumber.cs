using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    public static class SeatNumber
    {
        /// <summary>
        /// El número de asiento no puede ser nulo o vacío.
        /// </summary>
        public static readonly Error IsNullOrEmpty = Error.Create("SeatNumber.IsNullOrEmpty", "Seat number cannot be null or empty.");
    }
}
