using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.SeatNumbers;

public partial class SeatNumberTests
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnSeatNumberValue()
        {
            // Arrange
            var value = "A23";
            var seatNumber = SeatNumber.Create(value).Value;

            // Act
            string result = seatNumber;

            // Assert
            Assert.Equal(value, result);
        }
    }
}
