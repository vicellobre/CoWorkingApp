using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Seats.Commands.CreateSeat;

/// <summary>
/// Comando para crear un nuevo asiento.
/// </summary>
public record class CreateSeatCommand : ICommand<CreateSeatCommandResponse>, IInputFilter
{
    /// <summary>
    /// El nombre del asiento.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// La descripción del asiento.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CreateSeatCommand"/>.
    /// </summary>
    /// <param name="name">El nombre del asiento.</param>
    /// <param name="description">La descripción del asiento.</param>
    public CreateSeatCommand(string name, string description)
    {
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
