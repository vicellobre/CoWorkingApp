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
        /// Error que indica que el nombre del asiento ya está en uso.
        /// </summary>
        public static readonly Error NameAlreadyInUse = Error.Create("Seat.NameAlreadyInUse", "El nombre del asiento ya está en uso.");

        /// <summary>
        /// Error que indica que el asiento con el identificador especificado no fue encontrado.
        /// </summary>
        public static readonly Func<Guid, Error> NotFound = id => Error.Create("Seat.NotFound", $"El asiento con el identificador {id} no fue encontrado.");

        /// <summary>
        /// Error que indica que el nombre del asiento no existe.
        /// </summary>
        public static readonly Func<string, Error> NameNotExist = name => Error.Create("Seat.NameNotExist", $"El asiento con el nombre {name} no existe.");

        /// <summary>
        /// Error que indica que el asiento no está disponible para la fecha especificada.
        /// </summary>
        public static readonly Func<Guid, DateTime, Error> NotAvailable = (id, date) => Error.Create("Seat.NotAvailable", $"El asiento con el identificador {id} no está disponible para la fecha {date}.");
    }
}
