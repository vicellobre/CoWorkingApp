namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class RemoveReservationTests
    {
        [Fact]
        public void RemoveReservation_ShouldRemoveReservationFromList()
        {
            // Arrange
            var user = GetCreateValidUser();
            var reservation = CreateReservation();
            user.AddReservation(reservation);

            // Act
            user.RemoveReservation(reservation);

            // Assert
            Assert.DoesNotContain(reservation, user.Reservations);
        }
    }
}
