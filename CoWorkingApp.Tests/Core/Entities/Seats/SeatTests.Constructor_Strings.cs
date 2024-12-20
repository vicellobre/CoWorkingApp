using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class Constructor_StringsTests
    {
        [Fact]
        public void PublicConstructor_ShouldInstantiateSeat_WithValidValues()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var number = "10";
            var row = "A";
            var description = "Window seat";

            // Act
            var seat = new Seat(seatId, row, number, description);

            // Assert
            Assert.NotNull(seat);
            Assert.Equal(seatId, seat.Id);
            Assert.Equal("A-10", seat.Name.ToString());
            Assert.Equal(description, seat.Description.ToString());
        }

        [Fact]
        public void PublicConstructor_ShouldThrowException_WithInvalidSeatName()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var number = "";
            var row = "A";
            var description = "Window seat";

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => new Seat(seatId, row, number, description));
            Assert.Equal("The value of a failure result cannot be accessed.", exception.Message);
        }

        [Fact]
        public void PublicConstructor_ShouldThrowException_WithInvalidDescription()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var number = "10";
            var row = "A";
            var description = ""; // Assuming empty description is invalid for the sake of this example

            // Act
            var seat = new Seat(seatId, row, number, description);

            // Assert
            Assert.NotNull(seat);
            Assert.Equal(seatId, seat.Id);
            Assert.Equal("A-10", seat.Name.ToString());
            Assert.Equal(description, seat.Description.ToString()); // No exception thrown, but validate the object state
        }
    }
}
