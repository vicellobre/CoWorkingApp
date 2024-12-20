using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;
using System.Reflection;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class Constructor_ValueObjectsTests
    {
        [Fact]
        public void ConstructorValueObjects_ShouldInstantiateSeat()
        {
            // Arrange
            var constructorInfo = typeof(Seat).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(Guid), typeof(SeatName), typeof(Description)],
                null);

            var seatId = Guid.NewGuid();
            var seatName = SeatName.Create("A", "10").Value;
            var description = Description.Create("Window seat").Value;

            // Act
            var seat = (Seat)constructorInfo?.Invoke([seatId, seatName, description])!;

            // Assert
            Assert.NotNull(seat);
            Assert.Equal(seatId, seat.Id);
            Assert.Equal(seatName, seat.Name);
            Assert.Equal(description, seat.Description);
        }
    }
}
