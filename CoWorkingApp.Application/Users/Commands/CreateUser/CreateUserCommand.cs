using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.CreateUser;

/// <summary>
/// Comando para crear un nuevo usuario.
/// </summary>
public record class CreateUserCommand : 
    ICommand<CreateUserCommandResponse>, IInputFilter
{
    /// <summary>
    /// El nombre del usuario.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// El apellido del usuario.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// El correo electrónico del usuario.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// La contraseña del usuario.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateUserCommand"/>.
    /// </summary>
    /// <param name="firstName">El nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <param name="password">La contraseña del usuario.</param>
    public CreateUserCommand(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    /// <summary>
    /// Filtra y normaliza los campos del usuario.
    /// </summary>
    public void Filter()
    {
        FirstName = FirstName
            .GetValueOrDefault(string.Empty)
            .Trim()
            .CapitalizeWords();

        LastName = LastName
            .GetValueOrDefault(string.Empty)
            .Trim()
            .CapitalizeWords();

        Email = Email
            .GetValueOrDefault(string.Empty)
            .Trim()
            .ToLowerInvariant();
    }
}
