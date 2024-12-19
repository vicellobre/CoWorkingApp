using CoWorkingApp.Core.ValueObjects.Composite;

namespace CoWorkingApp.Tests.Core.ValueObjects.Composite.Credentials;

public partial class CredentialsWithEmailAndPasswordTests
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnCredentialsValue()
        {
            // Arrange
            var email = "valid.email@example.com";
            var password = "Valid1@Password";
            var credentials = CredentialsWithEmailAndPassword.Create(email, password).Value;

            // Act
            string result = credentials;

            // Assert
            var expectedValue = $"{email}  {password}";
            Assert.Equal(expectedValue, result);
        }
    }
}
