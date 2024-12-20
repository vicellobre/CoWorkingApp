using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class GetHashCodeTests
    {
        [Fact]
        public void GetHashCode_ShouldReturnSameHashCode_ForSameId()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            var reservation1 = Reservation.Create(reservationId, DateTime.Now.AddDays(1), GetCreateValidUser(), GetCreateValidSeat()).Value;
            var reservation2 = Reservation.Create(reservationId, DateTime.Now.AddDays(2), GetCreateValidUser(), GetCreateValidSeat()).Value;

            // Act
            var hash1 = reservation1.GetHashCode();
            var hash2 = reservation2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCodes_ForDifferentIds()
        {
            // Arrange
            var reservation1 = GetCreateValidReservation();
            var reservation2 = GetCreateValidReservation();

            // Act
            var hash1 = reservation1.GetHashCode();
            var hash2 = reservation2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
}
