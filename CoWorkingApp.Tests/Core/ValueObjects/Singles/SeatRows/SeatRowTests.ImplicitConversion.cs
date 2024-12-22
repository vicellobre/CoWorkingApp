using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.SeatRows;

public partial class SeatRowTests
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnSeatRowValue()
        {
            // Arrange
            var value = "A1";
            var seatRow = SeatRow.Create(value).Value;

            // Act
            string result = seatRow;

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ImplicitConversion_ShouldThrowInvalidOperationException_WhenSeatRowIsInvalid()
        {
            // Arrange
            var result = SeatRow.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => {
                string seatRow = result.Value;
            });
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
