using CoWorkingApp.Core.Entities;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class EqualityOperatorsTests
    {
        [Fact]
        public void EqualityOperator_ShouldReturnTrue_WhenBothAreNull()
        {
            // Arrange
            Reservation? reservation1 = null;
            Reservation? reservation2 = null;

            // Act
            var result = reservation1 == reservation2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnFalse_WhenOneIsNull()
        {
            // Arrange
            var reservation1 = GetCreateValidReservation();
            Reservation? reservation2 = null;

            // Act
            var result = reservation1 == reservation2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualityOperator_ShouldReturnTrue_WhenBothHaveSameId()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            var reservation1 = Reservation.Create(reservationId, DateTime.Now.AddDays(1), GetCreateValidUser(), GetCreateValidSeat()).Value;
            var reservation2 = Reservation.Create(reservationId, DateTime.Now.AddDays(2), GetCreateValidUser(), GetCreateValidSeat()).Value;

            // Act
            var result = reservation1 == reservation2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnFalse_WhenBothAreNull()
        {
            // Arrange
            Reservation? reservation1 = null;
            Reservation? reservation2 = null;

            // Act
            var result = reservation1 != reservation2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_WhenOneIsNull()
        {
            // Arrange
            var reservation1 = GetCreateValidReservation();
            Reservation? reservation2 = null;

            // Act
            var result = reservation1 != reservation2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ShouldReturnTrue_WhenDifferentIds()
        {
            // Arrange
            var reservation1 = GetCreateValidReservation();
            var reservation2 = GetCreateValidReservation();

            // Act
            var result = reservation1 != reservation2;

            // Assert
            Assert.True(result);
        }
    }
}
