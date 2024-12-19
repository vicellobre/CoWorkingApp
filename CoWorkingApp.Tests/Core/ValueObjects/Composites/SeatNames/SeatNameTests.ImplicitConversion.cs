using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composites.SeatNames;

public partial class SeatNameTests
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnSeatNameValue()
        {
            // Arrange
            var seatRow = "1";
            var seatNumber = "A1";
            var seatName = SeatName.Create(seatRow, seatNumber).Value;

            // Act
            string result = seatName;

            // Assert
            var expectedValue = $"{seatRow}-{seatNumber}";
            Assert.Equal(expectedValue, result);
        }
    }
}
