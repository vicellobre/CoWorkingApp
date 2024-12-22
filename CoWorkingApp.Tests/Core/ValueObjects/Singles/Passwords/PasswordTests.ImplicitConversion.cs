using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Passwords;

public partial class PasswordTest
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnPasswordValue()
        {
            // Arrange
            var value = "Valid1@Password";
            var password = Password.Create(value).Value;

            // Act
            string result = password;

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ImplicitConversion_ShouldThrowInvalidOperationException_WhenPasswordIsEmpty()
        {
            // Arrange
            var result = Password.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => {
                string password = result.Value;
            });
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
