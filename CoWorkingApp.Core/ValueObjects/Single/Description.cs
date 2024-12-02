namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa la descripción del asiento.
/// </summary>
public readonly record struct Description
{
    /// <summary>
    /// Obtiene la descripción del asiento.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Constructor sin parámetros que asigna una cadena vacía.
    /// </summary>
    public Description()
    {
        throw new InvalidOperationException("Use the static Create method to instantiate Description.");
    }

    /// <summary>
    /// Constructor privado para inicializar el valor de la descripción.
    /// </summary>
    /// <param name="value">El valor de la descripción.</param>
    private Description(string value)
    {
        Value = value ?? string.Empty;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Description"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor de la descripción.</param>
    /// <returns>Una instancia de <see cref="Description"/>.</returns>
    public static Description Create(string value) => new(value);

    /// <summary>
    /// Devuelve una representación en cadena de la descripción.
    /// </summary>
    /// <returns>La descripción como una cadena.</returns>
    public override string ToString() => Value;
}
