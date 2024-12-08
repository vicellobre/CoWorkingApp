using CoWorkingApp.Core.DomainErrors;
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
    public User User { get; set; } = default!;

    /// <summary>
    /// Obtiene o establece el identificador único del asiento asociado a la reserva.
    /// </summary>
    public Guid SeatId { get; set; }

    /// <summary>
    /// Obtiene o establece el asiento asociado a la reserva.
    /// </summary>
    public Seat Seat { get; set; } = default!;

    /// <summary>
    /// Constructor sin parámetros requerido por Entity Framework Core.
    /// </summary>
    private Reservation() : base() { }


    /// <summary>
    /// Constructor privado para inicializar una reserva con un identificador especificado.
    /// </summary>
    /// <param name="id">El identificador de la reserva.</param>
    /// <param name="date">La fecha de la reserva.</param>
    /// <param name="user">El usuario asociado a la reserva.</param>
    /// <param name="seat">El asiento asociado a la reserva.</param>
    private Reservation(Guid id, Date date, User user, Seat seat) : base(id)
    {
        Date = date;
        UserId = user.Id;
        User = user;
        SeatId = seat.Id;
        Seat = seat;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="Reservation"/> con los valores especificados.
    /// </summary>
    /// <param name="id">El identificador de la reserva.</param>
    /// <param name="date">La fecha de la reserva.</param>
    /// <param name="user">El usuario asociado a la reserva.</param>
    /// <param name="seat">El asiento asociado a la reserva.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="Reservation"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<Reservation> Create(Guid id, DateTime date, User user, Seat seat)
    {
        var dateResult = Date.Create(date);
        if (dateResult.IsFailure)
        {
            return Result<Reservation>.Failure(dateResult.FirstError);
        }

        return Result<Reservation>.Success(new Reservation(id, dateResult.Value, user, seat));
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
    /// <param name="newUser">El nuevo usuario asociado a la reserva.</param>
    /// <returns>Un resultado que indica si la operación fue exitosa.</returns>
    public Result ChangeUser(User? newUser)
    {
        if (newUser is null)
        {
            return Result.Failure(Errors.User.IsNull);
        }

        UserId = newUser.Id;
        User = newUser;

        return Result.Success();
    }

    /// <summary>
    /// Cambia el asiento asociado a la reserva.
    /// </summary>
    /// <param name="newSeat">El nuevo asiento asociado a la reserva.</param>
    /// <returns>Un resultado que indica si la operación fue exitosa.</returns>
    public Result ChangeSeat(Seat? newSeat)
    {
        if (newSeat is null)
        {
            return Result.Failure(Errors.Seat.IsNull);
        }

        SeatId = newSeat.Id;
        Seat = newSeat;

        return Result.Success();
    }
}
