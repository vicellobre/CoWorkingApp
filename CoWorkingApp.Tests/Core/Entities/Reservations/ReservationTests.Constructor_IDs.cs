using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Single;
using System.Reflection;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class Constructor_IdsTests
    {
        [Fact]
        public void PrivateConstructor_ShouldInstantiateReservation_WithUserIdAndSeatId()
        {
            // Arrange
            var constructorInfo = typeof(Reservation).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(Guid), typeof(Date), typeof(Guid), typeof(Guid)],
                null);

            var reservationId = Guid.NewGuid();
            var date = Date.Create(DateTime.Now.AddDays(1)).Value;
            var userId = Guid.NewGuid();
            var seatId = Guid.NewGuid();

            // Act
            var reservation = (Reservation)constructorInfo?.Invoke([reservationId, date, userId, seatId])!;

            // Assert
            Assert.NotNull(reservation);
            Assert.Equal(reservationId, reservation.Id);
            Assert.Equal(date, reservation.Date);
            Assert.Equal(userId, reservation.UserId);
            Assert.Equal(seatId, reservation.SeatId);
        }
    }
}
