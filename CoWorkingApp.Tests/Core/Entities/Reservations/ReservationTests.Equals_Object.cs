namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class EqualsObjectTests
    {
        [Fact]
        public void Equals_ShouldReturnFalse_WhenObjectIsNull()
        {
            // Arrange
            var reservation = GetCreateValidReservation();

            // Act
            var result = reservation.Equals((object?)null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameReference()
        {
            // Arrange
            var reservation = GetCreateValidReservation();

            // Act
            var result = reservation.Equals((object)reservation);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenDifferentType()
        {
            // Arrange
            var reservation = GetCreateValidReservation();
            var other = new AnotherEntityType { Id = reservation.Id };

            // Act
            var result = reservation.Equals((object)other);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameId()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            var reservation1 = GetCreateValidReservationWithId(reservationId);
            var reservation2 = GetCreateValidReservationWithId(reservationId);

            // Act
            var result = reservation1.Equals((object)reservation2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenDifferentId()
        {
            // Arrange
            var reservation1 = GetCreateValidReservation();
            var reservation2 = GetCreateValidReservation();

            // Act
            var result = reservation1.Equals((object)reservation2);

            // Assert
            Assert.False(result);
        }
    }
}
