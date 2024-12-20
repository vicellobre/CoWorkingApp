using CoWorkingApp.Core.DomainErrors;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class ChangeNameTests
    {
        [Fact]
        public void ChangeName_ShouldReturnFailure_WhenNumberIsInvalid()
        {
            // Arrange
            var seat = GetCreateValidSeat();
            string number = "";
            string row = "A";

            // Act
            var result = seat.ChangeName(number, row);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.SeatNumber.IsNullOrEmpty, result.FirstError);
        }

        [Fact]
        public void ChangeName_ShouldReturnFailure_WhenRowIsInvalid()
        {
            // Arrange
            var seat = GetCreateValidSeat();
            string number = "10";
            string row = "";

            // Act
            var result = seat.ChangeName(number, row);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.SeatRow.IsNullOrEmpty, result.FirstError);
        }

        [Fact]
        public void ChangeName_ShouldReturnSuccess_WhenValuesAreValid()
        {
            // Arrange
            var seat = GetCreateValidSeat();
            string number = "20";
            string row = "B";

            // Act
            var result = seat.ChangeName(number, row);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("B-20", seat.Name.ToString());
        }
    }
}
