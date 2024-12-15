using CoWorkingApp.Core.Primitives;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Core.Entities;

/// <summary>
/// Representa un asiento en el sistema.
/// </summary>
public class Seat : EntityBase
{
    /// <summary>
    /// Obtiene o establece el nombre del asiento.
    /// </summary>
    public SeatName Name { get; set; }

    /// <summary>
    /// Obtiene o establece la descripción del asiento.
    /// </summary>
    public Description Description { get; set; }

    /// <summary>
    /// Obtiene o establece la lista de reservas asociadas al asiento.
    /// </summary>
    public List<Reservation> Reservations { get; set; } = [];

    /// <summary>
    /// Constructor privado para inicializar un asiento con un identificador especificado.
    /// </summary>
    /// <param name="id">El identificador del asiento.</param>
    /// <param name="name">El nombre del asiento.</param>
    /// <param name="description">La descripción del asiento.</param>
    private Seat(Guid id, SeatName name, Description description) : base(id)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Constructor público para inicializar un asiento con identificadores y valores básicos.
    /// </summary>
    /// <param name="id">El identificador del asiento.</param>
    /// <param name="number">El número del asiento.</param>
    /// <param name="row">La fila del asiento.</param>
    /// <param name="description">La descripción del asiento.</param>
    public Seat(Guid id, string number, string row, string description) : base(id)
    {
        Name = SeatName.Create(number, row).Value;
        Description = Description.Create(description).Value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Seat"/> con los valores especificados.
    /// </summary>
    /// <param name="id">El identificador del asiento.</param>
    /// <param name="number">El número del asiento.</param>
    /// <param name="row">La fila del asiento.</param>
    /// <param name="description">La descripción del asiento.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="Seat"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<Seat> Create(Guid id, string number, string row, string description)
    {
        var seatNameResult = SeatName.Create(number, row);
        if (seatNameResult.IsFailure)
        {
            return Result<Seat>.Failure(seatNameResult.Errors);
        }

        var descriptionResult = Description.Create(description);

        return Result<Seat>.Success(new Seat(id, seatNameResult.Value, descriptionResult.Value));
    }

    /// <summary>
    /// Cambia el nombre del asiento.
    /// </summary>
    /// <param name="number">El nuevo número del asiento.</param>
    /// <param name="row">La nueva fila del asiento.</param>
    /// <returns>Un resultado que indica si la operación fue exitosa.</returns>
    public Result ChangeName(string number, string row)
    {
        var seatNameResult = SeatName.Create(number, row);
        if (seatNameResult.IsFailure)
        {
            return Result.Failure(seatNameResult.FirstError);
        }

        Name = seatNameResult.Value;
        return Result.Success();
    }

    /// <summary>
    /// Cambia la descripción del asiento.
    /// </summary>
    /// <param name="description">La nueva descripción del asiento.</param>
    public void ChangeDescription(string description)
    {
        Description = Description.Create(description).Value;
    }

    /// <summary>
    /// Agrega una reserva a la lista de reservas del asiento.
    /// </summary>
    /// <param name="reservation">La reserva a agregar.</param>
    public void AddReservation(Reservation reservation)
    {
        Reservations.Add(reservation);
    }

    /// <summary>
    /// Elimina una reserva de la lista de reservas del asiento.
    /// </summary>
    /// <param name="reservation">La reserva a eliminar.</param>
    public void RemoveReservation(Reservation reservation)
    {
        Reservations.Remove(reservation);
    }
}
