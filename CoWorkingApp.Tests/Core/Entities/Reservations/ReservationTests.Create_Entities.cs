using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class CreateWithUserSeatTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenDateIsInvalid()
        {
            // Arrange
            Guid reservationId = Guid.NewGuid();
            DateTime invalidDate = DateTime.MinValue;
            var user = GetCreateValidUser();
            var seat = GetCreateValidSeat();

            // Act
            var result = Reservation.Create(reservationId, invalidDate, user, seat);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.Date.Invalid, result.FirstError);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenAllValuesAreValid()
        {
            // Arrange
            Guid reservationId = Guid.NewGuid();
            DateTime validDate = DateTime.Now.AddDays(1); // Assumed future date is valid
            var user = GetCreateValidUser();
            var seat = GetCreateValidSeat();

            // Act
            var result = Reservation.Create(reservationId, validDate, user, seat);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(reservationId, result.Value.Id);
            Assert.Equal(validDate, result.Value.Date.Value);
            Assert.Equal(user.Id, result.Value.UserId);
            Assert.Equal(user, result.Value.User);
            Assert.Equal(seat.Id, result.Value.SeatId);
            Assert.Equal(seat, result.Value.Seat);
        }
    }
}
