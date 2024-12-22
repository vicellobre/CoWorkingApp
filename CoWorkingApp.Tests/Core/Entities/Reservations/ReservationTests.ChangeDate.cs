using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class ChangeDateTests
    {
        [Fact]
        public void ChangeDate_ShouldReturnFailure_WhenDateIsInvalid()
        {
            // Arrange
            var reservation = GetCreateValidReservation();
            DateTime invalidDate = DateTime.MinValue;

            // Act
            var result = reservation.ChangeDate(invalidDate);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.Date.Invalid, result.FirstError);
        }

        [Fact]
        public void ChangeDate_ShouldReturnSuccess_WhenDateIsValid()
        {
            // Arrange
            var reservation = GetCreateValidReservation();
            DateTime newDate = DateTime.Now.AddDays(10);

            // Act
            var result = reservation.ChangeDate(newDate);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newDate, reservation.Date.Value);
        }
    }
}
