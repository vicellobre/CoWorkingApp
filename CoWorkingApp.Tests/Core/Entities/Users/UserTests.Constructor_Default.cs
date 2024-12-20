using CoWorkingApp.Core.Entities;
using System.Reflection;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class Constructor_DefaultTests
    {
        [Fact]
        public void PrivateConstructor_ShouldInstantiateUser()
        {
            // Arrange
            var constructorInfo = typeof(User).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);

            // Act
            var user = (User)constructorInfo?.Invoke(null)!;

            // Assert
            Assert.NotNull(user);
            Assert.Equal(Guid.Empty, user.Id); // El ID debe ser Guid.Empty porque no se inicializa en el constructor sin parámetros
            Assert.Null(user.Name);
            Assert.Null(user.Credentials);
            Assert.Empty(user.Reservations);
        }
    }
}
