namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class AddReservationTests
    {
        [Fact]
        public void AddReservation_ShouldAddReservationToList()
        {
            // Arrange
            var user = GetCreateValidUser();
            var reservation = CreateReservation();

            // Act
            user.AddReservation(reservation);

            // Assert
            Assert.Contains(reservation, user.Reservations);
        }
    }
}
