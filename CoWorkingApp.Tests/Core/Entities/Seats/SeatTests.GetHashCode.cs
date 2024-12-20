using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class GetHashCodeTests
    {
        [Fact]
        public void GetHashCode_ShouldReturnSameHashCode_ForSameId()
        {
            // Arrange
            var seatId = Guid.NewGuid();
            var seat1 = Seat.Create(seatId, "10", "A", "Window seat").Value;
            var seat2 = Seat.Create(seatId, "20", "B", "Aisle seat").Value;

            // Act
            var hash1 = seat1.GetHashCode();
            var hash2 = seat2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCodes_ForDifferentIds()
        {
            // Arrange
            var seat1 = GetCreateValidSeat();
            var seat2 = GetCreateValidSeat();

            // Act
            var hash1 = seat1.GetHashCode();
            var hash2 = seat2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
}
