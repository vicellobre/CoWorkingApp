using CoWorkingApp.Core.Entities;
using System.Reflection;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class Constructor_DefaultTests
    {
        [Fact]
        public void ConstructorDefault_ShouldInstantiateReservation()
        {
            // Arrange
            var constructorInfo = typeof(Reservation).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);

            // Act
            var reservation = (Reservation)constructorInfo?.Invoke(null)!;

            // Assert
            Assert.NotNull(reservation);
            Assert.Equal(Guid.Empty, reservation.Id); // El ID debe ser Guid.Empty porque no se inicializa en el constructor sin parámetros
            Assert.Equal(default, reservation.Date);
            Assert.Equal(Guid.Empty, reservation.UserId);
            Assert.Null(reservation.User);
            Assert.Equal(Guid.Empty, reservation.SeatId);
            Assert.Null(reservation.Seat);
        }
    }
}
