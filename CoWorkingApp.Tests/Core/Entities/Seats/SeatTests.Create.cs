using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class CreateTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenNumberIsInvalid()
        {
            // Arrange
            Guid seatId = Guid.NewGuid();
            string number = "";
            string row = "A";
            string description = "Window seat";

            // Act
            var result = Seat.Create(seatId, row, number, description);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(Errors.SeatNumber.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenRowIsInvalid()
        {
            // Arrange
            Guid seatId = Guid.NewGuid();
            string number = "10";
            string row = "";
            string description = "Window seat";

            // Act
            var result = Seat.Create(seatId, row, number, description);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains(Errors.SeatRow.IsNullOrEmpty, result.Errors);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenAllValuesAreValid()
        {
            // Arrange
            Guid seatId = Guid.NewGuid();
            string number = "10";
            string row = "A";
            string description = "Window seat";

            // Act
            var result = Seat.Create(seatId, row, number, description);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(seatId, result.Value.Id);
            Assert.Equal("A-10", result.Value.Name.ToString());
            Assert.Equal(description, result.Value.Description.ToString());
        }
    }
}
