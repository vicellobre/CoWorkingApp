using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Seats.Commands.CreateSeat;

/// <summary>
/// Comando para crear un nuevo asiento.
/// </summary>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record struct CreateSeatCommand(
    string Name,
    string Description) : ICommand<CreateSeatCommandResponse>, IInputFilter
{
    /// <summary>
    /// Filtra y normaliza los campos del asiento.
    /// </summary>
    public void Filter()
    {
        Name = Name
            .GetValueOrDefault(string.Empty)
            .Trim();

        Description = Description
            .GetValueOrDefault(string.Empty)
            .Trim();
    }
}
