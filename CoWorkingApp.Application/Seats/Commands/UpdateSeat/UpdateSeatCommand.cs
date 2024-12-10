using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Seats.Commands.UpdateSeat;

/// <summary>
/// Comando para actualizar un asiento.
/// </summary>
/// <param name="SeatId">El identificador del asiento.</param>
/// <param name="Name">El nombre del asiento.</param>
/// <param name="Description">La descripción del asiento.</param>
public record struct UpdateSeatCommand(
    Guid SeatId,
    string Name,
    string Description) : ICommand<UpdateSeatCommandResponse>, IInputFilter
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
