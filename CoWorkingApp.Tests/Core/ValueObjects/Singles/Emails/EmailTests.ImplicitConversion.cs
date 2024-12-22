using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.Emails;

public partial class EmailTest
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnEmailValue()
        {
            // Arrange
            var value = "implicit@test.com";
            var email = Email.Create(value).Value;

            // Act
            string result = email;

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ImplicitConversion_ShouldThrowInvalidOperationException_WhenEmailIsInvalid()
        {
            // Arrange
            var result = Email.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => {
                string email = result.Value;
            });
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
