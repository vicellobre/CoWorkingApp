using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Contiene los errores relacionados con Date.
/// </summary>
public static partial class Errors
{
    public static class Date
    {
        /// <summary>
        /// La fecha es inválida.
        /// </summary>
        public static readonly Error Invalid = Error.Validation("Date.Invalid", "The date is invalid.");
    }
}
