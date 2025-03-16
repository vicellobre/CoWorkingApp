using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Commands.DeleteUser;

/// <summary>
/// Comando para eliminar un usuario.
/// </summary>
public record class DeleteUserCommand : ICommand<DeleteUserCommandResponse>
{
    /// <summary>
    /// El identificador del usuario.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="DeleteUserCommand"/>.
    /// </summary>
    /// <param name="userId">El identificador del usuario.</param>
    public DeleteUserCommand(Guid userId)
    {
        UserId = userId;
    }
}
