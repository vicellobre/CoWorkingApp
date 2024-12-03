using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Users.Commands.DeleteUser;

/// <summary>
/// Comando para eliminar un usuario.
/// </summary>
/// <param name="UserId">El identificador del usuario.</param>
public readonly record struct DeleteUserCommand(Guid UserId) : ICommand<DeleteUserCommandResponse>;
