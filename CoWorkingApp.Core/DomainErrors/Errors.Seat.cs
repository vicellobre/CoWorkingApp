using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Contiene errores relacionados con la entidad <see cref="Seat"/>.
    /// </summary>
    public static class Seat
    {
        /// <summary>
        /// Error que indica que el asiento no puede ser nulo.
        /// </summary>
        public static readonly Error IsNull = Error.Create("Seat.IsNull", "The seat cannot be null.");

        /// <summary>
        /// Error que indica que el nombre del asiento ya está en uso.
        /// </summary>
        public static readonly Error NameAlreadyInUse = Error.Create("Seat.NameAlreadyInUse", "The seat name is already in use.");

        /// <summary>
        /// Error que indica que el asiento con el identificador especificado no fue encontrado.
        /// </summary>
        public static readonly Func<Guid, Error> NotFound = id => Error.Create("Seat.NotFound", $"The seat with the identifier {id} was not found.");

        /// <summary>
        /// Error que indica que el nombre del asiento no existe.
        /// </summary>
        public static readonly Func<string, Error> NameNotExist = name => Error.Create("Seat.NameNotExist", $"The seat with the name {name} does not exist.");

        /// <summary>
        /// Error que indica que el asiento no está disponible para la fecha especificada.
        /// </summary>
        public static readonly Func<Guid, DateTime, Error> NotAvailable = (id, date) => Error.Create("Seat.NotAvailable", $"The seat with the identifier {id} is not available for the date {date}.");
    }
}
