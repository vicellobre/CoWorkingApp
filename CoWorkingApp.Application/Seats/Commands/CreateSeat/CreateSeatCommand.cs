using CoWorkingApp.Application.Abstracts.Messaging;

namespace CoWorkingApp.Application.Seats.Commands.CreateSeat;

/// <summary>
/// Comando para crear un nuevo asiento.
/// </summary>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public readonly record struct CreateSeatCommand(
    string Name,
    string Description) : ICommand<CreateSeatCommandResponse>;
