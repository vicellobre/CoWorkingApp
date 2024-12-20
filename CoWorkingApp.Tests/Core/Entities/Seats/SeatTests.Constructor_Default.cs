using CoWorkingApp.Core.Entities;
using System.Reflection;

namespace CoWorkingApp.Tests.Core.Entities.Seats;

public partial class SeatTests
{
    public class Constructor_DefaultTests
    {
        [Fact]
        public void ConstructorDefault_ShouldInstantiateSeat()
        {
            // Arrange
            var constructorInfo = typeof(Seat).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);

            // Act
            var seat = (Seat)constructorInfo?.Invoke(null)!;

            // Assert
            Assert.NotNull(seat);
            Assert.Equal(Guid.Empty, seat.Id); // El ID debe ser Guid.Empty porque no se inicializa en el constructor sin parámetros

            // Dado que son structs, verificamos que los valores predeterminados sean asignados
            Assert.Equal(default, seat.Name);
            Assert.Equal(default, seat.Description);
            Assert.Empty(seat.Reservations);
        }
    }
}
