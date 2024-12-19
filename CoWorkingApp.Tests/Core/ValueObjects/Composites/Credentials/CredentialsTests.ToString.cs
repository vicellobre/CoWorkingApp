using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composite.Credentials;

public partial class CredentialsWithEmailAndPasswordTests
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnCredentialsValue()
        {
            // Arrange
            var email = "valid.email@example.com";
            var password = "Valid1@Password";
            var credentials = CredentialsWithEmailAndPassword.Create(email, password).Value;

            // Act
            var result = credentials.ToString();

            // Assert
            var expectedValue = $"{email}  {password}";
            Assert.Equal(expectedValue, result);
        }
    }
}
