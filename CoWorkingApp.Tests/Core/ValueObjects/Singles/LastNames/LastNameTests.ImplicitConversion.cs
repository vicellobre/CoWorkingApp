using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.LastNames;

public partial class LastNameTest
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnLastNameValue()
        {
            // Arrange
            var value = "ValidName";
            var lastName = LastName.Create(value).Value;

            // Act
            string result = lastName;

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ImplicitConversion_ShouldThrowInvalidOperationException_WhenLastNameIsEmpty()
        {
            // Arrange
            var result = LastName.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => {
                string lastName = result.Value;
            });
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
