using System.Text.RegularExpressions;

namespace CoWorkingApp.Core.Extensions;

/// <summary>
/// Contiene métodos de extensión para la clase <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Divide una cadena utilizando cualquier tipo de espacio en blanco (espacios, tabulaciones, saltos de línea) como delimitador.
    /// </summary>
    /// <param name="input">La cadena de entrada a dividir.</param>
    /// <returns>Un array de subcadenas.</returns>
    public static string[] SplitByWhitespace(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return [];
        }

        string pattern = @"\s+";
        var regex = new Regex(pattern);
        return regex.Split(input);
    }

    /// <summary>
    /// Pone la primera letra de la cadena en mayúscula y el resto en minúsculas.
    /// </summary>
    /// <param name="input">La cadena de entrada.</param>
    /// <returns>La cadena con la primera letra en mayúscula y el resto en minúsculas.</returns>
    public static string Capitalize(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        if (input.Length == 1)
        {
            return input.ToUpper();
        }

        return char.ToUpper(input[0]) + input[1..].ToLower();
    }

    /// <summary>
    /// Reemplaza cualquier tipo de espacio en blanco (espacios, tabulaciones, saltos de línea) en una cadena con el valor especificado.
    /// </summary>
    /// <param name="input">La cadena de entrada.</param>
    /// <param name="replacement">El valor con el que se reemplazarán los espacios en blanco.</param>
    /// <returns>La cadena modificada.</returns>
    public static string ReplaceWhitespace(this string input, string replacement)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        string pattern = @"\s+";
        var regex = new Regex(pattern);
        return regex.Replace(input, replacement);
    }

    /// <summary>
    /// Pone la primera letra de cada palabra en mayúscula y el resto en minúsculas.
    /// </summary>
    /// <param name="input">La cadena de entrada.</param>
    /// <returns>La cadena con cada palabra capitalizada.</returns>
    public static string CapitalizeWords(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        var words = input.SplitByWhitespace();
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i].Capitalize();
        }

        return string.Join(" ", words);
    }

    /// <summary>
    /// Devuelve un valor predeterminado si la cadena de entrada es null o está vacía; de lo contrario, devuelve la cadena de entrada.
    /// </summary>
    /// <param name="input">La cadena de entrada.</param>
    /// <param name="defaultValue">El valor predeterminado que se devolverá si la cadena de entrada es null o está vacía.</param>
    /// <returns>La cadena de entrada o el valor predeterminado si la cadena de entrada es null o está vacía.</returns>
    public static string GetValueOrDefault(this string input, string defaultValue) => string.IsNullOrEmpty(input) ? defaultValue : input;
}
