using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Seats.Commands.UpdateSeat;

/// <summary>
/// Comando para actualizar un asiento.
/// </summary>
public record class UpdateSeatCommand : ICommand<UpdateSeatCommandResponse>, IInputFilter
{
    /// <summary>
    /// El identificador del asiento.
    /// </summary>
    public Guid SeatId { get; private set; }

    /// <summary>
    /// El nombre del asiento.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// La descripción del asiento.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="UpdateSeatCommand"/>.
    /// </summary>
    /// <param name="seatId">El identificador del asiento.</param>
    /// <param name="name">El nombre del asiento.</param>
    /// <param name="description">La descripción del asiento.</param>
    public UpdateSeatCommand(Guid seatId, string name, string description)
    {
        SeatId = seatId;
        Name = name;
        Description = description;
    }

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
