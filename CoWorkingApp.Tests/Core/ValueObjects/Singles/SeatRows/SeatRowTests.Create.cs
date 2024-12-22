using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.SeatRows;

public partial class SeatRowTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsNullOrEmpty()
        {
            // Arrange
            string? value = null;
            var expectedError = Errors.SeatRow.IsNullOrEmpty;

            // Act
            var result = SeatRow.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenValueIsWhitespace()
        {
            // Arrange
            var value = "   ";
            var expectedError = Errors.SeatRow.IsNullOrEmpty;

            // Act
            var result = SeatRow.Create(value);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedError, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenValueIsValid()
        {
            // Arrange
            var value = "A1";

            // Act
            var result = SeatRow.Create(value);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value.Value);
        }
    }
}
