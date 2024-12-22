using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Emails;

public partial class EmailTest
{
    public class ToStringTests
    {
        [Fact]
        public void ToString_ShouldReturnEmailValue()
        {
            // Arrange
            var value = "test@example.com";
            var email = Email.Create(value).Value;

            // Act
            var result = email.ToString();

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ToString_ShouldThrowInvalidOperationException_WhenEmailIsInvalid()
        {
            // Arrange
            var result = Email.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => result.Value.ToString()!);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
