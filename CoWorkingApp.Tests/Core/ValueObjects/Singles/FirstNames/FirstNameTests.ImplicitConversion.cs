using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Tests.Core.ValueObjects.Singles.FirstNames;

public partial class FirstNameTest
{
    public class ImplicitConversionTests
    {
        [Fact]
        public void ImplicitConversion_ShouldReturnFirstNameValue()
        {
            // Arrange
            var value = "ValidName";
            var firstName = FirstName.Create(value).Value;

            // Act
            string result = firstName;

            // Assert
            Assert.Equal(value, result);
        }

        [Fact]
        public void ImplicitConversion_ShouldThrowInvalidOperationException_WhenFirstNameIsEmpty()
        {
            // Arrange
            var result = FirstName.Create(string.Empty);
            var expectedMessage = "The value of a failure result cannot be accessed.";

            // Act & Assert
            Assert.False(result.IsSuccess);
            var exception = Assert.Throws<InvalidOperationException>(() => {
                string firstName = result.Value;
            });
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
