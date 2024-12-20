namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class ChangeDescriptionTests
    {
        [Fact]
        public void ChangeDescription_ShouldUpdateDescription_WhenDescriptionIsValid()
        {
            // Arrange
            var seat = GetCreateValidSeat();
            string newDescription = "New description";

            // Act
            seat.ChangeDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, seat.Description.ToString());
        }
    }
}
