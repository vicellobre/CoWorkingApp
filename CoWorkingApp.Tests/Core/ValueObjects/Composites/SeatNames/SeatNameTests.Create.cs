using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composites.SeatNames;

public partial class SeatNameTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenSeatRowIsInvalid()
        {
            // Arrange
            var seatRow = "";
            var seatNumber = "A1";

            // Act
            var result = SeatName.Create(seatRow, seatNumber);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.SeatRow.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenSeatNumberIsInvalid()
        {
            // Arrange
            var seatRow = "1";
            var seatNumber = "";

            // Act
            var result = SeatName.Create(seatRow, seatNumber);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.SeatNumber.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenSeatRowAndSeatNumberAreInvalid()
        {
            // Arrange
            var seatRow = "";
            var seatNumber = "";

            // Act
            var result = SeatName.Create(seatRow, seatNumber);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Errors.SeatRow.IsNullOrEmpty, result.Errors);
            Assert.Contains(Errors.SeatNumber.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenSeatRowAndSeatNumberAreValid()
        {
            // Arrange
            var seatRow = "1";
            var seatNumber = "A1";

            // Act
            var result = SeatName.Create(seatRow, seatNumber);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(seatRow, result.Value.Row.Value);
            Assert.Equal(seatNumber, result.Value.Number.Value);
        }
    }
}
