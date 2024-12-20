using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class CreateWithUserIdSeatIdTests
    {
        [Fact]
        public void Create_ShouldReturnFailure_WhenDateIsInvalid()
        {
            // Arrange
            Guid reservationId = Guid.NewGuid();
            DateTime invalidDate = DateTime.MinValue;
            Guid userId = Guid.NewGuid();
            Guid seatId = Guid.NewGuid();

            // Act
            var result = Reservation.Create(reservationId, invalidDate, userId, seatId);

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
            Guid userId = Guid.NewGuid();
            Guid seatId = Guid.NewGuid();

            // Act
            var result = Reservation.Create(reservationId, validDate, userId, seatId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(reservationId, result.Value.Id);
            Assert.Equal(validDate, result.Value.Date.Value);
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal(seatId, result.Value.SeatId);
        }
    }
}
