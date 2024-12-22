using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class EqualityOperatorsTests
    {
        [Fact]
        public void EqualityOperator_ShouldReturnTrue_WhenBothAreNull()
        {
            // Arrange
            Seat? seat1 = null;
            Seat? seat2 = null;

            // Act
            var result = seat1 == seat2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnFalse_WhenOneIsNull()
        {
            // Arrange
            var seat1 = GetCreateValidSeat();
            Seat? seat2 = null;

            // Act
            var result = seat1 == seat2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnTrue_WhenBothHaveSameId()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var seat1 = Seat.Create(seatId, "10", "A", "Window seat").Value;
            var seat2 = Seat.Create(seatId, "20", "B", "Aisle seat").Value;

            // Act
            var result = seat1 == seat2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnFalse_WhenBothAreNull()
        {
            // Arrange
            Seat? seat1 = null;
            Seat? seat2 = null;

            // Act
            var result = seat1 != seat2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_WhenOneIsNull()
        {
            // Arrange
            var seat1 = GetCreateValidSeat();
            Seat? seat2 = null;

            // Act
            var result = seat1 != seat2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_WhenDifferentIds()
        {
            // Arrange
            var seat1 = GetCreateValidSeat();
            var seat2 = GetCreateValidSeat();

            // Act
            var result = seat1 != seat2;

            // Assert
            Assert.True(result);
        }
    }
}