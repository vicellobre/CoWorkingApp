using CoWorkingApp.Core.Errors;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class ChangeSeatTests
    {
        [Fact]
        public void ChangeSeat_ShouldReturnFailure_WhenSeatIsNull()
        {
            // Arrange
            var reservation = GetCreateValidReservation();

            // Act
            var result = reservation.ChangeSeat(null);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ERRORS.Seat.IsNull, result.FirstError);
        }

        [Fact]
        public void ChangeSeat_ShouldReturnSuccess_WhenSeatIsValid()
        {
            // Arrange
            var reservation = GetCreateValidReservation();
            var newSeat = GetCreateValidSeat();

            // Act
            var result = reservation.ChangeSeat(newSeat);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newSeat.Id, reservation.SeatId);
            Assert.Equal(newSeat, reservation.Seat);
        }
    }
}
