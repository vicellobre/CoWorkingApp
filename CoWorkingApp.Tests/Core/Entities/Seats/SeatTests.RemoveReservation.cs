namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class RemoveReservationTests
    {
        [Fact]
        public void RemoveReservation_ShouldRemoveReservationFromList()
        {
            // Arrange
            var seat = GetCreateValidSeat();
            var reservation = CreateReservation();
            seat.AddReservation(reservation);

            // Act
            seat.RemoveReservation(reservation);

            // Assert
            Assert.DoesNotContain(reservation, seat.Reservations);
        }
    }
}
