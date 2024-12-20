using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class EqualsObjectTests
    {
        [Fact]
        public void Equals_ShouldReturnFalse_WhenObjectIsNull()
        {
            // Arrange
            var seat = GetCreateValidSeat();

            // Act
            var result = seat.Equals((object?)null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameReference()
        {
            // Arrange
            var seat = GetCreateValidSeat();

            // Act
            var result = seat.Equals((object)seat);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenDifferentType()
        {
            // Arrange
            var seat = GetCreateValidSeat();
            var other = new AnotherEntityType { Id = seat.Id };

            // Act
            var result = seat.Equals((object)other);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameId()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var seat1 = Seat.Create(seatId, "10", "A", "Window seat").Value;
            var seat2 = Seat.Create(seatId, "20", "B", "Aisle seat").Value;

            // Act
            var result = seat1.Equals((object)seat2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenDifferentId()
        {
            // Arrange
            var seat1 = GetCreateValidSeat();
            var seat2 = GetCreateValidSeat();

            // Act
            var result = seat1.Equals((object)seat2);

            // Assert
            Assert.False(result);
        }
    }
}
