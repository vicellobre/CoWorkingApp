namespace CoWorkingApp.Core.Extensions;

/// <summary>
/// Métodos de extensión para enumeraciones.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Convierte un valor de enumeración a su valor entero.
    /// </summary>
    /// <param name="enumValue">El valor de enumeración a convertir.</param>
    /// <returns>El valor entero correspondiente al valor de enumeración.</returns>
    public static int ToInt(this Enum enumValue)
    {
        return Convert.ToInt32(enumValue);
    }
}
