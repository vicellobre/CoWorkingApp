using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.SeatNumbers;

public partial class SeatNumberTests
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnSeatNumberValue()
        {
            // Arrange
            var value = "A23";
            var seatNumber = SeatNumber.Create(value).Value;

            // Act
            var result = seatNumber.ToString();

            // Assert
            Assert.Equal(value, result);
        }
    }
}
