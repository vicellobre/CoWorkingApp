using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Errores relacionados con el usuario.
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Error que indica que el correo electrónico especificado ya está en uso.
        /// </summary>
        public static readonly Error EmailAlreadyInUse = Error.Create("User.EmailAlreadyInUse", "El correo electrónico especificado ya está en uso");

        /// <summary>
        /// Error que indica que el usuario con el identificador especificado no fue encontrado.
        /// </summary>
        public static readonly Func<Guid, Error> NotFound = id => Error.Create("User.NotFound", $"El usuario con el identificador {id} no fue encontrado.");

        /// <summary>
        /// Error que indica que el usuario con el correo electrónico especificado no existe.
        /// </summary>
        public static readonly Func<string, Error> EmailNotExist = email => Error.Create("User.EmailNotExist", $"El correo electrónico {email} no existe.");

        /// <summary>
        /// Error que indica que las credenciales especificadas no son válidas.
        /// </summary>
        public static readonly Error InvalidCredentials = Error.Create("User.InvalidCredentials", "Las credenciales no son válidas.");
    }
}
