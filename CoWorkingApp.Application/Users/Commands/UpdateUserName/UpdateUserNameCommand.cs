using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Commands.UpdateUserName;

/// <summary>
/// Representa un comando para actualizar el nombre de un usuario.
/// </summary>
public record class UpdateUserNameCommand : ICommand, IInputFilter
{
    /// <summary>
    /// El ID del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// El primer nombre del usuario.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// El apellido del usuario.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateUserNameCommand"/>.
    /// </summary>
    /// <param name="userId">El ID del usuario.</param>
    /// <param name="firstName">El primer nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    public UpdateUserNameCommand(Guid userId, string firstName, string lastName)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Filtra y normaliza los campos del nombre del usuario.
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
    }
}
