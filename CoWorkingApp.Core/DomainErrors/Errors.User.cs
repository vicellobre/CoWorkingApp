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
        /// Error que indica que el usuario no puede ser nulo.
        /// </summary>
        public static readonly Error IsNull = Error.Create("User.IsNull", "The user cannot be null.");

        /// <summary>
        /// Error que indica que el correo electrónico especificado ya está en uso.
        /// </summary>
        public static readonly Error EmailAlreadyInUse = Error.Create("User.EmailAlreadyInUse", "The specified email is already in use.");

        /// <summary>
        /// Error que indica que el usuario con el identificador especificado no fue encontrado.
        /// </summary>
        public static readonly Func<Guid, Error> NotFound = id => Error.Create("User.NotFound", $"The user with the identifier {id} was not found.");

        /// <summary>
        /// Error que indica que el usuario con el correo electrónico especificado no existe.
        /// </summary>
        public static readonly Func<string, Error> EmailNotExist = email => Error.Create("User.EmailNotExist", $"The email {email} does not exist.");

        /// <summary>
        /// Error que indica que las credenciales especificadas no son válidas.
        /// </summary>
        public static readonly Error InvalidCredentials = Error.Create("User.InvalidCredentials", "The specified credentials are invalid.");
    }
}
