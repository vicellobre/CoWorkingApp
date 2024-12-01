using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Core.ValueObjects.Composite;

/// <summary>
/// Representa el nombre completo del usuario, compuesto por nombre y apellido.
/// </summary>
public record struct FullName
{
    /// <summary>
    /// Obtiene o establece el nombre del usuario.
    /// </summary>
    public FirstName FirstName { get; set; }

    /// <summary>
    /// Obtiene o establece el apellido del usuario.
    /// </summary>
    public LastName LastName { get; set; }

    /// <summary>
    /// Obtiene el nombre completo del usuario en formato "Nombre Apellido".
    /// </summary>
    public readonly string Value => $"{FirstName.Value} {LastName.Value}";

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="FullName"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public FullName()
    {
        throw new InvalidOperationException("Use the static Create method to instantiate FullName.");
    }

    /// <summary>
    /// Constructor privado para inicializar el valor del nombre completo.
    /// </summary>
    /// <param name="firstName">El nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    private FullName(FirstName firstName, LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="FullName"/> con los valores especificados.
    /// </summary>
    /// <param name="firstName">El nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="FullName"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<FullName> Create(string firstName, string lastName)
    {
        var firstNameResult = FirstName.Create(firstName);
        if (!firstNameResult.IsSuccess)
        {
            return Result<FullName>.Failure(firstNameResult.FirstError);
        }

        var lastNameResult = LastName.Create(lastName);
        if (!lastNameResult.IsSuccess)
        {
            return Result<FullName>.Failure(lastNameResult.FirstError);
        }

        return Result<FullName>.Success(new FullName(firstNameResult.Value, lastNameResult.Value));
    }

    /// <summary>
    /// Devuelve una representación en cadena del nombre completo.
    /// </summary>
    /// <returns>El nombre completo en formato "Nombre Apellido".</returns>
    public override readonly string ToString() => Value;
}
