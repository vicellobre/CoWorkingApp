using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Core.ValueObjects.Composite;

/// <summary>
/// Representa las credenciales del usuario, compuestas por correo electrónico y contraseña.
/// </summary>
public record struct CredentialsWithEmailAndPassword
{
    /// <summary>
    /// Obtiene o establece el correo electrónico del usuario.
    /// </summary>
    public Email Email { get; set; }

    /// <summary>
    /// Obtiene o establece la contraseña del usuario.
    /// </summary>
    public Password Password { get; set; }

    /// <summary>
    /// Constructor sin parámetros que lanza una excepción.
    /// Use el método estático <see cref="Create"/> para instanciar <see cref="CredentialsWithEmailAndPassword"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Lanza siempre indicando que use el método <see cref="Create"/>.</exception>
    public CredentialsWithEmailAndPassword()
    {
        throw new InvalidOperationException("Use the static Create method to instantiate CredentialsWithEmailAndPassword.");
    }

    /// <summary>
    /// Constructor privado para inicializar las credenciales.
    /// </summary>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <param name="password">La contraseña del usuario.</param>
    private CredentialsWithEmailAndPassword(Email email, Password password)
    {
        Email = email;
        Password = password;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="CredentialsWithEmailAndPassword"/> con los valores especificados.
    /// </summary>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <param name="password">La contraseña del usuario.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="CredentialsWithEmailAndPassword"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<CredentialsWithEmailAndPassword> Create(string email, string password)
    {
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
        {
            return Result<CredentialsWithEmailAndPassword>.Failure(emailResult.FirstError);
        }

        var passwordResult = Password.Create(password);
        if (passwordResult.IsFailure)
        {
            return Result<CredentialsWithEmailAndPassword>.Failure(passwordResult.FirstError);
        }

        return Result<CredentialsWithEmailAndPassword>.Success(new CredentialsWithEmailAndPassword(emailResult.Value, passwordResult.Value));
    }

    /// <summary>
    /// Devuelve una representación en cadena de las credenciales del usuario.
    /// </summary>
    /// <returns>Una cadena que representa las credenciales del usuario.</returns>
    public override readonly string ToString() => $"{Email.Value} / ******";
}
