using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Passwords;

public partial class PasswordTest
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnPasswordValue()
        {
            // Arrange
            var value = "Valid1@Password";
            var password = Password.Create(value).Value;

            // Act
            var result = password.ToString();

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ToString_ShouldThrowInvalidOperationException_WhenPasswordIsEmpty()
        {
            // Arrange
            var result = Password.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => result.Value.ToString());
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
