using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.SeatRows;

public partial class SeatRowTests
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnSeatRowValue()
        {
            // Arrange
            var value = "A1";
            var seatRow = SeatRow.Create(value).Value;

            // Act
            var result = seatRow.ToString();

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ToString_ShouldThrowInvalidOperationException_WhenSeatRowIsInvalid()
        {
            // Arrange
            var result = SeatRow.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => result.Value.ToString());
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
