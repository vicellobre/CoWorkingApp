using CoWorkingApp.Core.DomainErrors;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class ChangeUserTests
    {
        [Fact]
        public void ChangeUser_ShouldReturnFailure_WhenUserIsNull()
        {
            // Arrange
            var reservation = GetCreateValidReservation();

            // Act
            var result = reservation.ChangeUser(null);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.User.IsNull, result.FirstError);
        }

        [Fact]
        public void ChangeUser_ShouldReturnSuccess_WhenUserIsValid()
        {
            // Arrange
            var reservation = GetCreateValidReservation();
            var newUser = GetCreateValidUser();

            // Act
            var result = reservation.ChangeUser(newUser);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newUser.Id, reservation.UserId);
            Assert.Equal(newUser, reservation.User);
        }
    }
}
