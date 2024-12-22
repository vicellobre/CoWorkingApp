using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class AnotherEntityType : EntityBase;

    private static Seat GetCreateValidSeat()
    {
        var seatId = Guid.NewGuid();
        var number = "10";
        var row = "A";
        var description = "Window seat";

        return Seat.Create(seatId, row, number, description).Value;
    }

    private static Reservation CreateReservation()
    {
        // Crear y devolver una instancia de reserva válida aquí
        // Este método debería incluir la lógica para crear una reserva válida
        //return new Reservation();
        return null!;
    }
}
