using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Single;
using System.Reflection;

namespace CoWorkingApp.Tests.Core.Entities.Reservations;

public partial class ReservationTests
{
    public class Constructor_EntitiesTests
    {
        [Fact]
        public void PrivateConstructor_ShouldInstantiateReservation_WithUserAndSeat()
        {
            // Arrange
            var constructorInfo = typeof(Reservation).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(Guid), typeof(Date), typeof(User), typeof(Seat)],
                null);

            var reservationId = Guid.NewGuid();
            var date = Date.Create(DateTime.Now.AddDays(1)).Value;
            var user = GetCreateValidUser();
            var seat = GetCreateValidSeat();

            // Act
            var reservation = (Reservation)constructorInfo?.Invoke([reservationId, date, user, seat])!;

            // Assert
            Assert.NotNull(reservation);
            Assert.Equal(reservationId, reservation.Id);
            Assert.Equal(date, reservation.Date);
            Assert.Equal(user.Id, reservation.UserId);
            Assert.Equal(user, reservation.User);
            Assert.Equal(seat.Id, reservation.SeatId);
            Assert.Equal(seat, reservation.Seat);
        }
    }
}
