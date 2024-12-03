using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.DomainErrors;

/// <summary>
/// Define errores predefinidos comunes utilizados en toda la aplicación.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// 
    /// </summary>
    public static class SeatName
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Error InvalidFormat = Error.Create("InvalidFormat", "The value must be in the format 'Row-Number'.");
    }
}
