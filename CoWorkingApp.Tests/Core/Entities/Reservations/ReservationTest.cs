using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class AnotherEntityType : EntityBase;

    public static Reservation GetCreateValidReservationWithId(Guid reservationId)
    {
        DateTime validDate = DateTime.Now.AddDays(1);
        var user = GetCreateValidUser();
        var seat = GetCreateValidSeat();

        return Reservation.Create(reservationId, validDate, user, seat).Value;
    }

    public static Reservation GetCreateValidReservation()
    {
        Guid reservationId = Guid.NewGuid();
        DateTime validDate = DateTime.Now.AddDays(1);
        var user = GetCreateValidUser();
        var seat = GetCreateValidSeat();

        return Reservation.Create(reservationId, validDate, user, seat).Value;
    }

    public static User GetCreateValidUser()
    {
        var userId = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var email = "valid@example.com";
        var password = "Valid1!";

        return User.Create(userId, firstName, lastName, email, password).Value;
    }

    public static Seat GetCreateValidSeat()
    {
        var seatId = Guid.NewGuid();
        var number = "10";
        var row = "A";
        var description = "Window seat";

        return Seat.Create(seatId, number, row, description).Value;
    }
}
