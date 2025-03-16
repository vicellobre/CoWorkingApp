using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.Errors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class ERRORS
{
    /// <summary>
    /// Contiene los errores relacionados con las reservas.
    /// </summary>
    public static class Reservation
    {
        /// <summary>
        /// Error que indica que una reserva no fue encontrada.
        /// </summary>
        public static readonly Func<Guid, Error> NotFound = id => Error.NotFound("Reservation.NotFound", $"The reservation with the identifier {id} was not found.");
    }
}
