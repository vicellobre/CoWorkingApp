using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUser;

/// <summary>
/// Comando para actualizar un usuario.
/// </summary>
public record class UpdateUserCommand : ICommand<UpdateUserCommandResponse>, IInputFilter
{
    /// <summary>
    /// El identificador del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

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
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserCommand"/>.
    /// </summary>
    /// <param name="userId">El identificador del usuario.</param>
    /// <param name="firstName">El nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <param name="password">La contraseña del usuario.</param>
    public UpdateUserCommand(Guid userId, string firstName, string lastName, string email, string password)
    {
        UserId = userId;
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

        Password = Password
            .GetValueOrDefault(string.Empty);
    }
}
