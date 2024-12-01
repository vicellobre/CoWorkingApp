using CoWorkingApp.Core.Primitives;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Core.Entities;

/// <summary>
/// Representa a un usuario en el sistema.
/// </summary>
public class User : EntityBase
{
    /// <summary>
    /// Obtiene o establece el nombre completo del usuario.
    /// </summary>
    public FullName FullName { get; set; }

    /// <summary>
    /// Obtiene o establece las credenciales del usuario (correo electrónico y contraseña).
    /// </summary>
    public CredentialsWithEmailAndPassword Credentials { get; set; }

    /// <summary>
    /// Obtiene o establece la lista de reservas asociadas al usuario.
    /// </summary>
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    /// <summary>
    /// Constructor para inicializar un usuario.
    /// </summary>
    protected User() : base() { }

    /// <summary>
    /// Constructor privado para inicializar un usuario.
    /// </summary>
    /// <param name="fullName">El nombre completo del usuario.</param>
    /// <param name="credentials">Las credenciales del usuario.</param>
    private User(FullName fullName, CredentialsWithEmailAndPassword credentials)
    {
        FullName = fullName;
        Credentials = credentials;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="User"/> con los valores especificados.
    /// </summary>
    /// <param name="firstName">El primer nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <param name="password">La contraseña del usuario.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="User"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<User> Create(string firstName, string lastName, string email, string password)
    {
        var fullNameResult = FullName.Create(firstName, lastName);
        if (!fullNameResult.IsSuccess)
        {
            return Result<User>.Failure(fullNameResult.FirstError);
        }

        var credentialsResult = CredentialsWithEmailAndPassword.Create(email, password);
        if (!credentialsResult.IsSuccess)
        {
            return Result<User>.Failure(credentialsResult.FirstError);
        }

        return Result<User>.Success(new User(fullNameResult.Value, credentialsResult.Value));
    }
}
