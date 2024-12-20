using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.ValueObjects.Composite;
using System.Reflection;

namespace CoWorkingApp.Tests.Core.Entities.Users;

public partial class UserTests
{
    public class Constructor_ValueObjectsTests
    {
        [Fact]
        public void PrivateConstructor_ShouldInstantiateUser()
        {
            // Arrange
            var constructorInfo = typeof(User).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                [typeof(Guid), typeof(FullName), typeof(CredentialsWithEmailAndPassword)],
                null);

            var userId = Guid.NewGuid();
            var fullName = FullName.Create("John", "Doe").Value;
            var credentials = CredentialsWithEmailAndPassword.Create("john.doe@example.com", "Valid1!").Value;

            // Act
            var user = (User)constructorInfo?.Invoke([userId, fullName, credentials])!;

            // Assert
            Assert.NotNull(user);
            Assert.Equal(userId, user.Id);
            Assert.Equal(fullName, user.Name);
            Assert.Equal(credentials, user.Credentials);
        }
    }
}
