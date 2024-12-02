using CoWorkingApp.Core.Primitives;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Core.Entities;

/// <summary>
/// Representa una reserva en el sistema.
/// </summary>
public class Reservation : EntityBase
{
    /// <summary>
    /// Obtiene o establece la fecha de la reserva.
    /// </summary>
    public Date Date { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador único del usuario asociado a la reserva.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Obtiene o establece el usuario asociado a la reserva.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador único del asiento asociado a la reserva.
    /// </summary>
    public Guid SeatId { get; set; }

    /// <summary>
    /// Obtiene o establece el asiento asociado a la reserva.
    /// </summary>
    public Seat? Seat { get; set; }

    /// <summary>
    /// Constructor privado para inicializar una reserva con un identificador especificado.
    /// </summary>
    /// <param name="id">El identificador de la reserva.</param>
    /// <param name="date">La fecha de la reserva.</param>
    /// <param name="userId">El identificador del usuario asociado a la reserva.</param>
    /// <param name="seatId">El identificador del asiento asociado a la reserva.</param>
    private Reservation(Guid id, Date date, Guid userId, Guid seatId) : base(id)
    {
        Date = date;
        UserId = userId;
        SeatId = seatId;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Reservation"/> con los valores especificados.
    /// </summary>
    /// <param name="id">El identificador de la reserva.</param>
    /// <param name="date">La fecha de la reserva.</param>
    /// <param name="userId">El identificador del usuario asociado a la reserva.</param>
    /// <param name="seatId">El identificador del asiento asociado a la reserva.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="Reservation"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<Reservation> Create(Guid id, DateTime date, Guid userId, Guid seatId)
    {
        var dateResult = Date.Create(date);
        if (dateResult.IsFailure)
        {
            return Result<Reservation>.Failure(dateResult.FirstError);
        }

        return Result<Reservation>.Success(new Reservation(id, dateResult.Value, userId, seatId));
    }

    /// <summary>
    /// Cambia la fecha de la reserva.
    /// </summary>
    /// <param name="newDate">La nueva fecha de la reserva.</param>
    /// <returns>Un resultado que indica si la operación fue exitosa.</returns>
    public Result ChangeDate(DateTime newDate)
    {
        var dateResult = Date.Create(newDate);
        if (dateResult.IsFailure)
        {
            return Result.Failure(dateResult.FirstError);
        }

        Date = dateResult.Value;
        return Result.Success();
    }

    /// <summary>
    /// Cambia el usuario asociado a la reserva.
    /// </summary>
    /// <param name="userId">El nuevo identificador del usuario.</param>
    /// <param name="user">El nuevo usuario asociado a la reserva.</param>
    public void ChangeUser(Guid userId, User user)
    {
        UserId = userId;
        User = user;
    }

    /// <summary>
    /// Cambia el asiento asociado a la reserva.
    /// </summary>
    /// <param name="seatId">El nuevo identificador del asiento.</param>
    /// <param name="seat">El nuevo asiento asociado a la reserva.</param>
    public void ChangeSeat(Guid seatId, Seat seat)
    {
        SeatId = seatId;
        Seat = seat;
    }
}
