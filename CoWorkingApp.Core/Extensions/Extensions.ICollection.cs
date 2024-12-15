namespace CoWorkingApp.Core.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Comprueba si la colección está vacía.
    /// </summary>
    /// <typeparam name="T">El tipo de elementos en la colección.</typeparam>
    /// <param name="collection">La colección a comprobar.</param>
    /// <returns>True si la colección está vacía; de lo contrario, false.</returns>
    public static bool IsEmpty<T>(this ICollection<T> collection) => collection.Count == 0;

    /// <summary>
    /// Comprueba si la colección es nula.
    /// </summary>
    /// <typeparam name="T">El tipo de elementos en la colección.</typeparam>
    /// <param name="collection">La colección a comprobar.</param>
    /// <returns>True si la colección es nula; de lo contrario, false.</returns>
    public static bool IsNull<T>(this ICollection<T> collection) => collection is null;

    /// <summary>
    /// Comprueba si la colección es nula o está vacía.
    /// </summary>
    /// <typeparam name="T">El tipo de elementos en la colección.</typeparam>
    /// <param name="collection">La colección a comprobar.</param>
    /// <returns>True si la colección es nula o está vacía; de lo contrario, false.</returns>
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection.IsNull() || collection.IsEmpty();
}
