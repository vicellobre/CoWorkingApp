namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class AddReservationTests
    {
        [Fact]
        public void AddReservation_ShouldAddReservationToList()
        {
            // Arrange
            var seat = GetCreateValidSeat();
            var reservation = CreateReservation();

            // Act
            seat.AddReservation(reservation);

            // Assert
            Assert.Contains(reservation, seat.Reservations);
        }
    }
}
